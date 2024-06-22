// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Microsoft.Extensions.Logging;
using Microsoft.Maui.Platform;
using MountFuji.Models.Keyboard;
using MountFuji.Views;
using KeyboardOptions = MountFuji.Models.Keyboard.KeyboardOptions;

namespace MountFuji.ViewModels;

public partial class GlobalKeyboardOptionsPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;
    private readonly IGlobalSystemConfigurationService globalConfigService;
    private readonly IPreferencesService preferencesService;
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<GlobalKeyboardOptionsPopupViewModel> log;

    private string hatariConfigFilePath = String.Empty;
    public GlobalSystemConfiguration Configuration { get; }
    
    public GlobalKeyboardOptionsPopupViewModel(IPopupNavigation popupNavigation, 
        IGlobalSystemConfigurationService globalConfigService, 
        IPreferencesService preferencesService, 
        IFujiFilePickerService fujiFilePicker,
        IServiceProvider serviceProvider,
        ILogger<GlobalKeyboardOptionsPopupViewModel> log)
    {
        this.popupNavigation = popupNavigation;
        this.globalConfigService = globalConfigService;
        this.preferencesService = preferencesService;
        this.fujiFilePicker = fujiFilePicker;
        this.serviceProvider = serviceProvider;
        this.log = log;
        Configuration = globalConfigService.Configuration;
        hatariConfigFilePath = Path.GetDirectoryName((string)preferencesService.Preferences.HatariConfigFile);
    }
    
        
    [RelayCommand()]
    private async Task BrowseKeyboardMapping()
    {
        await fujiFilePicker.PickFile("Keyboard Mapping File", (filename) => Configuration.KeyboardOptions.MappingFile = filename,
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand]
    private void ClearKeyboardMapping()
    {
        Configuration.KeyboardOptions.MappingFile = String.Empty;
    }

    [RelayCommand]
    private async Task ImportFromKeyboardConfigFile()
    {
        await fujiFilePicker.PickFile("Import global keyboard settings", async (filename) =>
            {
                var newOptions = await globalConfigService.ImportKeyboardOptionsFromConfigFile(filename);
                Configuration.KeyboardOptions = newOptions;
            },
            hatariConfigFilePath);
    }
    
    [RelayCommand]
    private async Task SetKey(HatariShortcut key)
    {
        log.LogInformation("Setting key for {Key}", key);
        ISetShortcutPopupView popup = serviceProvider.GetService<ISetShortcutPopupView>();

        popup.ViewModel.SetInitialState(key);
        await popupNavigation.PushAsync(popup.AsPopUp());
    }
    
    [RelayCommand]
    private async Task Cancel()
    {
        await globalConfigService.LoadAsync();
        await popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async Task Ok()
    {
        await globalConfigService.SaveAsync();
        await popupNavigation.PopAsync();
    }
}