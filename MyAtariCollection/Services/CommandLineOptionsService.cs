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

    public CommandLineOptionsService(ISystemOptionsGenerator systemOptions, ICpuOptionsGenerator cpuOptions, IRomOptionsGenerator romOptions)
    {
        this.systemOptions = systemOptions;
        this.cpuOptions = cpuOptions;
        this.romOptions = romOptions;
    }

    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        systemOptions.Generate(config, builder);
        cpuOptions.Generate(config, builder);
        romOptions.Generate(config, builder);
        return builder.ToString();
    }
    
}