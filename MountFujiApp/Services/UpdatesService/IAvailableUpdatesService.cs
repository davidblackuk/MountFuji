namespace MountFuji.Services.UpdatesService;

public interface IAvailableUpdatesService
{
    /// <summary>
    /// Checks if an update is available based from the provided current version. This makes a call to a GitHub API.
    /// </summary>
    /// <returns>A tuple reflecting if an update is available and if so the lastest version number.</returns>
    Task<(bool IsUpdateAvailable, Version ToVersion)> CheckForUpdate();
}