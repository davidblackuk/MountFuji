namespace MyAtariCollection.Services.ConfigFileSections;

public interface ILogConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
}