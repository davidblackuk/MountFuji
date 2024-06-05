namespace MountFuji.Services;

/// <summary>
/// Manages preferences
/// </summary>
public interface IPreferencesService
{
    /// <summary>
    /// Gives access to the current preferences
    /// </summary>
    ApplicationPreferences Preferences { get; }
    
    /// <summary>
    /// Loads the preferences, if they exist. Note this isn't async because it is run during
    /// startup in a non async code path.
    /// </summary>
    void Load();
    
    /// <summary>
    /// Saves the Preferences to the data folder, you can find the
    /// location of that file from the about dialog
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();

    /// <summary>
    /// Toggles the theme between light and dark modes
    /// </summary>
    void ToggleTheme();

    /// <summary>
    /// Gets the current them to use, since this isn't a single variable but
    /// a combination of several, the logic is encapsulated here in a single place.
    /// </summary>
    /// <returns>THe theme to use for the app</returns>
    AppTheme GetTheme();
}