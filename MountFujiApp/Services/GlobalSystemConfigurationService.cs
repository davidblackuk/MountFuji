// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Animations;
using MountFuji.Models.Keyboard;
using MountFuji.Services.ConfigFileSections;
using MountFuji.Services.GlobalConfig;
using KeyboardOptions = MountFuji.Models.Keyboard.KeyboardOptions;
using KeyboardShortcuts = MountFuji.Models.Keyboard.KeyboardShortcuts;

namespace MountFuji.Services;

public interface IGlobalSystemConfigurationService
{
    /// <summary>
    /// Gets the global configuration for items like keyboards, printers etc
    /// </summary>
    GlobalSystemConfiguration Configuration { get; }
    
    /// <summary>
    /// Loads the config from disk, this call is not async as it's inoked pre app setup
    /// </summary>
    void Load();

    /// <summary>
    /// Loads the config from disk asynchronously
    /// </summary>
    Task LoadAsync();
    
    /// <summary>
    /// Saves the current global config to disk
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();

    /// <summary>
    /// Extracts out the KeyBoardOptions from a specified hatari config file
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    Task<KeyboardOptions> ImportKeyboardOptionsFromConfigFile(string filename);
    
    // sets a short cut key, does not persist the result
    void SetShortcutKey(ShortcutModifier modifier, ShortcutKey key, string newValue);
}

public class GlobalSystemConfigurationService : IGlobalSystemConfigurationService
{
    private readonly IKeyboardConfigFileSection keyboardConfigSection;
    private readonly IPersistence persistence;
    private readonly IRawHatariConfigFile rawFileReader;
    private readonly ISortcutKeySetter shortcutKeySetter;
    private readonly ILogger<GlobalSystemConfigurationService> logger;
    public GlobalSystemConfiguration Configuration { get; private set; } = new();

    public GlobalSystemConfigurationService(IKeyboardConfigFileSection keyboardConfigSection,
        IPersistence persistence,
        IRawHatariConfigFile rawFileReader,
        ISortcutKeySetter shortcutKeySetter,
        ILogger<GlobalSystemConfigurationService> logger)
    {
        this.keyboardConfigSection = keyboardConfigSection;
        this.persistence = persistence;
        this.rawFileReader = rawFileReader;
        this.shortcutKeySetter = shortcutKeySetter;
        this.logger = logger;
    }
    
    public void Load()
    {
        logger.LogInformation("Attempting to load global system config from: {File}", persistence.MountFujiGlobalSystemConfigurationFile);
        Configuration = persistence.DeSerialize<GlobalSystemConfiguration>(persistence.MountFujiGlobalSystemConfigurationFile);
    }

    public async Task LoadAsync()
    {
        logger.LogInformation("Attempting to load global system config from: {File}", persistence.MountFujiGlobalSystemConfigurationFile);
        Configuration = await persistence.DeSerializeAsync<GlobalSystemConfiguration>(persistence.MountFujiGlobalSystemConfigurationFile);

    }

    public async Task SaveAsync()
    {
        logger.LogInformation("Attempting to save global system config to: {File}", persistence.MountFujiGlobalSystemConfigurationFile);
        await persistence.SerializeAsync(persistence.MountFujiGlobalSystemConfigurationFile, Configuration);
    }

    public async Task<KeyboardOptions> ImportKeyboardOptionsFromConfigFile(string filename)
    {
        var res = new KeyboardOptions();
        await rawFileReader.Read(filename);
        keyboardConfigSection.FromHatariConfig(res, rawFileReader.Sections);
        return res;
    }
    
    public void SetShortcutKey(ShortcutModifier modifier, ShortcutKey key, string newValue)
    {
        if (modifier == ShortcutModifier.WithModifier)
        {
            shortcutKeySetter.SetShortcutKey(Configuration.KeyboardOptions.ShortcutsWithModifier, key, newValue);
        }
        else
        {
            shortcutKeySetter.SetShortcutKey(Configuration.KeyboardOptions.ShortcutsWithoutModifier, key, newValue);
        }
    }
}