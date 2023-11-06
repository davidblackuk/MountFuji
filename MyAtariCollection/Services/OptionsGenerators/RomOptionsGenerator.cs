namespace MyAtariCollection.Services.OptionsGenerators;

public class RomOptionsGenerator: OptionsGenerator, IRomOptionsGenerator
{
    public void Generate(AtariConfiguration config, StringBuilder builder)
    {
        AddQuotedFlag(builder, "tos", config.RomImage);
    }

}