namespace MountFuji.Models;

public partial class ApplicationPreferences: ObservableObject
{
    [ObservableProperty] public string hatariApplication;
    
    [ObservableProperty] public string hatariConfigFile;
    
    [ObservableProperty] public string romFolder;
    
    [ObservableProperty] public string cartridgeFolder;
    
    [ObservableProperty] public string hardDiskFolder;
    
    [ObservableProperty] public string gemDosFolder;
    
    [ObservableProperty] public string floppyDiskFolder;
    
}