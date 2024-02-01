using MountFuji.Services.ConfigFileSections;

namespace MountFuji.Extensions;

public static class ConfigWireUp
{
    /// <summary>
    /// Adds support for config file processing, import and export
    /// </summary>
    /// <param name="services"></param>
    public static void AddConfiService(this IServiceCollection services)
    {
        services.AddTransient<ILogConfigFileSection, LogConfigFileSection>();
        services.AddTransient<IMemoryConfigFileSection, MemoryConfigFileSection>();
        services.AddTransient<ISystemConfigFileSection, SystemConfigFileSection>();
        services.AddTransient<IRomConfigFileSection, RomConfigFileSection>();
        services.AddTransient<IHardDiskConfigFileSection, HardDiskConfigFileSection>();
        services.AddTransient<IFloppyConfigFileSection, FloppyConfigFileSection>();
        services.AddTransient<IAcsiConfigFileSection, AcsiConfigFileSection>();
        services.AddTransient<IScsiConfigFileSection, ScsiConfigFileSection>();
        services.AddTransient<IIdeConfigFileSection, IdeConfigFileSection>();
        services.AddTransient<IScreenConfigFileSection, ScreenConfigFileSection>();
        services.AddTransient<ISoundConfigFileSection, SoundConfigFileSection>();
        services.AddTransient<IRawHatariConfigFile, RawHatariConfigFile>();
    }
}