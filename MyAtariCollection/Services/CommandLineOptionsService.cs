using System.Text;
using MyAtariCollection.Services.CommandLineArgumentGenerators;
using MyAtariCollection.Services.ConfigFileSections;

namespace MyAtariCollection.Services;

public interface ICommandLineOptionsService
{
    string Generate(AtariConfiguration config);
}

public class CommandLineOptionsService(ISystemCommandLineArguments systemArguments, ICpuCommandLineArguments cpuArguments,
        IRomCommandLineArguments romArguments, IHardDiskCommandLineArguments hddArguments, IFloppyCommandLineArguments floppyArguments)
    : ICommandLineOptionsService
{
    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        systemArguments.Generate(config, builder);
        cpuArguments.Generate(config, builder);
        romArguments.Generate(config, builder);
        hddArguments.Generate(config, builder);
        floppyArguments.Generate(config, builder);
        return builder.ToString();
    }
    
}

public interface IConfigFileService
{
    string Generate(AtariConfiguration config);
}

public class ConfigFileService : IConfigFileService
{
    private readonly IMemoryConfigFileSection memoryConfig;
    private readonly ISystemConfigFileSection systemConfig;
    private readonly ILogConfigFileSection logConfig;

    public ConfigFileService(ILogConfigFileSection logConfig, IMemoryConfigFileSection memoryConfig, ISystemConfigFileSection systemConfig)
    {
        this.memoryConfig = memoryConfig;
        this.systemConfig = systemConfig;
        this.logConfig = logConfig;
    }
    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        logConfig.Generate(builder, config);
        memoryConfig.Generate(builder, config);
        systemConfig.Generate(builder, config);
        return builder.ToString();
    }
}