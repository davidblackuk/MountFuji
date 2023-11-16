namespace MyAtariCollection.Models;

public enum AtariMonitorType
{
    Mono,
    
    Rgb,
    
    Vga,
    
    Tv
}

public partial class AtariScreenOptions: ObservableObject
{
    [ObservableProperty] private bool showBorders = false;
    [ObservableProperty] private AtariMonitorType monitorType = AtariMonitorType.Mono;
}