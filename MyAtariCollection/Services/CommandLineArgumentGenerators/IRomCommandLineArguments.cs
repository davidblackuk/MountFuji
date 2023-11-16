namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public interface IRomCommandLineArguments
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}