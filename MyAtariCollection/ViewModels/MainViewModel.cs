using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Storage;


namespace MyAtariCollection.ViewModels;

public partial class MainViewModel : TinyViewModel
{
    private readonly ISystemsService systemService;
    private readonly IConfigFileService configFileService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IFilePicker filePicker;
    private readonly IFolderPicker folderPicker;
    private readonly IServiceProvider serviceProvider;
    private readonly IPreferencesService preferencesService;
    private readonly Random random = new();

    public MainViewModel(ISystemsService systemService, IConfigFileService configFileService, 
        IPopupNavigation popupNavigation, IFilePicker filePicker, IFolderPicker folderPicker, 
        IServiceProvider serviceProvider, IPreferencesService preferencesService)
    {
        this.systemService = systemService;
        this.configFileService = configFileService;
        this.popupNavigation = popupNavigation;
        this.filePicker = filePicker;
        this.folderPicker = folderPicker;
        this.serviceProvider = serviceProvider;
        this.preferencesService = preferencesService;
    }


    [ObservableProperty] 
    private ObservableCollection<AtariConfiguration> systems = new();

    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(HasSelectedConfig))]
    private AtariConfiguration selectedConfiguration;
    
    [ObservableProperty]
    private SectionVisibility sectionVisibility = new SectionVisibility(); 

    public bool HasSelectedConfig => SelectedConfiguration != null;
    

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

    
    
    [RelayCommand()]
    private async void BrowseGemdosFolder()
    {
        var folder = await folderPicker.PickAsync();
        
        if (folder.IsSuccessful) SelectedConfiguration.GdosDriveOptions.GemdosFolder = folder.Folder.Path;
    }
    
    [RelayCommand()]
    private void ClearGemdosFolder(int diskId) => SelectedConfiguration.GdosDriveOptions.GemdosFolder = string.Empty;



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

    [RelayCommand]
    private async void EditPreferences()
    {
        var popup = serviceProvider.GetService<PreferencesPopup>();
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += async (sender, args) =>
        {
             if (popup.ViewModel.Confirmed)
             {
                await preferencesService.Save();
             }
        };      
    }
 

    [RelayCommand()]
    private async void Run()
    {
       
        var configFileContent = configFileService.Generate(SelectedConfiguration);
        Console.WriteLine($"\n{configFileContent}");

        await File.WriteAllTextAsync("/Users/davidblack/Library/Application Support/Hatari/hatari.cfg", configFileContent);
        await Launcher.Default.OpenAsync("file:///Applications/Hatari.app?--args%20--machine%20falcon");
     
    }

    #endregion
}