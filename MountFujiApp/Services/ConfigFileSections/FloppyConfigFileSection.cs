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

public class FloppyConfigFileSection : ConfigFileSection, IFloppyConfigFileSection
{
    private readonly IPreferencesService preferencesService;
    public const string ConfigSectionName = "Floppy";

    private const string AutoInsertDiskBKey = "bAutoInsertDiskB";
    private const string FastFloppyAccessKey = "FastFloppy";
    private const string DriveAEnabledKey = "EnableDriveA";
    private const string DriveBEnabledKey = "EnableDriveB";
    private const string DriveAPathKey = "szDiskAFileName";
    private const string DriveBPathKey = "szDiskBFileName";
    private const string DriveANumberOfHeadsKey = "DriveA_NumberOfHeads";
    private const string DriveBNumberOfHeadsKey = "DriveB_NumberOfHeads";
    private const string WriteProtectionKey = "nWriteProtection";
    private const string DefaultDiskImageKey = "szDiskImageDirectory";

    public FloppyConfigFileSection(IPreferencesService preferencesService)
    {
        this.preferencesService = preferencesService;
    }
    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        
        AddFlag(builder, AutoInsertDiskBKey, config.FloppyOptions.AutoInsertB);
        AddFlag(builder, FastFloppyAccessKey, config.FloppyOptions.FastFloppyAccess);

        AddFlag(builder, DriveAEnabledKey, config.FloppyOptions.DriveAEnabled);
        AddFlag(builder, DriveAPathKey, config.FloppyOptions.DriveAPath);
        AddFlag(builder, DriveANumberOfHeadsKey, config.FloppyOptions.DriveADoubleSided ? 2 : 1);
        
        
        
        AddFlag(builder, DriveBEnabledKey, config.FloppyOptions.DriveBEnabled);
        AddFlag(builder, DriveBPathKey, config.FloppyOptions.DriveBPath);
        AddFlag(builder, DriveBNumberOfHeadsKey, config.FloppyOptions.DriveBDoubleSided ? 2 : 1);
        
        AddFlag(builder, WriteProtectionKey, (int) config.FloppyOptions.WriteProtection);

        if (!String.IsNullOrEmpty(config.FloppyOptions.DriveAPath))
        {
            // if we have a floppy disk specified, set the default floppy folder to the containing folder
            string floppyFolder = Path.GetDirectoryName(config.FloppyOptions.DriveAPath);
            AddFlag(builder, DefaultDiskImageKey , floppyFolder);
        }
        else
        {
            // if we don't have a floppy specified, set the default folder to the preferences floppy folder
            AddFlag(builder, DefaultDiskImageKey , preferencesService.Preferences.FloppyDiskFolder);
        }
        
        
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];
        
        to.FloppyOptions.AutoInsertB = ParseBool(AutoInsertDiskBKey, section);
        to.FloppyOptions.FastFloppyAccess = ParseBool(FastFloppyAccessKey, section);
        to.FloppyOptions.WriteProtection = ParseEnumValue<DiskWriteProtection>(WriteProtectionKey, section);
        
        to.FloppyOptions.DriveAEnabled = ParseBool(DriveAEnabledKey, section);
        to.FloppyOptions.DriveAPath = section[DriveAPathKey];
        var numberOfHeadsA = ParseInt(DriveANumberOfHeadsKey, section);
        to.FloppyOptions.DriveADoubleSided = numberOfHeadsA == 2 ? true : false;
        
        to.FloppyOptions.DriveBEnabled = ParseBool(DriveBEnabledKey, section);
        to.FloppyOptions.DriveBPath = section[DriveBPathKey];
        var numberOfHeadsB = ParseInt(DriveBNumberOfHeadsKey, section);
        to.FloppyOptions.DriveBDoubleSided = numberOfHeadsB == 2 ? true : false;
        
    }
}