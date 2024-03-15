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

namespace MountFuji.Strategies;

public interface IDriveRetrievalStrategy
{
    IEnumerable<FileSystemDrive> RetrieveDrives();
}

public class MacOsDriveRetrievalStrategy : IDriveRetrievalStrategy
{
    private const string SystemsFolder = "/System";
    private const string VolumesFolder = "/Volumes";
    private const string DocumentsFolder = "Documents";

    public IEnumerable<FileSystemDrive> RetrieveDrives()
    {
        var res = new List<FileSystemDrive>();
        var systemDrives = DriveInfo.GetDrives();
        
        
        string homePath = Environment.GetEnvironmentVariable("HOME");
        if (Path.Exists(homePath))
        {
            res.Add(new FileSystemDrive(homePath, "Home"));
            var documentsPath = Path.Combine(homePath, DocumentsFolder);
            if (Path.Exists(documentsPath))
            {
                res.Add(new FileSystemDrive(documentsPath, "Documents"));
            }
        }

        foreach (var drive in systemDrives)
        {
            // exclude the RAM drive for things like /dev and the system volumes
            if (drive.IsReady && drive.DriveType != DriveType.Ram && !drive.Name.StartsWith(SystemsFolder))
            {
                string displayName = drive.Name;
                if (displayName.StartsWith(VolumesFolder))
                {
                    displayName = displayName.Replace($"{VolumesFolder}/", "");
                }
                else if (displayName == "/")
                {
                    displayName = "System";
                }

                res.Add(new FileSystemDrive(drive.Name, displayName));
            }
        }

        return res;
    }
}

public class WindowsDriveRetrievalStrategy : IDriveRetrievalStrategy
{
    public IEnumerable<FileSystemDrive> RetrieveDrives()
    {
        var res = new List<FileSystemDrive>();
        
        
        string homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        
        var systemDrives = DriveInfo.GetDrives();
        foreach (var drive in systemDrives)
        {
            if (drive.IsReady && !drive.Name.StartsWith("/"))
            {
                res.Add(new FileSystemDrive(drive.Name, drive.Name));
            }
        }

        return res;
    }
}