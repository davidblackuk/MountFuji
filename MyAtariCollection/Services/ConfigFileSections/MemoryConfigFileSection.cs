namespace MyAtariCollection.Services.ConfigFileSections;

public class MemoryConfigFileSection: ConfigFileSection, IMemoryConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Memory");
        
        AddFlag(builder, "nMemorySize", config.StMemorySize);
        AddFlag(builder, "nTTRamSize", config.TtMemorySize * 1024);
        AddFlag(builder, "bAutoSave", false);
        // AddFlag(builder, "szMemoryCaptureFileName");
        // AddFlag(builder, "szAutoSaveFileName");

    }
}