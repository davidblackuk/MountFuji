namespace MyAtariCollection.Services.ConfigFileSections;

public class ScreenConfigFileSection: ConfigFileSection, IScreenConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Screen");
        
        AddFlag(builder,"nMonitorType", (int)config.ScreenOptions.MonitorType);
        AddFlag(builder,"nFrameSkips", (int)config.ScreenOptions.FrameSkip);
        AddFlag(builder,"bFullScreen", config.ScreenOptions.FullScreen);
        AddFlag(builder,"bAllowOverscan", config.ScreenOptions.ShowBorders);
        
        
        AddFlag(builder,"bUseExtVdiResolutions", config.ScreenOptions.EnableExtendedResolutions);

        AddResolution(builder, config);
        
        AddFlag(builder,"nVdiColors", (int)config.ScreenOptions.ColourDepth);

        AddIndicators(builder, config);

        AddFlag(builder,"bUseVsync", config.ScreenOptions.Vsync);
        AddFlag(builder,"bResizable", config.ScreenOptions.Resizable);
        AddFlag(builder,"bUseSdlRenderer", config.ScreenOptions.GpuScaling);

    }

    private void AddResolution(StringBuilder builder, AtariConfiguration config)
    {
        switch (config.ScreenOptions.Resolution)
        {
            case Resolution.Resolution640X480:
                AddFlag(builder,"nVdiWidth", 640);
                AddFlag(builder,"nVdiHeight", 480);
                break;
            case Resolution.Resolution800X600:
                AddFlag(builder,"nVdiWidth", 800);
                AddFlag(builder,"nVdiHeight", 600);
                break;
            case Resolution.Resolution1024X768:
                AddFlag(builder,"nVdiWidth", 1024);
                AddFlag(builder,"nVdiHeight", 768);
                break;
        }
    }

    private void AddIndicators(StringBuilder builder, AtariConfiguration config)
    {
        switch (config.ScreenOptions.Indicators)
        {
            case Indicators.StatusBar:
                AddFlag(builder,"bShowStatusbar", true);
                AddFlag(builder,"bShowDriveLed", false);
                break;
            case Indicators.DriveLed:
                AddFlag(builder,"bShowStatusbar", false);
                AddFlag(builder,"bShowDriveLed", true);
                break;
            case Indicators.None:
                AddFlag(builder,"bShowStatusbar", false);
                AddFlag(builder,"bShowDriveLed", false);
                break;
        }
    }
}

/*
 

   nSpec512Threshold = 1
   nForceBpp = 0
   bMouseWarp = TRUE

   TT/Falcon stuff add in v2

   bCrop = FALSE
   bForceMax = FALSE
   nZoomFactor = 1

   
   bKeepResolution = TRUE
   nMaxWidth = 832
   nMaxHeight = 588
*/