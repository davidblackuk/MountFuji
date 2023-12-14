namespace MyAtariCollection.Services.ConfigFileSections;

public class RomConfigFileSection: ConfigFileSection, IRomConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "ROM");
        
        AddFlag(builder, "szTosImageFileName", config.RomImage);
        AddFlag(builder, "szCartridgeImageFileName", config.CartridgeImage);
        AddFlag(builder, "bPatchTos", true);
    }
}