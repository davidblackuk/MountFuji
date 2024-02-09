namespace MountFuji.ViewModels;

public partial class AboutPopupViewModel: TinyViewModel
{
    private readonly IPersistance persistance;
    private readonly IPopupNavigation popupNavigation;
    
    public string Version => AppInfo.VersionString;
    public string BuildInfo => AppInfo.BuildString;
    public string MountFujiFolder => persistance.MountFujiFolder;
    
    public string P => Path.Combine(FileSystem.AppDataDirectory, "fuji");
    
    public AboutPopupViewModel(IPopupNavigation popupNavigation, IPersistance persistance)
    {
        this.popupNavigation = popupNavigation;
        this.persistance = persistance;
    }
    
    [RelayCommand]
    private async Task Close()
    {
        await popupNavigation.PopAsync();
    }
}

