using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MountFuji.Extensions;
using MountFuji.Views;

namespace MountFuji.ViewModels;

public partial class MainViewModel : TinyViewModel
{
    private readonly IConfigFileService configFileService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IServiceProvider serviceProvider;
    private readonly IPreferencesService preferencesService;
    private readonly ISystemsService systemsService;
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly ILogger<MainViewModel> log;

    public MainViewModel(IConfigFileService configFileService,
        IPopupNavigation popupNavigation,
        IServiceProvider serviceProvider,
        IPreferencesService preferencesService,
        ISystemsService systemsService,
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
            (filename) =>
                DiskImagePathsExtensions.SetImagePath((AcsiScsiDiskOptions)SelectedConfiguration.AcsiImagePaths, diskId,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void ClearAcsiDiskImage(int diskId) =>
        DiskImagePathsExtensions.ClearImagePath((AcsiScsiDiskOptions)SelectedConfiguration.AcsiImagePaths, diskId);


    [RelayCommand()]
    private async Task BrowseScsiDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("SCSI Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath((AcsiScsiDiskOptions)SelectedConfiguration.ScsiImagePaths, diskId,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void ClearScsiDiskImage(int diskId) =>
        DiskImagePathsExtensions.ClearImagePath((AcsiScsiDiskOptions)SelectedConfiguration.ScsiImagePaths, diskId);

    [RelayCommand()]
    private async Task BrowseIdeDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("IDE Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath((IdeDiskOptions)SelectedConfiguration.IdeOptions, diskId,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void ClearIdeDiskImage(int diskId) =>
        DiskImagePathsExtensions.ClearImagePath((IdeDiskOptions)SelectedConfiguration.IdeOptions, diskId);

    [RelayCommand()]
    private async Task BrowseFloppyDiskImage(int diskId)
    {
        await fujiFilePicker.PickFile("Floppy Disk Image",
            (filename) => DiskImagePathsExtensions.SetImagePath((FloppyDriveOptions)SelectedConfiguration.FloppyOptions,
                diskId, filename),
            preferencesService.Preferences.FloppyDiskFolder);
    }

    [RelayCommand()]
    private void ClearFloppyDiskImage(int diskId) =>
        DiskImagePathsExtensions.ClearImagePath((FloppyDriveOptions)SelectedConfiguration.FloppyOptions, diskId);


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

        popup.Disappearing += (sender, args) =>
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
    private async Task About()
    {
        AboutPopup popup = serviceProvider.GetService<AboutPopup>();

        await popupNavigation.PushAsync(popup);
        
    }
    
    [RelayCommand]
    private async Task ImportHatariConfig()
    {
        ImportSystemPopup popup = serviceProvider.GetService<ImportSystemPopup>();

        await popupNavigation.PushAsync(popup);

        popup.Disappearing += async (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            AtariConfiguration clone =
                await systemsService.Import(popup.ViewModel.FileName, popup.ViewModel.DisplayName);
            UpdateSystemsFromService();
            SelectedConfiguration = clone;
        };
    }

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
        await configFileService.Save(SelectedConfiguration);

        // TODO - Platform specific? is it File:// on PC it is on mac
        var app = preferencesService.Preferences.HatariApplication;
        var applicationUrl = $"file://{app}";
        log.LogInformation("Launching application: {Url}", applicationUrl);
        await Launcher.Default.OpenAsync(applicationUrl);
    }

    [RelayCommand]
    private Task Reordered()
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

    /// <summary>
    /// Updates the systems in the MainViewModel from the service.
    /// </summary>
    private void UpdateSystemsFromService()
    {
        Systems.Clear();
        int count = 0;
        foreach (var system in systemsService.All())
        {
            Systems.Add(system);
            count++;
        }

        NumberOfSystems = count;
    }


    /// <summary>
    /// Reorders the systems in the store based on their display order on screen.
    /// </summary>
    private void ReorderServicesFromDisplayOrder()
    {
        var ids = Systems.Select<AtariConfiguration, string>(s => s.Id).ToList();
        systemsService.ReorderByIds(ids);
    }


    private void TimerTick(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() => { SaveSystemsCommand.NotifyCanExecuteChanged(); });
    }

    #endregion
}