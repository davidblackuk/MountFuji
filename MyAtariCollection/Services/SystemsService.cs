

using Newtonsoft.Json;

namespace MyAtariCollection.Services;

public class SystemsService
{
    private readonly IPersistance persistance;

    private List<AtariConfiguration> store = [];
    public IEnumerable<AtariConfiguration> All() => store.AsEnumerable();

    public SystemsService(IPersistance persistance)
    {
        this.persistance = persistance;
    }
    
    public void Load()
    {
        Console.WriteLine("Attempting to load systems from: " + persistance.MountFujiSystemsFile);
        store = persistance.DeSerialize<List<AtariConfiguration>>(persistance.MountFujiSystemsFile);
    }
    
    /// <summary>
    /// Saves the preferences to the the preferences JSON file.
    /// </summary>
    public async Task Save()
    {
        Console.WriteLine("Attempting to save preferences to: " + persistance.MountFujiSystemsFile);
        await persistance.SerializeAsync(persistance.MountFujiSystemsFile, store);
    }

 
    public void Add(AtariConfiguration newConfiguration)
    {
        newConfiguration.Id = Guid.NewGuid().ToString();
        store.Add(newConfiguration);
    }


    public AtariConfiguration Clone(AtariConfiguration original, string newName)
    {
        // not the fastest approach but this isn't used in a game loop!
        var json = JsonConvert.SerializeObject(original);
        AtariConfiguration clone = JsonConvert.DeserializeObject<AtariConfiguration>(json);
        clone.DisplayName = newName;
        Add(clone);
        return clone;
    }
    
    public void Delete(string id)
    {
        AtariConfiguration config = Find(id);
        
        // technically a moot test as List.Remove(null) just returns false if there
        // are no null entries in the List, which there can be
        if (config is not null)
        {
            store.Remove(config);
        }
    }

    public AtariConfiguration Find(string id)
    {
        return store.FirstOrDefault(system => system.Id == id);
    }
    
}