

using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace MyAtariCollection.Services;

public class SystemsService
{
    private readonly IPersistance persistance;

    private List<AtariConfiguration> store = [];
    private string stateForDirtyCheck;
    public IEnumerable<AtariConfiguration> All() => store.AsEnumerable();

    public SystemsService(IPersistance persistance)
    {
        this.persistance = persistance;
    }
    
    public void Load()
    {
        Console.WriteLine("Attempting to load systems from: " + persistance.MountFujiSystemsFile);
        store = persistance.DeSerialize<List<AtariConfiguration>>(persistance.MountFujiSystemsFile);
        SetStateForDirtyCheck();
    }
    
    /// <summary>
    /// Saves the preferences to the the preferences JSON file.
    /// </summary>
    public async Task Save()
    {
        Console.WriteLine("Attempting to save preferences to: " + persistance.MountFujiSystemsFile);
        await persistance.SerializeAsync(persistance.MountFujiSystemsFile, store);
        SetStateForDirtyCheck();
    }

    private void SetStateForDirtyCheck()
    {
        stateForDirtyCheck = JsonSerializer.Serialize(store);
    }

    public bool IsDirty
    {
        get
        {
            string currentState = JsonSerializer.Serialize(store);
            return !currentState.Equals(stateForDirtyCheck);
        }
    }

    public void Add(AtariConfiguration newConfiguration)
    {
        newConfiguration.Id = Guid.NewGuid().ToString();
        store.Add(newConfiguration);
    }


    public AtariConfiguration Clone(AtariConfiguration original, string newName)
    {
        // not the fastest approach but this isn't used in a game loop!
        var json = JsonSerializer.Serialize<AtariConfiguration>(original);
        AtariConfiguration clone = JsonSerializer.Deserialize<AtariConfiguration>(json);
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

    public void ReorderByIds(List<string> ids)
    {
        store = store.OrderBy(d => ids.IndexOf(d.Id)).ToList();
    }
}