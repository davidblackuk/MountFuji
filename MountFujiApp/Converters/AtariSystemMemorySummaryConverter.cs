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
using MountFuji.Extensions;

namespace MountFuji.Converters;


/// <summary>
/// Convert a system configuration into a descriptive 1 line summary
/// </summary>
public class AtariSystemMemorySummaryConverter: NaiveConverter, IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string res = "";
        if (ValuesAllSet(values))
        {
            AtariSystemType systemType= (AtariSystemType)values[0];
            int stMemory = (int)values[1];
            int ttMemory = (int)values[2];

            res = $"{systemType.DisplayText()}, ram: {stMemory} ";
            if (stMemory > 255)
            {
                res += "KB";
            }
            else
            {
                res += "MB";
            }
            
            if (ttMemory > 0)
            {
                res += $", tt ram: {ttMemory}MB";
            }
        }
        return res;
    }

   
}