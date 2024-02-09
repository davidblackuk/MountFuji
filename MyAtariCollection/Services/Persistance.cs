using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MountFuji.Services;

public interface IPersistance
{
    string MountFujiFolder { get; }
    string MountFujiPreferencesFile { get; }
    string MountFujiSystemsFile { get; }
    
    Task SerializeAsync<T>(string filename, T toSerialize);

    T DeSerialize<T>(string filename) where T: new();
}

public class Persistance : IPersistance
{
    private readonly ILogger<Persistance> log;
    private const string AppPreferencesFilename = "preferences.json";
    private const string AppSystemsFilename = "systems.json";
    private const string AppDataFolder= "fuji";

    public string MountFujiFolder => Path.Combine(FileSystem.AppDataDirectory, AppDataFolder);

    public string MountFujiPreferencesFile => Path.Combine(MountFujiFolder, AppPreferencesFilename);
    public string MountFujiSystemsFile => Path.Combine(MountFujiFolder, AppSystemsFilename);

    public Persistance(ILogger<Persistance> log)
    {
        this.log = log;
    }
    
    private void EnsureFolderExists()
    {
        try
        {
            if (!Path.Exists(MountFujiFolder))
            {
                log.LogInformation("Creating App folder: {MountFujiFolder}", MountFujiFolder);
                Directory.CreateDirectory(MountFujiFolder);
            }
        }
        catch (Exception e)
        {
            log.LogError(e,"Error creating Fuji folder {MountFujiFolder}", MountFujiFolder);
        }
    }

    public async Task SerializeAsync<T>(string filename, T toSerialize)
    {
        try
        {
            EnsureFolderExists();
            using FileStream createStream = File.Create(filename);
            await JsonSerializer.SerializeAsync<T>(createStream, toSerialize, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }
        catch (Exception e)
        {
            log.LogInformation(e, "Error serializing type: {Type}", typeof(T).FullName);
        }
    }
    
    public T DeSerialize<T>(string filename)
    where T: new()
    {
        T deserialized = default;
        try
        {
            if (File.Exists(filename))
            {
                string data = File.ReadAllText(filename);
                deserialized = JsonSerializer.Deserialize<T>(data);
                if (deserialized is null)
                    log.LogInformation("Error deserializing type: {Type}, result is null", typeof(T).FullName);

            }
            else
            {
                log.LogInformation("File: {Filename}, does not exist, going with defaults", filename);
            }
        }
        catch (Exception e)
        {
            log.LogInformation(e, "Error deserializing type: {Type}", typeof(T).FullName);
        }
        return deserialized != null ? deserialized : new T();
    } 
}