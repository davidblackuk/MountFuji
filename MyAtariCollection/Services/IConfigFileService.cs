namespace MountFuji.Services;

public interface IConfigFileService
{

    /// <summary>
    /// Persists the specified configuration into the hatari.cfg file
    /// </summary>
    /// <returns></returns>
    Task Save(AtariConfiguration config);

    /// <summary>
    /// Read a Hatari config file and returns an AtariConfiguration based on that
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    Task<AtariConfiguration> Load(string path);


}