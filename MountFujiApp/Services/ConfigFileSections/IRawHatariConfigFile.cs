namespace MountFuji.Services.ConfigFileSections;

public interface IRawHatariConfigFile
{
    Task Read(string configFilePath);

    Dictionary<String, Dictionary<string, string>> Sections { get; }
}