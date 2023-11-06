global using System;
global using System.Text;
global using Microsoft.Extensions.Logging;

global using TinyMvvm;

global using MyAtariCollection.Services;
global using MyAtariCollection.Models;
global using MyAtariCollection.Views;
global using MyAtariCollection.ViewModels;

global using CommunityToolkit.Maui;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Storage;
using MyAtariCollection.Services.OptionsGenerators;

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
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIcons");
            })
            .UseTinyMvvm()
            .UseMauiCommunityToolkit();

        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<MainViewModel>();

        builder.Services.AddTransient<IFolderPicker>(provider => FolderPicker.Default);
        builder.Services.AddTransient<IFilePicker>(provider => FilePicker.Default);
        
        builder.Services.AddTransient<ISystemOptionsGenerator, SystemOptionsGenerator>();
        builder.Services.AddTransient<ICpuOptionsGenerator, CpuOptionsGenerator>();
        builder.Services.AddTransient<IRomOptionsGenerator, RomOptionsGenerator>();

        
        builder.Services.AddTransient<ICommandLineOptionsService, CommandLineOptionsService>();
        builder.Services.AddSingleton<IMachineTemplateService, MachineTemplateService>();
        builder.Services.AddSingleton<ISystemsService, SystemsService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}