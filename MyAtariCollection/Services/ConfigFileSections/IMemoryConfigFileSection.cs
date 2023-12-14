namespace MyAtariCollection.Services.ConfigFileSections;

public interface IMemoryConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}