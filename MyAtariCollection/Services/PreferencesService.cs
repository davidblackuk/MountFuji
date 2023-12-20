

using System.Text.Json;
using CommunityToolkit.Maui.Core.Primitives;

namespace MyAtariCollection.Services;

public interface IPreferencesService
{
    ApplicationPreferences Preferences { get; set; }
    void Load();
    Task Save();
}

public class PreferencesService : IPreferencesService
{
    private const string AppPreferencesFilename = "preferences.json";
    private const string AppDataFolder= "fuji";

    public ApplicationPreferences Preferences { get; set; } = new();
    
    /// <summary>
    /// Loads the preferences, if they exist. Note this isn't async because it is run during
    /// startup in a non async code path.
    /// </summary>
    public void Load()
    {
        Console.WriteLine("Attempting to load preferences from: " + MountFujiPreferencesFile);
        if (File.Exists(MountFujiPreferencesFile))
        {
            try
            {
                string data = File.ReadAllText(MountFujiPreferencesFile);
                ApplicationPreferences? prefs = JsonSerializer.Deserialize<ApplicationPreferences>(data);
                if (prefs != null)
                {
                    Preferences = prefs;
                }
                else
                {
                    Console.WriteLine("Failed to deserialize preferences: null result");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading preferences {e.Message}");
            }
        }
        else
        {
            Console.WriteLine("No preferences file exists, using empty defaults");
        }
    }

    public async Task Save()
    {
        EnsureFolderExists();
        Console.WriteLine($"Saving preferences to: {MountFujiPreferencesFile}");

        try
        {
            using FileStream createStream = File.Create(MountFujiPreferencesFile);
            await JsonSerializer.SerializeAsync(createStream, Preferences, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            //await createStream.DisposeAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error serializing preferences: {e.Message}");
        }
    }

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

    private string MountFujiFolder => Path.Combine(FileSystem.AppDataDirectory, AppDataFolder);
    private string MountFujiPreferencesFile => Path.Combine(MountFujiFolder, AppPreferencesFilename);


}