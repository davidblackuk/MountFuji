using System.Text;
using MountFuji.Models;

namespace MountFuji.Services.ConfigFileSections;

public class MemoryConfigFileSection: ConfigFileSection, IMemoryConfigFileSection
{
    public const string ConfigSectionName = "Memory";

    private const string MemorySizeKey = "nMemorySize";
    private const string TtRamSizeKey = "nTTRamSize";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {

        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder, (string)MemorySizeKey, (int)config.StMemorySize);
        AddFlag(builder, TtRamSizeKey, config.TtMemorySize * 1024);
        AddFlag(builder, "bAutoSave", false);
        // AddFlag(builder, "szMemoryCaptureFileName");
        // AddFlag(builder, "szAutoSaveFileName");

        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];

        to.StMemorySize = ParseInt(MemorySizeKey, section);
        to.TtMemorySize = ParseInt(TtRamSizeKey, section) / 1024;
        
    }
}