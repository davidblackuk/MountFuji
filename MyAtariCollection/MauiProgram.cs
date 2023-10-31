global using System;
global using TinyMvvm;
global using CommunityToolkit.Maui;
global using Microsoft.Extensions.Logging;
global using MyAtariCollection.ViewModels;
global using MyAtariCollection.Views;
global using MyAtariCollection.Services;

namespace MyAtariCollection;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseTinyMvvm()
            .UseMauiCommunityToolkit();

        builder.Services.AddTransient<MainView>();
        builder.Services.AddSingleton<MainViewModel>();
        
        builder.Services.AddSingleton<IMachineTemplateService, MachineTemplateService>();
        builder.Services.AddSingleton<ISystemsService, SystemsService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}