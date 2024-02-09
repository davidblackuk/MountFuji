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

public partial class FloppyDriveOptions: ObservableObject
{
    [ObservableProperty] private string driveAPath = String.Empty;
    [ObservableProperty] private bool driveAEnabled = true;
    [ObservableProperty] private bool driveADoubleSided = true;

    [ObservableProperty] private string driveBPath = String.Empty;
    [ObservableProperty] private bool driveBEnabled = true;
    [ObservableProperty] private bool driveBDoubleSided = true;

    [ObservableProperty] private bool autoInsertB = true;
    [ObservableProperty] private bool fastFloppyAccess = false;

    [ObservableProperty] private DiskWriteProtection writeProtection = DiskWriteProtection.Off;


}