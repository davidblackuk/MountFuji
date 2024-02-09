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
    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        
        AddFlag(builder, (string)AutoInsertDiskBKey, (bool)config.FloppyOptions.AutoInsertB);
        AddFlag(builder, (string)FastFloppyAccessKey, (bool)config.FloppyOptions.FastFloppyAccess);

        AddFlag(builder, (string)DriveAEnabledKey, (bool)config.FloppyOptions.DriveAEnabled);
        AddFlag(builder, (string)DriveAPathKey, (string)config.FloppyOptions.DriveAPath);
        AddFlag(builder, DriveANumberOfHeadsKey, config.FloppyOptions.DriveADoubleSided ? 2 : 1);
        
        
        
        AddFlag(builder, (string)DriveBEnabledKey, (bool)config.FloppyOptions.DriveBEnabled);
        AddFlag(builder, (string)DriveBPathKey, (string)config.FloppyOptions.DriveBPath);
        AddFlag(builder, DriveBNumberOfHeadsKey, config.FloppyOptions.DriveBDoubleSided ? 2 : 1);
        
        AddFlag(builder, WriteProtectionKey, (int) config.FloppyOptions.WriteProtection);
        
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