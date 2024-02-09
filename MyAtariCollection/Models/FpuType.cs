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
/// Floating point unit type 
/// </summary>
public enum FpuType
{
    /// <summary>
    /// ST/STE + Mega varieties did not come with an FPU in the case
    /// </summary>
    None = 0,
    
    MC68881 = 68881,
    
    /// <summary>
    /// Included by default for TT computers
    /// </summary>
    MC68882 = 68882,
    
    Internal = 68040
}