namespace MountFuji.Services;

public interface IPersistence
{
    /// <summary>
    /// The root folder for storing save files from fuji
    /// </summary>
    string MountFujiFolder { get; }
    
    /// <summary>
    /// Full path to the preferences file
    /// </summary>
    string MountFujiPreferencesFile { get; }
    
    /// <summary>
    /// Full path to the systems file
    /// </summary>
    string MountFujiSystemsFile { get; }

    /// <summary>
    /// Full path to the global config file (keyboard settings etc
    /// </summary>
    string MountFujiGlobalSystemConfigurationFile { get; }
    
    /// <summary>
    /// Serialize an instance of type T to a json file
    /// </summary>
    /// <param name="filename">full path to the file that we are serializing to</param>
    /// <param name="toSerialize">The object to serialize</param>
    Task SerializeAsync<T>(string filename, T toSerialize);

    /// <summary>
    /// Deserialize an instance of type T from a specified file. This will return default of T if
    /// the file does not exist
    /// </summary>
    /// <param name="filename">full path to the file to deserialize from</param>
    /// <typeparam name="T">A type to deserialize, must have a default constructor</typeparam>
    /// <returns>An instance of type T</returns>
    Task<T> DeSerializeAsync<T>(string filename) where T: new();

    /// <summary>
    /// Deserialize an instance of type T from a specified file. This will return default of T if
    /// the file does not exist
    /// </summary>
    /// <param name="filename">full path to the file to deserialize from</param>
    /// <typeparam name="T">A type to deserialize, must have a default constructor</typeparam>
    /// <returns>An instance of type T</returns>
    T DeSerialize<T>(string filename) where T: new();
}