global using System;
global using System.Text;
global using Microsoft.Extensions.Logging;

global using TinyMvvm;
global using System.Windows.Input;
global using Maui.BindableProperty.Generator.Core;

global using MyAtariCollection.Extensions;
global using MyAtariCollection.Services;
global using MyAtariCollection.Models;
global using MyAtariCollection.Views;
global using MyAtariCollection.ViewModels;

global using CommunityToolkit.Maui;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;

global using Mopups.Interfaces;


using CommunityToolkit.Maui.Storage;
using MyAtariCollection.Services.CommandLineArgumentGenerators;
using Mopups.Hosting;
using Mopups.Services;
using MyAtariCollection.Services.ConfigFileSections;

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
            .ConfigureMopups()
            .UseTinyMvvm()
            .UseMauiCommunityToolkit();

        builder.Services.AddTransient<MainView>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<NewSystemPopup>();
        builder.Services.AddTransient<NewSystemPopupViewModel>();

        builder.Services.AddTransient<IFolderPicker>(provider => FolderPicker.Default);
        builder.Services.AddTransient<IFilePicker>(provider => FilePicker.Default);

        builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);

        builder.Services.AddTransient<ISystemCommandLineArguments, SystemCommandLineArguments>();
        builder.Services.AddTransient<ICpuCommandLineArguments, CpuCommandLineArguments>();
        builder.Services.AddTransient<IRomCommandLineArguments, RomCommandLineArguments>();
        builder.Services.AddTransient<IHardDiskCommandLineArguments, HardDiskCommandLineArguments>();
        builder.Services.AddTransient<IFloppyCommandLineArguments, FloppyCommandLineArguments>();

        builder.Services.AddTransient<ILogConfigFileSection, LogConfigFileSection>();
        builder.Services.AddTransient<IMemoryConfigFileSection, MemoryConfigFileSection>();
        builder.Services.AddTransient<ISystemConfigFileSection, SystemConfigFileSection>();
        builder.Services.AddTransient<IRomConfigFileSection, RomConfigFileSection>();
        builder.Services.AddTransient<IAcsiConfigFileSection, AcsiConfigFileSection>();
        builder.Services.AddTransient<IScsiConfigFileSection, ScsiConfigFileSection>();
        builder.Services.AddTransient<IConfigFileService, ConfigFileService>();
        
        builder.Services.AddTransient<ICommandLineOptionsService, CommandLineOptionsService>();
        builder.Services.AddSingleton<IMachineTemplateService, MachineTemplateService>();
        builder.Services.AddSingleton<ISystemsService, SystemsService>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}