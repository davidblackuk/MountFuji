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

namespace MountFuji.Services;

/// <summary>
/// We can't wire up the Application class for injection as it's created after the DI
/// container is built.
///
/// This interface allows us to wrap the static Application.Current with an injectable
/// resolver that uses the static method, BUT, can have the implementation swapped out
/// with another in a unit test
/// </summary>
public interface IApplicationResolver
{
    /// <summary>
    /// Gets the application.
    /// </summary>
    Application Application { get; }
}

/// <summary>
/// We can't wire up the Application class for injection as it's created after the DI
/// container is built.
///
/// This class allows us to wrap the static Application.Current with an injectable
/// resolver that uses the static method, BUT, can have the implementation swapped out
/// with another in a unit test
/// </summary>
public class ApplicationResolver : IApplicationResolver
{

    /// <summary>
    /// returns ether the static Application
    /// </summary>
    public  Application Application
    {
        get => Application.Current;

    }
    
}