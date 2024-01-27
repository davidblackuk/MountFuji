using System.Collections.ObjectModel;
using MyAtariCollection.Services.Filesystem;


namespace MyAtariCollection.ViewModels;

public partial class MainViewModel : TinyViewModel
{
    private readonly IConfigFileService configFileService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IServiceProvider serviceProvider;
    private readonly IPreferencesService preferencesService;
    private readonly SystemsService systemsService;
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly ILogger<MainViewModel> log;

    public MainViewModel(IConfigFileService configFileService,
        IPopupNavigation popupNavigation,
        IServiceProvider serviceProvider,
        IPreferencesService preferencesService,
        SystemsService systemsService,
        IFujiFilePickerService fujiFilePicker,
        ILogger<MainViewModel> log)
    {
        this.configFileService = configFileService;
        this.popupNavigation = popupNavigation;
        this.serviceProvider = serviceProvider;
        this.preferencesService = preferencesService;
        this.systemsService = systemsService;
        this.fujiFilePicker = fujiFilePicker;
        this.log = log;

        UpdateSystemsFromService();
    }

    public ObservableCollection<AtariConfiguration> Systems { get; } = new();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasSelectedConfig))]
    private AtariConfiguration selectedConfiguration = AtariConfiguration.Empty;

    [ObservableProperty] private SectionVisibility sectionVisibility = new SectionVisibility();

    [ObservableProperty] private int numberOfSystems;

    public bool HasSelectedConfig =>
        SelectedConfiguration is not null && SelectedConfiguration.Id != AtariConfiguration.Empty.Id;


    private IDispatcherTimer timer;
    
    public override Task OnFirstAppear()
    {
        if (Systems is not null && Systems.Count > 0)
        {
            SelectedConfiguration = Systems.First();
        }
        RunCommand.NotifyCanExecuteChanged();

        timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(2);
        timer.Tick += TimerTick;
        timer.Start();
        return base.OnFirstAppear();
    }

    
    
    


    #region ---- RELAY COMMANDS ----

    [RelayCommand()]
    private async Task BrowseRoms()
    {
        var file = await fujiFilePicker.PickFile("ROM Image", (filename) =>
            {
                SelectedConfiguration.RomImage = filename;
                RunCommand.NotifyCanExecuteChanged();
            },
            preferencesService.Preferences.RomFolder);
    }

    [RelayCommand]
    private void ClearRom()
    {
        SelectedConfiguration.RomImage = String.Empty;
        RunCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand()]
    private async Task BrowseCartridges()
    {
        await fujiFilePicker.PickFile("Cartridge Image", (filename) => SelectedConfiguration.CartridgeImage = filename,
            preferencesService.Preferences.CartridgeFolder);
    }

    [RelayCommand]
    private void ClearCartridge()
    {
        SelectedConfiguration.CartridgeImage = String.Empty;
    }

    #region ---- DISK COMMANDS ----

    [RelayCommand()]
    private async Task BrowseAcsiDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("ASCI Disk Image",
            (filename) => SelectedConfiguration.AcsiImagePaths.SetImagePath(diskId, filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void ClearAcsiDiskImage(int diskId) => SelectedConfiguration.AcsiImagePaths.ClearImagePath(diskId);


    [RelayCommand()]
    private async Task BrowseScsiDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("SCSI Disk Image",
            (filename) => SelectedConfiguration.ScsiImagePaths.SetImagePath(diskId, filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void ClearScsiDiskImage(int diskId) => SelectedConfiguration.ScsiImagePaths.ClearImagePath(diskId);

    [RelayCommand()]
    private async Task BrowseIdeDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("IDE Disk Image",
            (filename) => SelectedConfiguration.IdeOptions.SetImagePath(diskId, filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void ClearIdeDiskImage(int diskId) => SelectedConfiguration.IdeOptions.ClearImagePath(diskId);

    [RelayCommand()]
    private async Task BrowseFloppyDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("Floppy Disk Image",
            (filename) => SelectedConfiguration.FloppyOptions.SetImagePath(diskId, filename),
            preferencesService.Preferences.FloppyDiskFolder);
    }

    [RelayCommand()]
    private void ClearFloppyDiskImage(int diskId) => SelectedConfiguration.FloppyOptions.ClearImagePath(diskId);


    [RelayCommand()]
    private async Task BrowseGemdosFolder()
    {
        await fujiFilePicker.PickFolder("GEMDOS Folder",
            (filename) => SelectedConfiguration.GdosDriveOptions.GemdosFolder = filename,
            preferencesService.Preferences.GemDosFolder);
    }


    [RelayCommand()]
    private void ClearGemdosFolder(int diskId) => SelectedConfiguration.GdosDriveOptions.GemdosFolder = string.Empty;

    #endregion


    #region ---- CRUD ----

    [RelayCommand]
    private async Task CreateNewSystem()
    {
        var popup = serviceProvider.GetService<NewSystemPopup>();
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (!popup.ViewModelViewModel.Confirmed) return;
            var system = popup.ViewModelViewModel.GetConfiguration();
            systemsService.Add(system);
            UpdateSystemsFromService();
            SelectedConfiguration = system;
        };
    }

    [RelayCommand]
    private async Task DeleteSystem(string id)
    {
        AtariConfiguration system = systemsService.Find(id);
        if (system is null) return;

        DeleteSystemPopup popup = serviceProvider.GetService<DeleteSystemPopup>();
        popup.ViewModel.System = system;
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += async (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            int currentIndex = Systems.IndexOf(system);
            int newIndex = currentIndex > 0 ? currentIndex - 1 : 0;
            systemsService.Delete(system.Id);
            UpdateSystemsFromService();

            SelectedConfiguration = Systems.Count > 0 ? Systems[newIndex] : AtariConfiguration.Empty;
        };
    }

    [RelayCommand]
    private async Task CloneSystem(string id)
    {
        AtariConfiguration system = systemsService.Find(id);
        if (system is null) return;


        CloneSystemPopup popup = serviceProvider.GetService<CloneSystemPopup>();
        popup.ViewModel.System = system;

        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            AtariConfiguration clone = systemsService.Clone(system, popup.ViewModel.NewName);
            UpdateSystemsFromService();
            SelectedConfiguration = clone;
        };
    }

    #endregion

    [RelayCommand]
    private async Task EditPreferences()
    {
        var popup = serviceProvider.GetService<PreferencesPopup>();
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += async (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            await preferencesService.Save();
            RunCommand.NotifyCanExecuteChanged();
        };
    }

    [RelayCommand(CanExecute = nameof(SaveNeeded))]
    private async Task SaveSystems()
    {
        await systemsService.Save();
    }

    [RelayCommand(CanExecute = nameof(CanRun))]
    private async Task Run()
    {
        await configFileService.Persist(SelectedConfiguration);

        // TODO - Platform specific? is it File:// on PC it is on mac
        var app = preferencesService.Preferences.HatariApplication;
        var applicationUrl = $"file://{app}";
        log.LogInformation("Launching application: {Url}", applicationUrl);
        await Launcher.Default.OpenAsync(applicationUrl);
    }

    [RelayCommand]
    private  Task Reordered()
    {
        ReorderServicesFromDisplayOrder();
        return Task.CompletedTask;
    }

    private bool CanRun()
    {
        if (string.IsNullOrWhiteSpace(preferencesService.Preferences.HatariApplication) ||
            string.IsNullOrWhiteSpace(preferencesService.Preferences.HatariConfigFile) ||
            SelectedConfiguration is null ||
            string.IsNullOrWhiteSpace(SelectedConfiguration.RomImage))
        {
            return false;
        }

        return true;
    }

    private bool SaveNeeded => systemsService.IsDirty;
    
    
    #endregion


    #region ---- HELPERS ----

    private void UpdateSystemsFromService()
    {
        Systems.Clear();
        int count = 0;
        foreach (var system in  systemsService.All())
        {
            Systems.Add(system);
            count++;
        }

        NumberOfSystems = count;
    }
    
    
    private void ReorderServicesFromDisplayOrder()
    {
        var ids = Systems.Select(s => s.Id).ToList();
        systemsService.ReorderByIds(ids);
    }



    private void TimerTick(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            SaveSystemsCommand.NotifyCanExecuteChanged();
        });
    }
    
    #endregion
}