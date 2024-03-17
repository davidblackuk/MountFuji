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

using Microsoft.Extensions.Logging;

namespace MountFuji.Services;

public interface IFileSystemService
{
    /// <summary>
    /// Retrieves the sub folders of the specified parent folder, this includes the uplevel node for non-root folders
    /// </summary>
    /// <param name="parent">The file system path of the parent</param>
    /// <returns>An enumerable list of FileSystemEntries for the child folders</returns>
    IEnumerable<FileSystemEntry> GetChildFolders(string parent);

    /// <summary>
    /// Retrieves the files contained in the specified parent folder
    /// </summary>
    /// <param name="parent">The file system path of the parent</param>
    /// <returns>An enumerable list of FileSystemEntries for the child files</returns>
    IEnumerable<FileSystemEntry> GetChildFiles(string parent);

    /// <summary>
    /// For a specified folder, gets a list of file system entries for all folders from the root,
    /// upto and including the specified folder
    /// </summary>
    /// <param name="folder">The full path of the folder to form the breadcrumb for</param>
    /// <returns>an enumerable of BreadcrumbEntry for the folders in the breadcrumb</returns>
    IEnumerable<BreadcrumbEntry> GetBreadCrumbFromRoot(string folder);
}

public class FileSystemService : IFileSystemService
{
    private readonly ILogger<FileSystemService> log;

    /// <summary>
    /// Constructs a new instance of a file system service.
    /// </summary>
    public FileSystemService(ILogger<FileSystemService> log)
    {
        this.log = log;
    }

    /// <summary>
    /// Retrieves the sub folders of the specified parent folder, this includes the uplevel node for non-root folders
    /// </summary>
    /// <param name="parent">The file system path of the parent</param>
    /// <returns>An enumerable list of FileSystemEntries for the children</returns>
    public IEnumerable<FileSystemEntry> GetChildFolders(string parent)
    {
        List<FileSystemEntry> res = new List<FileSystemEntry>();

        string[] dirs = [];
        try
        {
            dirs = Directory.GetDirectories(parent);
        }
        catch (Exception e)
        {
            log.LogError(e, "Could not iterate folders in the file service");
        }

        DirectoryInfo info = new DirectoryInfo(parent);
        if (info.Parent != null)
        {
            res.Add(new FileSystemEntry(parent, EntryType.ParentNavigation));
        }

        Array.Sort(dirs, String.Compare);
        res.AddRange(dirs.Select(dir => new FileSystemEntry(dir, EntryType.Folder)));
        return res;
    }

    /// <summary>
    /// Retrieves the files contained in the specified parent folder
    /// </summary>
    /// <param name="parent">The file system path of the parent</param>
    /// <returns>An enumerable list of FileSystemEntries for the child files</returns>
    public IEnumerable<FileSystemEntry> GetChildFiles(string parent)
    {
        List<FileSystemEntry> res = new List<FileSystemEntry>();

        string[] files = [];
        try
        {
            files = Directory.GetFiles(parent, "*", new EnumerationOptions()
            {
                ReturnSpecialDirectories = false,
                IgnoreInaccessible = true,
                RecurseSubdirectories = false,
            });
            files = Directory.GetFiles(parent);
        }
        catch (Exception e)
        {
            log.LogError(e, "Could not iterate files in the file service");
        }


        Array.Sort(files, String.Compare);
        res.AddRange(files.Select(dir => new FileSystemEntry(dir, EntryType.File)));
        return res;
    }

    /// <summary>
    /// For a specified folder, gets a list of file system entries for all folders from the root,
    /// upto and including the specified folder
    /// </summary>
    /// <param name="folder">The full path of the folder to form the breadcrumb for</param>
    /// <returns>an enumerable of BreadcrumbEntry for the folders in the breadcrumb</returns>
    public IEnumerable<BreadcrumbEntry> GetBreadCrumbFromRoot(string folder)
    {
        List<BreadcrumbEntry> res = new List<BreadcrumbEntry>();

#nullable enable
        DirectoryInfo? info = new DirectoryInfo(folder);
#nullable disable

        while (info is not null)
        {
            res.Insert(0, new BreadcrumbEntry(info.FullName));
            info = info.Parent;
        }

        res.First().IsRootFolder = true;
        res.Last().IsTerminalFolder = true;

        return res;
    }
}