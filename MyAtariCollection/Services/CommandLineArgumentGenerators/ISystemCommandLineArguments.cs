namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public interface ISystemCommandLineArguments
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}