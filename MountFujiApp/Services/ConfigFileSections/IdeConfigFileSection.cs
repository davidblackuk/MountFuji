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

public class IdeConfigFileSection : ConfigFileSection, IIdeConfigFileSection
{
    private const string ByteSwapDrive0Key = "nByteSwap0";
    private const string ByteSwapDrive1Key = "nByteSwap1";
    public const string ConfigSectionName = "IDE";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);


        AddDrive(builder, 0, config.IdeOptions.Disk0);
        AddFlag(builder, ByteSwapDrive0Key, (int)config.IdeOptions.ByteSwapDrive0);

        AddDrive(builder, 1, config.IdeOptions.Disk1);
        AddFlag(builder, ByteSwapDrive1Key, (int)config.IdeOptions.ByteSwapDrive1); 

        builder.AppendLine();    
        
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        to.IdeOptions.Disk0 = ParseDrive(sections[ConfigSectionName], 0 );
        to.IdeOptions.Disk1 = ParseDrive(sections[ConfigSectionName], 1 );

        to.IdeOptions.ByteSwapDrive0 = ParseEnumValue<IdeByteSwap>(ByteSwapDrive0Key, sections[ConfigSectionName]);
        to.IdeOptions.ByteSwapDrive1 = ParseEnumValue<IdeByteSwap>(ByteSwapDrive1Key, sections[ConfigSectionName]);
    }
}