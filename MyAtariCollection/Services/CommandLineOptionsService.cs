using System.Text;
using MyAtariCollection.Services.OptionsGenerators;

namespace MyAtariCollection.Services;

public interface ICommandLineOptionsService
{
    string Generate(AtariConfiguration config);
}

public class CommandLineOptionsService(ISystemOptionsGenerator systemOptions, ICpuOptionsGenerator cpuOptions,
        IRomOptionsGenerator romOptions, IHardDiskOptionsGenerator hddOptions, IFloppyOptionsGenerator floppyOptions)
    : ICommandLineOptionsService
{
    public string Generate(AtariConfiguration config)
    {
        StringBuilder builder = new StringBuilder();
        systemOptions.Generate(config, builder);
        cpuOptions.Generate(config, builder);
        romOptions.Generate(config, builder);
        hddOptions.Generate(config, builder);
        floppyOptions.Generate(config, builder);
        return builder.ToString();
    }
    
}