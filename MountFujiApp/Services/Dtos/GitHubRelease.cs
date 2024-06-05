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

using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming

namespace MountFuji.Services.Dtos;


#nullable disable

[ExcludeFromCodeCoverage]
public class GitHubRelease
{
    public string url { get; set; }
    public string assets_url { get; set; }
    public string upload_url { get; set; }
    public string html_url { get; set; }
    public int id { get; set; }
    public GitHubReleaseAuthor GitHubReleaseAuthor { get; set; }
    public string node_id { get; set; }
    public string tag_name { get; set; }
    public string target_commitish { get; set; }
    public string name { get; set; }
    public bool draft { get; set; }
    public bool prerelease { get; set; }
    public string created_at { get; set; }
    public string published_at { get; set; }
    public GitHubReleaseAssets[] assets { get; set; }
    public string tarball_url { get; set; }
    public string zipball_url { get; set; }
    public string body { get; set; }
}

#nullable enable