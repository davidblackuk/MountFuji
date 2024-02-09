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

public interface IPreferencesService
{
    ApplicationPreferences Preferences { get; set; }
    void Load();
    Task Save();
}

public class PreferencesService : IPreferencesService
{
    private readonly IPersistance persistance;
    private readonly ILogger<PreferencesService> log;

    public PreferencesService(IPersistance persistance, ILogger<PreferencesService> log)
    {
        this.persistance = persistance;
        this.log = log;
    }
    
    public ApplicationPreferences Preferences { get; set; } = new();
    
    /// <summary>
    /// Loads the preferences, if they exist. Note this isn't async because it is run during
    /// startup in a non async code path.
    /// </summary>
    public void Load()
    {
        log.LogInformation("Attempting to load preferences from: {PreferencesFile}", persistance.MountFujiPreferencesFile);
        Preferences = persistance.DeSerialize<ApplicationPreferences>(persistance.MountFujiPreferencesFile);
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
    public async Task Save()
    {
        log.LogInformation("Attempting to save preferences to: {PreferencesFile}", persistance.MountFujiPreferencesFile);
        await persistance.SerializeAsync(persistance.MountFujiPreferencesFile, Preferences);
    }
}