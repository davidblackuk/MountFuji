using MountFuji.Strategies;

namespace MountFuji.Extensions;

public static class StratergiesWireUp
{
    /// <summary>
    /// Adds strategies that handle different logic between MACs and PCs
    /// </summary>
    /// <param name="services"></param>
    public static void AddStrategies(this IServiceCollection services)
    {

#if MACCATALYST
        AddMacStrategies(services);
#elif WINDOWS
        AddWindowsStrategies(services);
#else
        throw new NotImplementedException("Unknow platform, can't wire up strategies");
#endif

    }

    private static void AddMacStrategies(IServiceCollection services)
    {
        services.AddTransient<IAppSelectorStrategy, MacOsAppSelectorStrategy>();
    }

    private static void AddWindowsStrategies(IServiceCollection services)
    {
        services.AddTransient<IAppSelectorStrategy, WindowsAppSelectorStrategy>();
    }


}