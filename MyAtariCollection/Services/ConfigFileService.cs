using MyAtariCollection.Services.ConfigFileSections;

namespace MyAtariCollection.Services;

public class ConfigFileService : IConfigFileService
{
    private readonly IMemoryConfigFileSection memoryConfig;
    private readonly ISystemConfigFileSection systemConfig;
    private readonly IRomConfigFileSection romConfig;
    private readonly ILogConfigFileSection logConfig;

    public ConfigFileService(ILogConfigFileSection logConfig, 
        IMemoryConfigFileSection memoryConfig, ISystemConfigFileSection systemConfig,
        IRomConfigFileSection romConfig)
    {
        this.memoryConfig = memoryConfig;
        this.systemConfig = systemConfig;
        this.romConfig = romConfig;
        this.logConfig = logConfig;
    }
    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        logConfig.Generate(builder, config);
        romConfig.Generate(builder, config);
        memoryConfig.Generate(builder, config);
        systemConfig.Generate(builder, config);
        return builder.ToString();
    }
}