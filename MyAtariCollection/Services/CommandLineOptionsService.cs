using System.Text;
using MyAtariCollection.Services.CommandLineArgumentGenerators;

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