namespace MyAtariCollection.Services.ConfigFileSections;

public interface ISystemConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}