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

using Mopups.Services;
using MountFuji.ViewModels;
using MountFuji.ViewModels.MainViewModelCommands;
using MountFuji.Views;

namespace MountFuji.Extensions;

public static class MvvmWireUp {

    /// <summary>
    /// Adds the Views and ViewModels from MVVM to the app
    /// </summary>
    /// <param name="services"></param>
    public static void AddViewViewModels(this IServiceCollection services)
    {
        services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        
        AddMainViewModel(services);

        services.AddTransient<INewSystemPopup, NewSystemPopup>();
        services.AddTransient<NewSystemViewModel>();
        
        services.AddTransient<IPreferencesPopup, PreferencesPopup>();
        services.AddTransient<PreferencesPopupViewModel>();
        
        services.AddTransient<FujiFilePickerPopup>();
        services.AddTransient<FujiFilePickerPopupViewModel>();
        
        services.AddTransient<ICloneSystemPopup, CloneSystemPopup>();
        services.AddTransient<CloneSystemPopupViewModel>();
        
        services.AddTransient<IDeleteSystemPopup, DeleteSystemPopup>();
        services.AddTransient<DeleteSystemPopupViewModel>();

        services.AddTransient<IImportSystemPopup, ImportSystemPopup>();
        services.AddTransient<ImportSystemPopupViewModel>();
        
        services.AddTransient<AboutPopup>();
        services.AddTransient<AboutPopupViewModel>();  
        
        services.AddTransient<RomPickerPopup>();
        services.AddTransient<RomPickerPopupViewModel>();

        services.AddTransient<IGlobalKeyboardConfigurationPopup, GlobalKeyboardConfigurationPopup>();
        services.AddTransient<GlobalKeyboardOptionsPopupViewModel>();
        
        services.AddTransient<SetShortcutPopupView>();
        services.AddTransient<SetShortcutPopupViewModel>();
        
    }

    private static void AddMainViewModel(IServiceCollection services)
    {
        services.AddSingleton<MainView>();
        services.AddSingleton<MainViewModel>();

        services.AddSingleton<IRomCommands, RomCommands>();
        services.AddSingleton<ICartridgeCommands, CartridgeCommands>();
        services.AddSingleton<IGemdosCommands, GemdosCommands>();
        services.AddSingleton<IFloppyCommands, FloppyCommands>();
        services.AddSingleton<IAcsiCommands, AcsiCommands>();
        services.AddSingleton<IScsiCommands, ScsiCommands>();
        services.AddSingleton<IIdeCommands, IdeCommands>();
        services.AddTransient<IToolbarCommands, ToolbarCommands>();
    }
}