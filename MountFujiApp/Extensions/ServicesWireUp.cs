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
        services.AddSingleton<ISystemsService, SystemsService>();
        services.AddSingleton<IPreferencesService, PreferencesService>();
        services.AddSingleton<IApplicationResolver, ApplicationResolver>();
        services.AddSingleton<IFujiFilePickerService, FujiFilePickerService>();
        services.AddTransient<IPersistence, Persistence>();
        services.AddTransient<IConfigFileService, ConfigFileService>();
        services.AddTransient<IFileSystemService, FileSystemService>();
        services.AddTransient<IRomService, RomService>();
        services.AddSingleton<IGlobalSystemConfigurationService, GlobalSystemConfigurationService>();
    }

}