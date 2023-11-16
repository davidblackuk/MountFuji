namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public class RomCommandLineArguments: CommandLineArguments, IRomCommandLineArguments
{
    public void Generate(AtariConfiguration config, StringBuilder builder)
    {
        AddQuotedFlag(builder, "tos", config.RomImage);
    }

}