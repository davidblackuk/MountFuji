/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public class ScreenConfigFileSection: ConfigFileSection, IScreenConfigFileSection
{
    public const string ConfigSectionName = "Screen";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder,"nMonitorType", (int)config.ScreenOptions.MonitorType);
        AddFlag(builder,"nFrameSkips", (int)config.ScreenOptions.FrameSkip);
        AddFlag(builder,(string)"bFullScreen", (bool)config.ScreenOptions.FullScreen);
        AddFlag(builder,(string)"bAllowOverscan", (bool)config.ScreenOptions.ShowBorders);
        
        
        AddFlag(builder,(string)"bUseExtVdiResolutions", (bool)config.ScreenOptions.EnableExtendedResolutions);

        AddResolution(builder, config);
        
        AddFlag(builder,"nVdiColors", (int)config.ScreenOptions.ColourDepth);

        AddIndicators(builder, config);

        AddFlag(builder,(string)"bUseVsync", (bool)config.ScreenOptions.Vsync);
        AddFlag(builder,(string)"bResizable", (bool)config.ScreenOptions.Resizable);
        AddFlag(builder,(string)"bUseSdlRenderer", (bool)config.ScreenOptions.GpuScaling);

        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];
        to.ScreenOptions.MonitorType = ParseEnumValue<AtariMonitorType>("nMonitorType", section);
        to.ScreenOptions.FrameSkip = ParseEnumValue<FrameSkip>("nFrameSkips", section);
        to.ScreenOptions.FullScreen = ParseBool("bFullScreen", section);
        to.ScreenOptions.ShowBorders = ParseBool("bAllowOverscan", section);
        
        to.ScreenOptions.EnableExtendedResolutions = ParseBool("bUseExtVdiResolutions", section);
        
        // resolution
        to.ScreenOptions.ColourDepth = ParseEnumValue<ColorDepth>("nVdiColors", section);
        
        to.ScreenOptions.Vsync = ParseBool("bUseVsync", section);
        to.ScreenOptions.Resizable = ParseBool("bResizable", section);
        to.ScreenOptions.GpuScaling = ParseBool("bUseSdlRenderer", section);
        
        ParseIndicators(to, section);
    }

    private void ParseIndicators(AtariConfiguration to, Dictionary<string, string> section)
    {
        var statusBar = ParseBool("bShowStatusbar", section);
        var driveLed = ParseBool("bShowDriveLed", section);

        to.ScreenOptions.Indicators = statusBar switch
        {
            true when !driveLed => Indicators.StatusBar,
            false when driveLed => Indicators.DriveLed,
            _ => Indicators.None
        };
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