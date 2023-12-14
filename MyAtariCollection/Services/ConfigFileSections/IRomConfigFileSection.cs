namespace MyAtariCollection.Services.ConfigFileSections;

public interface IRomConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}