namespace MyAtariCollection.Services;

public interface IConfigFileService
{

    /// <summary>
    /// Persists the current configuration into the hatari.cfg file
    /// </summary>
    /// <returns></returns>
    Task Persist(AtariConfiguration config);
}