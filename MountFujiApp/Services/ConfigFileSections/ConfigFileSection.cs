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

using System.Globalization;
using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public class ConfigFileSection
{
    protected void AddSection(StringBuilder builder, string name)
    {
        builder.AppendLine($"[{name}]");
    }

    protected void AddFlag(StringBuilder builder, string flag, string value)
    {
        builder.AppendLine($"{flag} = {value}");
    }
    
    protected void AddFlag(StringBuilder builder, string flag, bool value)
    {
        builder.AppendLine($"{flag} = {value.ToString(CultureInfo.InvariantCulture).ToUpper()}");
    }

    protected void AddFlag(StringBuilder builder, string flag, Int32 value)
    {
        builder.AppendLine($"{flag} = {value.ToString(CultureInfo.InvariantCulture).ToUpper()}");
    }
    
    protected void AddDrive(StringBuilder builder, int driveIndex, string path)
    {
        bool inuse = !String.IsNullOrWhiteSpace(path);
        AddFlag(builder, $"bUseDevice{driveIndex}", inuse);
        AddFlag(builder, $"sDeviceFile{driveIndex}", path);
        AddFlag(builder, $"nBlockSize{driveIndex}", 512);
    }

    protected T ParseEnumValue<T>(string key, Dictionary<string, string> section)
        where T: struct    
    {
        return Enum.Parse<T>(section[key], true);
    }

    protected bool ParseBool(string key, Dictionary<string, string> section)
    {
        return Boolean.Parse(section[key]);
    }
    
    protected int ParseInt(string key, Dictionary<string, string> section)
    {
        return Int32.Parse(section[key]);
    }
    
    protected string ParseDrive(Dictionary<string, string> section, int driveIndex)
    {
        if (ParseBool($"bUseDevice{driveIndex}", section))
        {
            return section[$"sDeviceFile{driveIndex}"];
        }

        return String.Empty;
    }
}