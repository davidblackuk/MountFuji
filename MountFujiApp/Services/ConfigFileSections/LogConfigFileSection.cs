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

public class LogConfigFileSection: ConfigFileSection, ILogConfigFileSection
{
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Log");
        
        AddFlag(builder, "sLogFileName", "stderr");
        AddFlag(builder, "sTraceFileName", "stderr");
        AddFlag(builder, "nTextLogLevel", "3");
        AddFlag(builder, "nAlertDlgLogLevel", "1");
        AddFlag(builder, "bConfirmQuit", true);
        AddFlag(builder, "bNatFeats", false);
        AddFlag(builder, "bConsoleWindow", false);
        
        builder.AppendLine();
    }
}