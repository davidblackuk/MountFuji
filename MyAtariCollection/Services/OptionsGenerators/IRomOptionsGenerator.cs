namespace MyAtariCollection.Services.OptionsGenerators;

public interface IRomOptionsGenerator
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}