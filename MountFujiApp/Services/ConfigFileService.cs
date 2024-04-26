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
using Microsoft.Extensions.Logging;
using MountFuji.Services.ConfigFileSections;

namespace MountFuji.Services;

public class ConfigFileService : IConfigFileService
{
    private readonly IMemoryConfigFileSection memoryConfig;
    private readonly ISystemConfigFileSection systemConfig;
    private readonly IRomConfigFileSection romConfig;
    private readonly IAcsiConfigFileSection acsiConfig;
    private readonly IScsiConfigFileSection scsiConfig;
    private readonly IIdeConfigFileSection ideConfig;
    private readonly IHardDiskConfigFileSection hardDiskConfig;
    private readonly IFloppyConfigFileSection floppyConfig;
    private readonly IScreenConfigFileSection screenConfig;
    private readonly ISoundConfigFileSection soundConfig;
    private readonly IKeyboardConfigFileSection keyboardConfig;
    private readonly IPreferencesService preferencesService;
    private readonly IRawHatariConfigFile rawFileReader;
    private readonly IGlobalSystemConfigurationService globalConfig;
    private readonly ILogger<ConfigFileService> log;
    private readonly ILogConfigFileSection logConfig;

    public ConfigFileService(ILogConfigFileSection logConfig,
        IMemoryConfigFileSection memoryConfig, ISystemConfigFileSection systemConfig, IRomConfigFileSection romConfig,
        IAcsiConfigFileSection acsiConfig, IScsiConfigFileSection scsiConfig, IIdeConfigFileSection ideConfig,
        IHardDiskConfigFileSection hardDiskConfig, IFloppyConfigFileSection floppyConfig,
        IScreenConfigFileSection screenConfig,
        ISoundConfigFileSection soundConfig,
        IKeyboardConfigFileSection keyboardConfig,
        IPreferencesService preferencesService,
        IRawHatariConfigFile rawFileReader,
        IGlobalSystemConfigurationService globalConfig,
        ILogger<ConfigFileService> log)
    {
        this.memoryConfig = memoryConfig;
        this.systemConfig = systemConfig;
        this.romConfig = romConfig;
        this.acsiConfig = acsiConfig;
        this.scsiConfig = scsiConfig;
        this.ideConfig = ideConfig;
        this.hardDiskConfig = hardDiskConfig;
        this.floppyConfig = floppyConfig;
        this.screenConfig = screenConfig;
        this.soundConfig = soundConfig;
        this.keyboardConfig = keyboardConfig;
        this.preferencesService = preferencesService;
        this.rawFileReader = rawFileReader;
        this.globalConfig = globalConfig;
        this.log = log;
        this.logConfig = logConfig;
    }

    private string ToHatariConfig(AtariConfiguration from)
    {
        StringBuilder builder = new StringBuilder();

        keyboardConfig.ToHatariConfig(builder, globalConfig.Configuration);
        systemConfig.ToHatariConfig(builder, from);
        memoryConfig.ToHatariConfig(builder, from);
        romConfig.ToHatariConfig(builder, from);
        acsiConfig.ToHatariConfig(builder, from);
        scsiConfig.ToHatariConfig(builder, from);
        ideConfig.ToHatariConfig(builder, from);
        hardDiskConfig.ToHatariConfig(builder, from);
        floppyConfig.ToHatariConfig(builder, from);

        soundConfig.ToHatariConfig(builder, from);
        screenConfig.ToHatariConfig(builder, from);


        logConfig.ToHatariConfig(builder, from);

        return builder.ToString();
    }


    private AtariConfiguration FromHatariConfig()
    {
        AtariConfiguration to = new AtariConfiguration();


        foreach (var section in rawFileReader.Sections.Keys)
        {
            switch (section)
            {
                case SystemConfigFileSection.ConfigSectionName:
                    systemConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case MemoryConfigFileSection.ConfigSectionName:
                    memoryConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case RomConfigFileSection.ConfigSectionName:
                    romConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case AcsiConfigFileSection.ConfigSectionName:
                    acsiConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case ScsiConfigFileSection.ConfigSectionName:
                    scsiConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case IdeConfigFileSection.ConfigSectionName:
                    ideConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case HardDiskConfigFileSection.ConfigSectionName:
                    hardDiskConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case FloppyConfigFileSection.ConfigSectionName:
                    floppyConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case SoundConfigFileSection.ConfigSectionName:
                    soundConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case ScreenConfigFileSection.ConfigSectionName:
                    screenConfig.FromHatariConfig(to, rawFileReader.Sections);
                    break;
                case KeyboardConfigFileSection.KeyboardConfigSectionName:
                case KeyboardConfigFileSection.KeyboardShortcutsWithModSectionName:
                case KeyboardConfigFileSection.KeyboardShortcutsWithoutModSectionName:
                    // these are  imported via the Global system config service, but put here to avoid a warning below
                    break;
                
                case "Log":
                case "Debugger":
                case "RS232":
                case "Printer":
                case "Midi":
                case "Video":
                case "Joystick0":
                case "Joystick1":
                case "Joystick2":
                case "Joystick3":
                case "Joystick4":
                case "Joystick5":
                case "LILO":

                    // listed here to show explicitly what we don't support at the moment
                    break;
                default:
                    log.LogWarning("Skipping config section: {Section}", section);
                    break;
            }
        }
        
        
        return to;
    }

    public async Task<AtariConfiguration> Load(string path)
    {
        try
        {
            await rawFileReader.Read(path);
            var res = FromHatariConfig();
            return res;
        }
        catch (Exception e)
        {
            log.LogError(e, "Error loading Hatari config");
            throw;
        }
    }


    public async Task Save(AtariConfiguration config)
    {
        var configFileContent = ToHatariConfig(config);
        log.LogInformation("Overwriting Hatari config at: {File}", preferencesService.Preferences.HatariConfigFile);
        await File.WriteAllTextAsync(preferencesService.Preferences.HatariConfigFile,
            configFileContent);
    }
}