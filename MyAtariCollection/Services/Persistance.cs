using System.Text.Json;

namespace MyAtariCollection.Services;

public interface IPersistance
{
    string MountFujiPreferencesFile { get; }
    string MountFujiSystemsFile { get; }
    
    Task SerializeAsync<T>(string filename, T toSerialize);

    T DeSerialize<T>(string filename) where T: new();
}

public class Persistance : IPersistance
{
    private const string AppPreferencesFilename = "preferences.json";
    private const string AppSystemsFilename = "systems.json";
    private const string AppDataFolder= "fuji";

    private string MountFujiFolder => Path.Combine(FileSystem.AppDataDirectory, AppDataFolder);
    public string MountFujiPreferencesFile => Path.Combine(MountFujiFolder, AppPreferencesFilename);
    public string MountFujiSystemsFile => Path.Combine(MountFujiFolder, AppSystemsFilename);
    
    private void EnsureFolderExists()
    {
        try
        {
            if (!Path.Exists(MountFujiFolder))
            {
                Console.WriteLine($"Creating App folder: {MountFujiFolder}");
                Directory.CreateDirectory(MountFujiFolder);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating Fuji folder {MountFujiFolder} - {e.Message}");
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
            Console.WriteLine($"Error serializing {typeof(T).FullName} preferences: {e.Message}");
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
                    Console.WriteLine($"Failed to deserialize {typeof(T).Name} preferences: null result");
            }
            else
            {
                Console.WriteLine($"{filename} does not exist, going with defaults");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading {typeof(T).FullName} {e.Message}");
        }
        return deserialized != null ? deserialized : new T();
    } 
    
   
}