namespace MountFuji.Services;

public interface ISystemsService
{
    IEnumerable<AtariConfiguration> All();
    void Load();

    /// <summary>
    /// Saves the preferences to the the preferences JSON file.
    /// </summary>
    Task Save();

    void SetStateForDirtyCheck();
    bool IsDirty { get; }
    void Add(AtariConfiguration newConfiguration);
    AtariConfiguration Clone(AtariConfiguration original, string newName);
    void Delete(string id);
    AtariConfiguration Find(string id);
    void ReorderByIds(List<string> ids);

    /// <summary>
    /// Imports a hatari .cfg file and creates a new system from it
    /// </summary>
    /// <param name="filename">full path to (including name) to the config file we are importing </param>
    /// <param name="name">The name to give to the new system</param>
    /// <returns></returns>
    Task<AtariConfiguration> Import(string filename, string name);
}