using System.Text;
using MyAtariCollection.Services.OptionsGenerators;

namespace MyAtariCollection.Services;

public interface ICommandLineOptionsService
{
    string Generate(AtariConfiguration config);
}

public class CommandLineOptionsService : ICommandLineOptionsService
{
    private readonly ISystemOptionsGenerator systemOptions;
    private readonly ICpuOptionsGenerator cpuOptions;
    private readonly IRomOptionsGenerator romOptions;
    private readonly IHardDiskOptionsGenerator hddOptions;

    public CommandLineOptionsService(ISystemOptionsGenerator systemOptions, ICpuOptionsGenerator cpuOptions, 
        IRomOptionsGenerator romOptions, IHardDiskOptionsGenerator hddOptions)
    {
        this.systemOptions = systemOptions;
        this.cpuOptions = cpuOptions;
        this.romOptions = romOptions;
        this.hddOptions = hddOptions;
    }

    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        systemOptions.Generate(config, builder);
        cpuOptions.Generate(config, builder);
        romOptions.Generate(config, builder);
        hddOptions.Generate(config, builder);
        return builder.ToString();
    }
    
}