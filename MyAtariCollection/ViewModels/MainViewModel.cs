using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Storage;


namespace MyAtariCollection.ViewModels;

public partial class MainViewModel : TinyViewModel
{
    private readonly ISystemsService systemService;
    private readonly IConfigFileService configFileService;
    private readonly ICommandLineOptionsService optionsService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IFilePicker filePicker;
    private readonly IServiceProvider serviceProvider;
    private readonly Random random = new();

    public MainViewModel(ISystemsService systemService, IConfigFileService configFileService, ICommandLineOptionsService optionsService, IPopupNavigation popupNavigation, IFilePicker filePicker, IServiceProvider serviceProvider)
    {
        this.systemService = systemService;
        this.configFileService = configFileService;
        this.optionsService = optionsService;
        this.popupNavigation = popupNavigation;
        this.filePicker = filePicker;
        this.serviceProvider = serviceProvider;
    }


    [ObservableProperty] 
    private ObservableCollection<AtariConfiguration> systems = new();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(HasSelectedConfig))]
    private AtariConfiguration selectedConfiguration;
    
    [ObservableProperty]
    private SectionVisibility sectionVisibility = new SectionVisibility(); 

    public bool HasSelectedConfig => SelectedConfiguration != null;


    private int nextSystem = 0;

    #region ---- RELAY COMMANDS ----
    
        
    [RelayCommand()]
    private async void BrowseRoms()
    {
        var file = await filePicker.PickAsync();
        if (file != null)
        {
            SelectedConfiguration.RomImage = file.FullPath;
        }
    }
    [RelayCommand]
    private void ClearRom()
    {
        SelectedConfiguration.RomImage = String.Empty;
    }
    [RelayCommand()]
    private async void BrowseCartridges()
    {
        var file = await filePicker.PickAsync();
        if (file != null)
        {
            SelectedConfiguration.CartridgeImage = file.FullPath;
        }
    }
    [RelayCommand]
    private void ClearCartridge()
    {
        SelectedConfiguration.CartridgeImage = String.Empty;
    }
    
    #region disk image  operations
    
    [RelayCommand()]
    private async void BrowseAcsiDiskImage(int diskId)
    {
        var file = await filePicker.PickAsync();

        if (file != null)
        {
            SelectedConfiguration.AcsiImagePaths.SetImagePath(diskId, file.FullPath);
        }
    }
    
    [RelayCommand()]
    private void ClearAcsiDiskImage(int diskId) => SelectedConfiguration.AcsiImagePaths.ClearImagePath(diskId);


    [RelayCommand()]
    private async void BrowseScsiDiskImage(int diskId)
    {
        var file = await filePicker.PickAsync();
        
        if (file != null) SelectedConfiguration.ScsiImagePaths.SetImagePath(diskId, file.FullPath);
    }
    
    [RelayCommand()]
    private void ClearScsiDiskImage(int diskId) => SelectedConfiguration.ScsiImagePaths.ClearImagePath(diskId);

    [RelayCommand()]
    private async void BrowseIdeDiskImage(int diskId)
    {
        var file = await filePicker.PickAsync();
        
        if (file != null) SelectedConfiguration.IdeOptions.SetImagePath(diskId, file.FullPath);
    }
    
    [RelayCommand()]
    private void ClearIdeDiskImage(int diskId) => SelectedConfiguration.IdeOptions.ClearImagePath(diskId);

    [RelayCommand()]
    private async void BrowseFloppyDiskImage(int diskId)
    {
        var file = await filePicker.PickAsync();

        if (file != null) SelectedConfiguration.FloppyOptions.SetImagePath(diskId, file.FullPath);
    }

    [RelayCommand()]
    private void ClearFloppyDiskImage(int diskId) => SelectedConfiguration.FloppyOptions.ClearImagePath(diskId);




    #endregion

    [RelayCommand]
    private async void CreateNewSystem()
    {
        var popup = serviceProvider.GetService<NewSystemPopup>();
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (popup.ViewModel.Confirmed)
            {
                var system = popup.ViewModel.GetConfiguration();
                Systems.Add(system);
                SelectedConfiguration = system;
            }
        };      
    }

 

    [RelayCommand()]
    private async void Run()
    {
        Console.WriteLine("open /Applications/Hatari.app --args " + optionsService.Generate(SelectedConfiguration));

        var configFileContent = configFileService.Generate(SelectedConfiguration);
        Console.WriteLine($"\n{configFileContent}");

        await File.WriteAllTextAsync("/Users/davidblack/Library/Application Support/Hatari/hatari.cfg", configFileContent);
        await Launcher.Default.OpenAsync("file:///Applications/Hatari.app?--args%20--machine%20falcon");
     
    }

    #endregion
}