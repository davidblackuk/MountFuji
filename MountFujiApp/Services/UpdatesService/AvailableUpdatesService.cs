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

using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace MountFuji.Services.UpdatesService;

public class AvailableUpdatesService : IAvailableUpdatesService
{
    private readonly IGitHubVersionApi versionApi;
    private readonly ILogger<AvailableUpdatesService> logger;
    private readonly IApplicationVersion version;

    /// <summary>
    /// Single copy of an HttpClient as this service is a singleton
    /// </summary>
    HttpClient client = new();

    private Version gitHubVersion;
    private bool hasUpdate;
    private bool restApiCalled;
    
    public AvailableUpdatesService(IGitHubVersionApi versionApi, ILogger<AvailableUpdatesService> logger,
        IApplicationVersion version)
    {
        this.versionApi = versionApi;
        this.logger = logger;
        this.version = version;
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
        client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

    }
    
    
    
    public async Task<(bool IsUpdateAvailable, Version ToVersion)> CheckForUpdate()
    {
        // the data is going to be invariant across the lifetime of the application, for all intents and purposes
        // and this method is called multiple times. So we'll cache the results.
        if (restApiCalled) return (IsUpdateAvailable: hasUpdate, ToVersion: gitHubVersion);
        
        gitHubVersion = await GetLatestGitHubVersion();

        hasUpdate = gitHubVersion.CompareTo(version.Current) > 0;
        if (hasUpdate)
        {
            logger.LogInformation("Update available from {Current} to {Latest}", version.Current, gitHubVersion);
        }
        else
        {
            logger.LogInformation("We are on the latest version");
        }

        restApiCalled = true;
        return (IsUpdateAvailable: hasUpdate, ToVersion: gitHubVersion);
    }

    /// <summary>
    /// Accesses the GitHub API to download all releases and then return the latest.
    /// </summary>
    private async Task<Version> GetLatestGitHubVersion()
    {
        Version latestOnlineVersion = new Version(0,0,0);
        List<Version> versions = await versionApi.GetMountFujiPublicVersions(client);

        logger.LogInformation("Checking if update available. our version is {Version}", version.Current);
        foreach (var onlineVersion in versions)
        {
            if (onlineVersion.CompareTo(latestOnlineVersion) > 0)
            {
                latestOnlineVersion = onlineVersion;
            }
        }
        logger.LogInformation("Latest version available online: {Latest}", latestOnlineVersion);
        return latestOnlineVersion;
    }
}