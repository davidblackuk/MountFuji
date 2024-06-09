namespace MountFuji.Services.UpdatesService;

public interface IGitHubVersionApi
{
    Task<List<Version>> GetMountFujiPublicVersions (HttpClient httpClient);
}