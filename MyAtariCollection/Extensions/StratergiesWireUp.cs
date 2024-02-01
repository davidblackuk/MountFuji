using MountFuji.Platforms;

namespace MountFuji.Extensions;

public static class StratergiesWireUp
{
    /// <summary>
    /// Adds strategies that handle different logic between MACs and PCs
    /// </summary>
    /// <param name="services"></param>
    public static void AddStrategies(this IServiceCollection services)
    {
        services.AddTransient<IAppSelectorStrategy, AppSelectorStrategy>();
    }
}