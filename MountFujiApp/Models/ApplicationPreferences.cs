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


using System.Text.Json.Serialization;

namespace MountFuji.Models;

/// <summary>
/// The users application preferences. Mostly files and folder path at the moment,
/// but also the runtime theme
/// </summary>
public partial class ApplicationPreferences: ObservableObject
{
    /// <summary>
    /// The location of the Hatari Application. On windows this will be a .exe file,
    /// onMAC it'll be a .app which is a folder.
    /// </summary>
    [ObservableProperty] public string hatariApplication;
    
    /// <summary>
    /// Where Hatari loads and saves it's config file, different on all platforms.
    /// </summary>
    [ObservableProperty] public string hatariConfigFile;
    
    /// <summary>
    /// The folder to start to look for ROMS from. This is user specific
    /// </summary>
    [ObservableProperty] public string romFolder;
    
    /// <summary>
    /// The folder to start to look for Cartridge ROMS from. This is user specific
    /// </summary>
    [ObservableProperty] public string cartridgeFolder;
    
    /// <summary>
    /// The folder to start to look for Hard disk images (acsi, scsi, ide). This is user specific
    /// </summary>
    [ObservableProperty] public string hardDiskFolder;
    
    /// <summary>
    /// The folder to start to look for GEMDOS drive folders from. This is user specific
    /// </summary>
    [ObservableProperty] public string gemDosFolder;
    
    /// <summary>
    /// The folder to start to look for floppy images from. Strangely this is the only
    /// default folder Hatari implements internally.
    /// </summary>
    [ObservableProperty] public string floppyDiskFolder;
    
}