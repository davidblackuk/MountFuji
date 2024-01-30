namespace MyAtariCollection.Services.ConfigFileSections;

public interface IFloppyConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
    
    void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections);    
}