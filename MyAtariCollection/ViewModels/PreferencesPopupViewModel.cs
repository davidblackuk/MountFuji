using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;
using MyAtariCollection.Services.Filesystem;

namespace MyAtariCollection.ViewModels;

public partial class PreferencesPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;
    private readonly IFujiFilePickerService fujiFilePicker;

    private string lastUsedFolder;

    public PreferencesPopupViewModel(IPreferencesService preferencesService, IPopupNavigation popupNavigation, 
        IFujiFilePickerService fujiFilePicker)
    {
        this.popupNavigation = popupNavigation;
        this.fujiFilePicker = fujiFilePicker;

        Preferences = preferencesService.Preferences;
    }

    public bool Confirmed { get; set; } 
    
    [ObservableProperty] 
    private ApplicationPreferences preferences;

    
    [RelayCommand]
    private async Task Cancel()
    {
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(HasValidData))]
    private async Task Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

    [RelayCommand()]
    private async Task BrowseRomFolder()
    {
        await fujiFilePicker.PickFolder("Default ROM Folder", (filename) => Preferences.RomFolder = filename,
            GetInitialFolder(Preferences.RomFolder));
    }

    private string GetInitialFolder(string preferencesRomFolder)
    {
        return preferencesRomFolder;
    }

    [RelayCommand()]
    private void ClearRomFolder() => Preferences.RomFolder = string.Empty;


  
    [RelayCommand()]
    private async Task BrowseCartridgeFolder()
    {
        await fujiFilePicker.PickFolder("Default Cartridge Folder", (filename) => Preferences.CartridgeFolder = filename,
            lastUsedFolder);
    }
  
    [RelayCommand()]
    private void ClearCartridgeFolder() => Preferences.CartridgeFolder = string.Empty;

    
    [RelayCommand()]
    private async Task BrowseFloppyDiskFolder()
    {
        await fujiFilePicker.PickFolder("Default Floppies Folder", (filename) => Preferences.FloppyDiskFolder = filename,
            lastUsedFolder);
    }
  
    [RelayCommand()]
    private void ClearFloppyDiskFolder() => Preferences.FloppyDiskFolder = string.Empty;

    
    [RelayCommand()]
    private async Task BrowseHardDiskFolder()
    {
        await fujiFilePicker.PickFolder("Default Hard Drive Image Folder", (filename) => Preferences.HardDiskFolder = filename,
            lastUsedFolder);
    }
  
    [RelayCommand()]
    private void ClearHardDiskFolder() => Preferences.HardDiskFolder = string.Empty;
    

    [RelayCommand()]
    private async Task BrowseGemDosDiskFolder()
    {
        await fujiFilePicker.PickFolder("Default GEMDOS Folder", (filename) => Preferences.GemDosFolder = filename,
            lastUsedFolder);
    }
  
    [RelayCommand()]
    private void ClearGemDosDiskFolder() => Preferences.GemDosFolder = string.Empty;
  
    [RelayCommand()]
    private async Task BrowseHatariApp()
    {
        await fujiFilePicker.PickFile("Hatari Executable", 
            (filename) => Preferences.HatariApplication = filename,
            lastUsedFolder);
        

        OkCommand.NotifyCanExecuteChanged();
    }
  
    [RelayCommand()]
    private void ClearHatariApp()
    {
        Preferences.HatariApplication = string.Empty;
        OkCommand.NotifyCanExecuteChanged();
    }
    
    [RelayCommand()]
    private async Task BrowseHatariConfigFile()
    {
        await fujiFilePicker.PickFile("Hatari Config file", 
            (filename) => Preferences.HatariConfigFile = filename,
            lastUsedFolder);
        

        OkCommand.NotifyCanExecuteChanged();
    }
  
    [RelayCommand()]
    private void ClearHatariConfigFile()
    {
        Preferences.HatariConfigFile = string.Empty;
        OkCommand.NotifyCanExecuteChanged();
    }
    
    
    private bool HasValidData()
    {
        return !String.IsNullOrWhiteSpace(Preferences.HatariApplication);
    }
    
}