using System.Text.Json;
using Microsoft.Extensions.Logging;
using MountFuji.Services.Dtos;

namespace MountFuji.Services.UpdatesService;

public class GitHubVersionApi : IGitHubVersionApi
{
    private readonly ILogger<GitHubVersionApi> logger;
    private const string ReleaseApiUrl = "https://api.github.com/repos/davidblackuk/MountFuji/releases";

    public GitHubVersionApi(ILogger<GitHubVersionApi> logger)
    {
        this.logger = logger;
    }
    
    
    public async Task<List<Version>> GetMountFujiPublicVersions (HttpClient httpClient)
    {
        List<Version> versions = new List<Version>();

        try
        {
            await using Stream stream =
                await httpClient.GetStreamAsync(ReleaseApiUrl);
            var releases =
                await JsonSerializer.DeserializeAsync<List<GitHubRelease>>(stream);

            foreach (var release in releases)
            {
                if (Version.TryParse(release.tag_name, out Version version))
                {
                    versions.Add(version);
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e,"Could not retrieve release information from GitHub @ {Url}", ReleaseApiUrl);
        }

        return versions;
    }
}