using CommunityToolkit.Mvvm.ComponentModel;

namespace MountFuji.Models;

public partial class SectionVisibility: ObservableObject
{
    [ObservableProperty] private bool expandSystemSection = true;
    
    [ObservableProperty] private bool expandCpuSection = true;

    [ObservableProperty] private bool expandRomSection = true;
    
    [ObservableProperty] private bool expandAcsiHddSection = false;
    
    [ObservableProperty] private bool expandFloppySection = false;
    
    [ObservableProperty] private bool expandScreenSection = false;
    
    [ObservableProperty] private bool expandSoundSection = false;
}