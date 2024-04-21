/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Microsoft.Extensions.Logging;

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

public class PreferencesService : IPreferencesService
{
    private readonly IPersistence persistence;
    private readonly ILogger<PreferencesService> log;
    private readonly IApplicationResolver appResolver;


    /// <summary>
    /// Represents a service for managing user preferences.
    /// </summary>
    public PreferencesService(IPersistence persistence, ILogger<PreferencesService> log, IApplicationResolver appResolver)
    {
        this.persistence = persistence;
        this.log = log;
        this.appResolver = appResolver;
    }
    
    public ApplicationPreferences Preferences { get; private set; } = new();
    
    /// <summary>
    /// Loads the preferences, if they exist. Note this isn't async because it is run during
    /// startup in a non async code path.
    /// </summary>
    public void Load()
    {
        log.LogInformation("Attempting to load preferences from: {File}", persistence.MountFujiPreferencesFile);
        Preferences = persistence.DeSerialize<ApplicationPreferences>(persistence.MountFujiPreferencesFile);
        log.LogInformation("Preference {Pref}: {Value}", "CartridgeFolder", Preferences.CartridgeFolder);
        log.LogInformation("Preference {Pref}: {Value}", "FloppyDiskFolder", Preferences.FloppyDiskFolder);
        log.LogInformation("Preference {Pref}: {Value}", "GemDosFolder", Preferences.GemDosFolder);
        log.LogInformation("Preference {Pref}: {Value}", "HardDiskFolder", Preferences.HardDiskFolder);
        log.LogInformation("Preference {Pref}: {Value}", "HatariApplication", Preferences.HatariApplication);
        log.LogInformation("Preference {Pref}: {Value}", "HatariConfigFile", Preferences.HatariConfigFile);
        log.LogInformation("Preference {Pref}: {Value}", "RomFolder", Preferences.RomFolder);
    }

    /// <summary>
    /// Saves the preferences to the the preferences JSON file.
    /// </summary>
    public async Task SaveAsync()
    {
        log.LogInformation("Attempting to save preferences to: {File}", persistence.MountFujiPreferencesFile);
        await persistence.SerializeAsync(persistence.MountFujiPreferencesFile, Preferences);
    }

    /// <summary>
    /// Toggles the theme between ligh and dark modes
    /// </summary>
    public void ToggleTheme()
    {
        var current = GetTheme();
        
        // Toggle the theme, store the result.
        var next = (current == AppTheme.Light) ? AppTheme.Dark : AppTheme.Light;
        
        // Change the application theme
        appResolver.Application.UserAppTheme = next;
    }

    /// <summary>
    /// Gets the current them to use, since this isn't a single variable but
    /// a combination of several, the logic is encapsulated here in a single place.
    /// </summary>
    /// <returns>THe theme to use for the app</returns>
    public AppTheme GetTheme()
    {
        // When we first access the user theme, it Will be will be Unspecified, in that situation
        // we get the theme to the current platform one.
        return appResolver.Application.UserAppTheme == AppTheme.Unspecified
            ? appResolver.Application.PlatformAppTheme
            : appResolver.Application.UserAppTheme;
        
    }
}