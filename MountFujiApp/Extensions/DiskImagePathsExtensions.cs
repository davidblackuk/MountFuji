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

namespace MountFuji.Extensions;

public static class DiskImagePathsExtensions
{
    

    public static void SetImagePath(this AcsiScsiDiskOptions paths, int diskId, string fileFullPath)
    {
        switch (diskId)
        {
            case 0:
                paths.Disk0 = fileFullPath;
                break;
            case 1:
                paths.Disk1 = fileFullPath;
                break;
            case 2:
                paths.Disk2 = fileFullPath;
                break;
            case 3:
                paths.Disk3 = fileFullPath;
                break;
            case 4:
                paths.Disk4 = fileFullPath;
                break;
            case 5:
                paths.Disk5 = fileFullPath;
                break;
            case 6:
                paths.Disk6 = fileFullPath;
                break;
            case 7:
                paths.Disk7 = fileFullPath;
                break;
        }
    }
    
    public static void ClearImagePath(this AcsiScsiDiskOptions paths, int diskId)
    {
        switch (diskId)
        {
            case 0:
                paths.Disk0 = "";
                break;
            case 1:
                paths.Disk1 = "";
                break;
            case 2:
                paths.Disk2 = "";
                break;
            case 3:
                paths.Disk3 = "";
                break;
            case 4:
                paths.Disk4 = "";
                break;
            case 5:
                paths.Disk5 = "";
                break;
            case 6:
                paths.Disk6 = "";
                break;
            case 7:
                paths.Disk7 = "";
                break;
        }
    }
    
    
    public static void SetImagePath(this IdeDiskOptions options, int diskId, string fileFullPath)
    {
        if (diskId == 0)
        {
            options.Disk0 = fileFullPath;
        }
        else
        {
            options.Disk1 = fileFullPath;
        }
    }
    
    public static void ClearImagePath(this IdeDiskOptions options, int diskId)
    {
        if (diskId == 0)
        {
            options.Disk0 = "";
        }
        else
        {
            options.Disk1 = "";
        }
    }

    public static void SetImagePath(this FloppyDriveOptions options, int diskId, string fileFullPath)
    {
        if (diskId == 0)
        {
            options.DriveAPath = fileFullPath;
        }
        else
        {
            options.DriveBPath = fileFullPath;
        }
    }

    public static void ClearImagePath(this FloppyDriveOptions options, int diskId)
    {
        if (diskId == 0)
        {
            options.DriveAPath = "";
        }
        else
        {
            options.DriveBPath = "";
        }
    }
}