using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;
using MountFuji.Extensions;
using MountFuji.Services;
using MountFuji.Services.ConfigFileSections;
using MountFuji.Services.Filesystem;
using MountFuji.ViewModels;
using MountFuji.Views;
using MountFuji.Platforms;
using TinyMvvm;

namespace MountFuji;

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
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIcons");
            }) 
            .ConfigureMopups()
            .UseTinyMvvm()
            .UseMauiCommunityToolkit();

        builder.Logging.AddConsoleLogger(_ => { })
        .AddStreamingFileLogger(options =>
        {
            options.RetainDays = 2;
            options.FolderPath = Path.Combine(FileSystem.AppDataDirectory, "fuji");
        });
        
        
        
        builder.Services.AddStrategies();
        builder.Services.AddViewViewModels();
        builder.Services.AddConfiService();
        builder.Services.AddServices();
        
        MauiApp built =  builder.Build();

        
        IPreferencesService preferencesService = built.Services.GetService<IPreferencesService>();
        preferencesService.Load();

        SystemsService systemsService = built.Services.GetService<SystemsService>();
        systemsService.Load();
        
        return built;
    }
}