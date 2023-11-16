namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public interface IFloppyCommandLineArguments
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}