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

namespace MountFuji.Models;

public class Rom
{
    public int VersionMajor { get; set; }
    public int VersionMinor { get; set; }
    
    public string DisplayVersion => $"{VersionMajor}.{VersionMinor:D2}";

    public string DisplayReleaseDate => $"{ReleaseDate:MMM dd, yyyy}";

    public Country Country { get; set; }
    
    public string CountryFlag { get; set; }
    public string Name { get; set; }

    public string Path { get; set; }
    
    public DateTime ReleaseDate { get; set; }

    public override string ToString() => $" {DisplayVersion} - {Country} - {DisplayReleaseDate} - {Name}";
}