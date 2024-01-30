

namespace MyAtariCollection.Services.ConfigFileSections;

public interface IRawHatariConfigFile
{
    Task Read(string configFilePath);

    Dictionary<String, Dictionary<string, string>> Sections { get; }
}


public class RawHatariConfigFile : IRawHatariConfigFile
{
    private readonly ILogger<RawHatariConfigFile> logger;

    public Dictionary<String, Dictionary<string, string>> Sections { get; private set; }= [];

    private Dictionary<string, string> current;

    public RawHatariConfigFile(ILogger<RawHatariConfigFile> logger)
    {
        this.logger = logger;
    }
    
    public async Task Read(string configFilePath)
    {
        if (!File.Exists(configFilePath))
        {
            throw new FileNotFoundException($"Hatari config file not found at: {configFilePath}");
        }

        var allLines = await File.ReadAllLinesAsync(configFilePath);

        foreach (var line in allLines)
        {
            if (!String.IsNullOrWhiteSpace(line))
            {
                if (!IsSectionStart(line))
                {
                    AddLineToCurrentSection(line);
                }
                
            }
        }
    }


    public bool IsSectionStart(string line)
    {
        var trimmed = line.Trim();
        if (trimmed.StartsWith('['))
        {
            trimmed = trimmed.Substring(1, trimmed.IndexOf("]", StringComparison.Ordinal) -1);
            current = new Dictionary<string, string>();
            Sections[trimmed] = current;
            return true;
        }

        return false;
    }

    private void AddLineToCurrentSection(string line)
    {
        var parts = line.Split("=", StringSplitOptions.TrimEntries);
        if (parts.Length < 2)
        {
            throw new InvalidDataException($"Expected name value pair in config line, but got: {line}");
        }

        // we do this daft jig because in the case of the LILO section it more than just
        // name = value, its "Args = root=/dev/ram video=atafb:vga16 load_ramdisk=1"
        var value = String.Join(' ', parts.Skip(1).Take(parts.Length - 1)); 
        current.Add(parts[0], value);
        
    }
}