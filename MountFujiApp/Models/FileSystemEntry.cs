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

using MountFuji.Controls;

namespace MountFuji.Models;

public enum EntryType  
{

    ParentNavigation,
    Folder,
    File,
    Null
}

public class FileSystemEntry(string path, EntryType entryType)
{
    public static FileSystemEntry Null = new FileSystemEntry("",EntryType.Null);
    public string Path { get; set; } = path;
    public EntryType EntryType { get; private set; } = entryType;

    public string DisplayName => (EntryType == EntryType.ParentNavigation) ? ".." : new DirectoryInfo(Path).Name;

    public bool IsDirectory => EntryType == EntryType.Folder;

    public bool IsParentNavigation => EntryType == EntryType.ParentNavigation;
    
    public string Icon => (EntryType == EntryType.Folder || EntryType == EntryType.ParentNavigation) ? IconFont.Folder_open : IconFont.Description;
}

public class FileSystemDrive(string path, string displayName)
{
    public string Path { get; set; } = path;

    public string DisplayName { get; set; } = displayName;

    public string Icon => IconFont.Folder_special;
}