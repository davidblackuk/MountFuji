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

public class HardDiskConfigFileSection : ConfigFileSection, IHardDiskConfigFileSection
{
    private const string BootFromHardDiskKey = "bBootFromHardDisk";
    private const string AtariHostFilenameConversionKey = "bFilenameConversion";
    private const string GemdosDriveKey = "nGemdosDrive";
    private const string WriteProtectionKey = "nWriteProtection";
    private const string UseHdDirectoryKey = "bUseHardDiskDirectory";
    private const string HardDiskDirectoryKey = "szHardDiskDirectory";

    public const string ConfigSectionName = "HardDisk";
    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);

        AddFlag(builder, GemdosDriveKey, config.GdosDriveOptions.AddGemdosAfterPhysicalDrives ? -1 : 0);
        AddFlag(builder, (string)BootFromHardDiskKey, (bool)config.GdosDriveOptions.BootFromHardDisk);
        AddFlag(builder, UseHdDirectoryKey, !string.IsNullOrEmpty(config.GdosDriveOptions.GemdosFolder));
        AddFlag(builder, (string)HardDiskDirectoryKey, (string)config.GdosDriveOptions.GemdosFolder);
        AddFlag(builder, WriteProtectionKey, (int) config.GdosDriveOptions.WriteProtection);
        AddFlag(builder, (string)AtariHostFilenameConversionKey, (bool)config.GdosDriveOptions.AtariHostFilenameConversion );

        //     AddFlag(builder, "nGemdosCase", );

        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];

        int gemdosDrive = ParseInt(GemdosDriveKey, section);
        to.GdosDriveOptions.AddGemdosAfterPhysicalDrives = gemdosDrive == -1;
        
        to.GdosDriveOptions.BootFromHardDisk = ParseBool(BootFromHardDiskKey, section);
        to.GdosDriveOptions.AtariHostFilenameConversion = ParseBool(AtariHostFilenameConversionKey, section);

        bool useGemDos = ParseBool(UseHdDirectoryKey, section);
        if (useGemDos)
        {
            to.GdosDriveOptions.GemdosFolder = section[HardDiskDirectoryKey];
        }
        
        to.GdosDriveOptions.WriteProtection = ParseEnumValue<DiskWriteProtection>(WriteProtectionKey, section);
    }
}