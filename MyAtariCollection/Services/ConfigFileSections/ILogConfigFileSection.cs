namespace MyAtariCollection.Services.ConfigFileSections;

public interface ILogConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}