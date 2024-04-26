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

namespace MountFuji.Converters;

public class CpuSummaryConverter: NaiveConverter, IMultiValueConverter
{
    public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string res = String.Empty;
        if (ValuesAllSet(values))
        {
            CpuType cpuType = (CpuType)values[0];
            CpuClock clock = (CpuClock)values[1];
            FpuType fpuType = (FpuType)values[2];

            res = $"cpu: {cpuType.ToString().Replace("MC", "")}";
            res = $"{res}@{clock.ToString().Replace("Clock", "")}";
            res = $"{res}, fpu: {fpuType.ToString().Replace("MC", "")}";
        }

        return res;
    }
}