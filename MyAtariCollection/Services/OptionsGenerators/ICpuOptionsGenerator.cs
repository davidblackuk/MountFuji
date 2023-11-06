namespace MyAtariCollection.Services.OptionsGenerators;

public interface ICpuOptionsGenerator
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}