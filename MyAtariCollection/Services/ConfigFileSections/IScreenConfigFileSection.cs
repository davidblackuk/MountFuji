namespace MyAtariCollection.Services.ConfigFileSections;

public interface IScreenConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}