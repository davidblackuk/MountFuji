namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public interface ICpuCommandLineArguments
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}