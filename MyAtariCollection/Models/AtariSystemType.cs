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
/// Main Atari System types supported by Hatari. At the time of creation the STacy
/// is not a supported platform. it would be cool if it were, though of use to
/// no one other tan hard core ST enthusiasts :-)
/// </summary>
public enum AtariSystemType
{
    /// <summary>
    /// Atari ST, STF or STFM
    /// </summary>
    ST = 0,
    
    /// <summary>
    /// Mega St and ST in a box
    /// </summary>
    MegaST = 1,
    
    /// <summary>
    /// STE, more palette colors, a blitter and DMA access, a better ST
    /// </summary>
    STE = 2,
    
    /// <summary>
    /// Mega STE in a box
    /// </summary>
    MegaSTE = 3,
    
    /// <summary>
    /// UNix workstation compatible beast
    /// </summary>
    TT = 4,
    
    /// <summary>
    /// Falcon030: The last of it's line. If you're a billionaire you can own one of these
    /// </summary>
    Falcon = 5
}