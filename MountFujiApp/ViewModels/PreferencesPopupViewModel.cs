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

namespace MountFuji.ViewModels;

public partial class PreferencesPopupViewModel: TinyViewModel
{
    private readonly IPreferencesService preferencesService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IAppSelectorStrategy appSelector;

    public PreferencesPopupViewModel(IPreferencesService preferencesService,
        IPopupNavigation popupNavigation, 
        IFujiFilePickerService fujiFilePicker,
        IAppSelectorStrategy appSelector)
    {
        this.preferencesService = preferencesService;
        this.popupNavigation = popupNavigation;
        this.fujiFilePicker = fujiFilePicker;
        this.appSelector = appSelector;
        Preferences = preferencesService.Preferences;
    }

    public bool Confirmed { get; set; } 
    
    [ObservableProperty] 
    private ApplicationPreferences preferences;

    
    [RelayCommand]
    private async Task Cancel()
    {
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(HasValidData))]
    private async Task Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

    [RelayCommand()]
    private async Task BrowseRomFolder()
    {
        await fujiFilePicker.PickFolder("Default ROM Folder", (filename) =>
            {
                Preferences.RomFolder = filename;
            });
    }
    

    [RelayCommand()]
    private void ClearRomFolder() => Preferences.RomFolder = string.Empty;


  
    [RelayCommand()]
    private async Task BrowseCartridgeFolder()
    {
        await fujiFilePicker.PickFolder("Default Cartridge Folder", (filename) => Preferences.CartridgeFolder = filename);
    }
  
    [RelayCommand()]
    private void ClearCartridgeFolder() => Preferences.CartridgeFolder = string.Empty;

    
    [RelayCommand()]
    private async Task BrowseFloppyDiskFolder()
    {
        await fujiFilePicker.PickFolder("Default Floppies Folder", (filename) => Preferences.FloppyDiskFolder = filename);
    }
  
    [RelayCommand()]
    private void ClearFloppyDiskFolder() => Preferences.FloppyDiskFolder = string.Empty;

    
    [RelayCommand()]
    private async Task BrowseHardDiskFolder()
    {
        await fujiFilePicker.PickFolder("Default Hard Drive Image Folder", (filename) => Preferences.HardDiskFolder = filename);
    }
  
    [RelayCommand()]
    private void ClearHardDiskFolder() => Preferences.HardDiskFolder = string.Empty;
    

    [RelayCommand()]
    private async Task BrowseGemDosDiskFolder()
    {
        await fujiFilePicker.PickFolder("Default GEMDOS Folder", (filename) => Preferences.GemDosFolder = filename);
    }
  
    [RelayCommand()]
    private void ClearGemDosDiskFolder() => Preferences.GemDosFolder = string.Empty;
  
    [RelayCommand()]
    private async Task BrowseHatariApp()
    {
        await appSelector.SelectApplication("Hatari Executable", 
            (filename) => {
                Preferences.HatariApplication = filename;
                OkCommand.NotifyCanExecuteChanged();
            });
    }
  
    [RelayCommand()]
    private void ClearHatariApp()
    {
        Preferences.HatariApplication = string.Empty;
        OkCommand.NotifyCanExecuteChanged();
    }
    
    [RelayCommand()]
    private async Task BrowseHatariConfigFile()
    {
        await fujiFilePicker.PickFile("Hatari Config file", 
            (filename) => {
                Preferences.HatariConfigFile = filename;
                OkCommand.NotifyCanExecuteChanged();
            });
        
    }
  
    [RelayCommand()]
    private void ClearHatariConfigFile()
    {
        Preferences.HatariConfigFile = string.Empty;
        OkCommand.NotifyCanExecuteChanged();
    }
    
    
    private bool HasValidData()
    {
        return !String.IsNullOrWhiteSpace(Preferences.HatariApplication);
    }
    
}