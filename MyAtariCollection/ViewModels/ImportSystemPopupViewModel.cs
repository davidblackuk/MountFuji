namespace MountFuji.ViewModels;

public partial class ImportSystemPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;
    private readonly IFujiFilePickerService fujiFilePicker;

    public bool Confirmed { get; set; }
    
    [ObservableProperty] AtariConfiguration system;

    [NotifyCanExecuteChangedFor(nameof(MountFuji.ViewModels.ImportSystemPopupViewModel.OkCommand))]
    [ObservableProperty] private string displayName;

    [NotifyCanExecuteChangedFor(nameof(MountFuji.ViewModels.ImportSystemPopupViewModel.OkCommand))]
    [ObservableProperty] private string fileName;

    private string hatariConfigFilePath = String.Empty;
    
    public ImportSystemPopupViewModel(IPopupNavigation popupNavigation,  IFujiFilePickerService fujiFilePicker, IPreferencesService preferences)
    {
        this.popupNavigation = popupNavigation;
        this.fujiFilePicker = fujiFilePicker;

        this.hatariConfigFilePath = Path.GetDirectoryName((string)preferences.Preferences.HatariConfigFile);
    }
    
    
    [RelayCommand]
    private async Task Cancel()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(OkEnabled))]
    private async Task Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async Task BrowseHatariConfigFile()
    {
        await fujiFilePicker.PickFile("Pick a config to import", 
            (filename) => {
                FileName = filename;
                OkCommand.NotifyCanExecuteChanged();
            },
            hatariConfigFilePath);
    }
    
    [RelayCommand]
    private void ClearHatariConfigFile()
    {
        FileName = String.Empty;
    }
    
    private bool OkEnabled()
    {
        return !string.IsNullOrWhiteSpace(DisplayName) && !string.IsNullOrEmpty(FileName);
    }
    
}