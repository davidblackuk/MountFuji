/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Maui;
using MetroLog.MicrosoftExtensions;
using Microsoft.Maui.Handlers;
using Mopups.Hosting;
using MountFuji.Extensions;

namespace MountFuji;

[ExcludeFromCodeCoverage]
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Roboto-Regular.ttf", "FontRegular");
                fonts.AddFont("Roboto-Bold.ttf", "FontBold");
                fonts.AddFont("Roboto-Italic.ttf", "FontItalic");
                fonts.AddFont("Roboto-Black.ttf", "FontBlack");

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

        
        // hopfully this defers the wiring up of application until after the app is initialized
        builder.Services.AddSingleton<Application>((provider => Application.Current));

        builder.Services.AddStrategies();
        builder.Services.AddViewViewModels();
        builder.Services.AddConfigService();
        builder.Services.AddServices();

        MauiApp built = builder.Build();

        IPreferencesService preferencesService = built.Services.GetService<IPreferencesService>();
        preferencesService.Load();

        IGlobalSystemConfigurationService globalConfigService =
            built.Services.GetService<IGlobalSystemConfigurationService>();
        globalConfigService.Load();

        ISystemsService systemsService = built.Services.GetService<ISystemsService>();
        systemsService.Load();

        return built;
    }
}