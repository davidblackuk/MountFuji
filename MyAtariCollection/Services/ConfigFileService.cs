using MyAtariCollection.Services.ConfigFileSections;

namespace MyAtariCollection.Services;

public class ConfigFileService : IConfigFileService
{
    private readonly IMemoryConfigFileSection memoryConfig;
    private readonly ISystemConfigFileSection systemConfig;
    private readonly IRomConfigFileSection romConfig;
    private readonly IAcsiConfigFileSection acsiConfig;
    private readonly IScsiConfigFileSection scsiConfig;
    private readonly IIdeConfigFileSection ideSection;
    private readonly ILogConfigFileSection logConfig;

    public ConfigFileService(ILogConfigFileSection logConfig, 
        IMemoryConfigFileSection memoryConfig, ISystemConfigFileSection systemConfig, IRomConfigFileSection romConfig, 
        IAcsiConfigFileSection acsiConfig, IScsiConfigFileSection scsiConfig, IIdeConfigFileSection ideSection)
    {
        this.memoryConfig = memoryConfig;
        this.systemConfig = systemConfig;
        this.romConfig = romConfig;
        this.acsiConfig = acsiConfig;
        this.scsiConfig = scsiConfig;
        this.ideSection = ideSection;
        this.logConfig = logConfig;
    }
    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        logConfig.Generate(builder, config);
        builder.AppendLine("");
        
        scsiConfig.Generate(builder, config);
        builder.AppendLine("");
        
        ideSection.Generate(builder, config);
        builder.AppendLine("");
        
        acsiConfig.Generate(builder, config);
        builder.AppendLine("");
        
        romConfig.Generate(builder, config);
        builder.AppendLine("");
        
        memoryConfig.Generate(builder, config);
        builder.AppendLine("");
        
        systemConfig.Generate(builder, config);
        builder.AppendLine("");
        
        return builder.ToString();
    }
}