// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

namespace MountFuji.Services.UpdatesService;

public interface IApplicationVersion
{
    /// <summary>
    /// Gets the current app version but the interface allows use to inject this in tests. AppInfo.Current throws
    /// a not implemented exception in a unit test assembly.
    /// </summary>
    Version Current { get; }
}

public class ApplicationVersion : IApplicationVersion
{
    /// <summary>
    /// Gets the current app version but the interface allows use to inject this in tests. AppInfo.Current throws
    /// a not implemented exception in a unit test assembly.
    /// </summary>
    public Version Current => new Version(1,0,2); // AppInfo.Version;
}