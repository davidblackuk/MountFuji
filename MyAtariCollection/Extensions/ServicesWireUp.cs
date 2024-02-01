using MountFuji.Services;
using MountFuji.Services.Filesystem;

namespace MountFuji.Extensions;

public static class ServicesWireUp
{
    /// <summary>
    /// Adds all the services required by the app, for manipulating
    /// templates, preferences, file pickers, persistence and config
    /// </summary>
    /// <param name="services"></param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IMachineTemplateService, MachineTemplateService>();
        services.AddSingleton<SystemsService>();
        services.AddSingleton<IPreferencesService, PreferencesService>();
        services.AddSingleton<IFujiFilePickerService, FujiFilePickerService>();
        services.AddTransient<IPersistance, Persistance>();
        services.AddTransient<IConfigFileService, ConfigFileService>();
    }

}