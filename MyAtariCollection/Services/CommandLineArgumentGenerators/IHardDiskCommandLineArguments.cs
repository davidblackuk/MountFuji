namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public interface IHardDiskCommandLineArguments
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}