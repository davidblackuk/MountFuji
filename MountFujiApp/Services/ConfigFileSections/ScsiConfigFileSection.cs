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

public class ScsiConfigFileSection : ConfigFileSection, IScsiConfigFileSection
{
    public const string ConfigSectionName = "SCSI";

    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddDrive(builder, 0, config.ScsiImagePaths.Disk0);
        AddDrive(builder, 1, config.ScsiImagePaths.Disk1);
        AddDrive(builder, 2, config.ScsiImagePaths.Disk2);
        AddDrive(builder, 3, config.ScsiImagePaths.Disk3);
        AddDrive(builder, 4, config.ScsiImagePaths.Disk4);
        AddDrive(builder, 5, config.ScsiImagePaths.Disk5);
        AddDrive(builder, 6, config.ScsiImagePaths.Disk6);
        AddDrive(builder, 7, config.ScsiImagePaths.Disk7);
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        to.ScsiImagePaths.Disk0 = ParseDrive(sections[ConfigSectionName], 0 );
        to.ScsiImagePaths.Disk1 = ParseDrive(sections[ConfigSectionName], 1 );
        to.ScsiImagePaths.Disk2 = ParseDrive(sections[ConfigSectionName], 2 );
        to.ScsiImagePaths.Disk3 = ParseDrive(sections[ConfigSectionName], 3 );
        to.ScsiImagePaths.Disk4 = ParseDrive(sections[ConfigSectionName], 4 );
        to.ScsiImagePaths.Disk5 = ParseDrive(sections[ConfigSectionName], 5 );
        to.ScsiImagePaths.Disk6 = ParseDrive(sections[ConfigSectionName], 6 );
        to.ScsiImagePaths.Disk7 = ParseDrive(sections[ConfigSectionName], 7);
    }
}