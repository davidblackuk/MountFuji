using System.Text;
using MountFuji.Models;

namespace MountFuji.Services.ConfigFileSections;

public class RomConfigFileSection: ConfigFileSection, IRomConfigFileSection
{
    public const string ConfigSectionName = "ROM";

    private const string TosImageFilenameKey = "szTosImageFileName";
    private const string CartridgeImageFilenameKey = "szCartridgeImageFileName";
    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder, (string)TosImageFilenameKey, (string)config.RomImage);
        AddFlag(builder, (string)CartridgeImageFilenameKey, (string)config.CartridgeImage);
        AddFlag(builder, "bPatchTos", true);
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];

        to.RomImage = section[TosImageFilenameKey];
        to.CartridgeImage = section[CartridgeImageFilenameKey];
    }

}