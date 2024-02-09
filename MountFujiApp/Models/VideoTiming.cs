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
/// Wakeup State for MMU/GLUE (x=ws1/ws2/ws3/ws4/random, default ws3). When powering on, the STF will randomly
/// choose one of these wake up states. The state will then affect the timings where border removals and other
/// video tricks should be made, which can give different results on screen. For example, WS3 is known to be
/// compatible with many demos, while WS1 can show more problems.
/// </summary>
public enum VideoTiming
{
    /// <summary>
    /// Pick a randon wake state
    /// </summary>
    Random = 0,
    
    /// <summary>
    /// Wake state 1
    /// </summary>
    One = 1,
    
    /// <summary>
    /// Wake state 2, this is the default.
    /// </summary>
    Two = 2,
    
    /// <summary>
    /// Wake state 3
    /// </summary>
    Three = 3,
    
    /// <summary>
    /// Wake state 4
    /// </summary>
    Four = 4
}