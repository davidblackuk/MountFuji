

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
    private readonly IPersistance persistance;

    public PreferencesService(IPersistance persistance)
    {
        this.persistance = persistance;
    }
    
    public ApplicationPreferences Preferences { get; set; } = new();
    
    /// <summary>
    /// Loads the preferences, if they exist. Note this isn't async because it is run during
    /// startup in a non async code path.
    /// </summary>
    public void Load()
    {
        Console.WriteLine("Attempting to load preferences from: " + persistance.MountFujiPreferencesFile);
        Preferences = persistance.DeSerialize<ApplicationPreferences>(persistance.MountFujiPreferencesFile);
    }

    /// <summary>
    /// Saves the preferences to the the preferences JSON file.
    /// </summary>
    public async Task Save()
    {
        Console.WriteLine("Attempting to save preferences to: " + persistance.MountFujiPreferencesFile);
        await persistance.SerializeAsync(persistance.MountFujiPreferencesFile, Preferences);
    }
}