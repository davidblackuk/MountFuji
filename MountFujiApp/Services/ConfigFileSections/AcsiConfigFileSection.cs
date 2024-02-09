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

public class AcsiConfigFileSection : ConfigFileSection, IAcsiConfigFileSection
{
    public const string ConfigSectionName = "ACSI";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);


        AddDrive(builder, 0, config.AcsiImagePaths.Disk0);
        AddDrive(builder, 1, config.AcsiImagePaths.Disk1);
        AddDrive(builder, 2, config.AcsiImagePaths.Disk2);
        AddDrive(builder, 3, config.AcsiImagePaths.Disk3);
        AddDrive(builder, 4, config.AcsiImagePaths.Disk4);
        AddDrive(builder, 5, config.AcsiImagePaths.Disk5);
        AddDrive(builder, 6, config.AcsiImagePaths.Disk6);
        AddDrive(builder, 7, config.AcsiImagePaths.Disk7);
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        to.AcsiImagePaths.Disk0 = ParseDrive(sections[ConfigSectionName], 0 );
        to.AcsiImagePaths.Disk1 = ParseDrive(sections[ConfigSectionName], 1 );
        to.AcsiImagePaths.Disk2 = ParseDrive(sections[ConfigSectionName], 2 );
        to.AcsiImagePaths.Disk3 = ParseDrive(sections[ConfigSectionName], 3 );
        to.AcsiImagePaths.Disk4 = ParseDrive(sections[ConfigSectionName], 4 );
        to.AcsiImagePaths.Disk5 = ParseDrive(sections[ConfigSectionName], 5 );
        to.AcsiImagePaths.Disk6 = ParseDrive(sections[ConfigSectionName], 6 );
        to.AcsiImagePaths.Disk7 = ParseDrive(sections[ConfigSectionName], 7 );
    }



    
}




/*


filename is the zip file
zippath is within the zip file
   szDiskAZipPath =
   szDiskBZipPath =
   szDiskImageDirectory = /
   
*/