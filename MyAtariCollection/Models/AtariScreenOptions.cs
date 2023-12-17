namespace MyAtariCollection.Models;

public enum AtariMonitorType
{
    Mono = 0,
    
    Rgb =  1,
    
    Vga = 2,
    
    Tv = 3
}

public enum Resolution
{
    Resolution640X480,
    Resolution800X600,
    Resolution1024X768,
        
}

public enum ColorDepth
{
    Colours2 = 0,
    Colours4 = 1,
    Colours16 = 2
}

public enum Indicators
{
    StatusBar,
    DriveLed,
    None
}

public enum FrameSkip
{
    Off = 0, 
    Skip1 = 1,
    Skip2 = 2,
    Skip4 = 4,
    Auto = 5,
}

public partial class AtariScreenOptions: ObservableObject
{
    [ObservableProperty] private bool showBorders = false;
    [ObservableProperty] private bool falconTtAspectRatio = true;
    [ObservableProperty] private bool enableExtendedResolutions = false;
    [ObservableProperty] private bool fullScreen = false;

    [ObservableProperty] private bool gpuScaling = true;
    [ObservableProperty] private bool resizable = true;
    [ObservableProperty] private bool vsync = false;
    
    [ObservableProperty] private AtariMonitorType monitorType = AtariMonitorType.Mono;
    [ObservableProperty] private Resolution resolution = Resolution.Resolution640X480;
    [ObservableProperty] private ColorDepth colourDepth = ColorDepth.Colours16;
    [ObservableProperty] private Indicators indicators = Indicators.StatusBar;
    [ObservableProperty] private FrameSkip frameSkip = FrameSkip.Off;
}