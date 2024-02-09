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

namespace MountFuji.Models;

/// <summary>
///  The the speed we clock at
/// </summary>
public enum CpuClock
{
    /// <summary>
    /// Stock speed of ST, STE, pluse mega varieties
    /// </summary>
    Clock8Mhz = 8,
    
    /// <summary>
    /// Stock Falcon speed
    /// </summary>
    Clock16Mhz = 16,
    
    /// <summary>
    /// Stock TT speed
    /// </summary>
    Clock32Mhz = 32,
}