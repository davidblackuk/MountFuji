using CommunityToolkit.Maui.Storage;

namespace MyAtariCollection.ViewModels;

public partial class PreferencesPopupViewModel: TinyViewModel
{
    private readonly IPreferencesService preferencesService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IFolderPicker folderPicker;
    private readonly IFilePicker filePicker;


    public PreferencesPopupViewModel(IPreferencesService preferencesService, IPopupNavigation popupNavigation, 
        IFolderPicker folderPicker, IFilePicker filePicker)
    {
        this.preferencesService = preferencesService;
        this.popupNavigation = popupNavigation;
        this.folderPicker = folderPicker;
        this.filePicker = filePicker;

        Preferences = preferencesService.Preferences;
    }

    public bool Confirmed { get; set; } 
    
    [ObservableProperty] 
    private ApplicationPreferences preferences;

    
    [RelayCommand]
    private async void Cancel()
    {
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(HasValidData))]
    private async void Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

    [RelayCommand()]
    private async void BrowseRomFolder()
    {
        var folder = await PickFolder(Preferences.RomFolder);
        if (folder.IsSuccessful) Preferences.RomFolder = folder.Folder.Path;
    }
    
    [RelayCommand()]
    private void ClearRomFolder() => Preferences.RomFolder = string.Empty;


  
    [RelayCommand()]
    private async void BrowseCartridgeFolder()
    {
        var folder = await PickFolder(Preferences.CartridgeFolder);
        
        if (folder.IsSuccessful) Preferences.CartridgeFolder = folder.Folder.Path;
    }
  
    [RelayCommand()]
    private void ClearCartridgeFolder() => Preferences.CartridgeFolder = string.Empty;

    
    [RelayCommand()]
    private async void BrowseFloppyDiskFolder()
    {
        var folder = await PickFolder(Preferences.FloppyDiskFolder);

        
        if (folder.IsSuccessful) Preferences.FloppyDiskFolder = folder.Folder.Path;
    }
  
    [RelayCommand()]
    private void ClearFloppyDiskFolder() => Preferences.FloppyDiskFolder = string.Empty;

    
    [RelayCommand()]
    private async void BrowseHardDiskFolder()
    {
        var folder = await PickFolder(Preferences.HardDiskFolder);

        
        if (folder.IsSuccessful) Preferences.HardDiskFolder = folder.Folder.Path;
    }
  
    [RelayCommand()]
    private void ClearHardDiskFolder() => Preferences.HardDiskFolder = string.Empty;
    

    [RelayCommand()]
    private async void BrowseGemDosDiskFolder()
    {
        var folder = await PickFolder(Preferences.GemDosFolder);
        
        if (folder.IsSuccessful) Preferences.GemDosFolder = folder.Folder.Path;
    }
  
    [RelayCommand()]
    private void ClearGemDosDiskFolder() => Preferences.GemDosFolder = string.Empty;
  
    [RelayCommand()]
    private async void BrowseHatariApp()
    {
        
        var file = await filePicker.PickAsync();

        if (file != null)
        {
            Preferences.HatariApplication = file.FullPath;
            
        }
        OkCommand.NotifyCanExecuteChanged();
    }
  
    [RelayCommand()]
    private void ClearHatariApp()
    {
        Preferences.HatariApplication = string.Empty;
        OkCommand.NotifyCanExecuteChanged();
    }


    /// <summary>
    /// Picks a folder, the passed initial folder value is used as the CWD of
    /// the picker unless it's empty. Then the picker is initialized to the
    /// users directory for the current OS
    /// <para>/home/user on unix and</para>
    /// <para>c:\users\user on windows</para>
    /// 
    /// </summary>
    /// <param name="initialFolder">THe initial folder to open the picker in</param>
    /// <returns></returns>
    private  Task<FolderPickerResult> PickFolder(string initialFolder)
    {
        if (String.IsNullOrWhiteSpace(initialFolder))
        {
            initialFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        return folderPicker.PickAsync(initialFolder);
    }

    

    private bool HasValidData()
    {
        return !String.IsNullOrWhiteSpace(Preferences.HatariApplication);
    }
    
}