using MountFuji.Models.Keyboard;

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