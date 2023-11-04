using System.Text;

namespace MyAtariCollection.Services;

public interface ICommandLineOptionsService
{
    string Generate(AtariConfiguration config);
}

public class CommandLineOptionsService : ICommandLineOptionsService
{
    private readonly ISystemOptionsGenerator systemOptions;
    private readonly ICpuOptionsGenerator cpuOptions;

    public CommandLineOptionsService(ISystemOptionsGenerator systemOptions, ICpuOptionsGenerator cpuOptions)
    {
        this.systemOptions = systemOptions;
        this.cpuOptions = cpuOptions;
    }

    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        systemOptions.Generate(config, builder);
        cpuOptions.Generate(config, builder);
        return builder.ToString();
    }
    
}