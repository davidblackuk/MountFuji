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

namespace MountFuji.ViewModels.MainViewModelCommands;

public partial class CartridgeCommands : MainViewModelCommandsBase, ICartridgeCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;
    
    public CartridgeCommands(IFujiFilePickerService fujiFilePicker, 
        IPreferencesService preferencesService, 
            IServiceProvider serviceProvider): base(serviceProvider)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;

    }
    
    [RelayCommand()]
    private async Task Browse()
    {
        await fujiFilePicker.PickFile("Cartridge Image", (filename) => ViewModel.SelectedConfiguration.CartridgeImage = filename,
            preferencesService.Preferences.CartridgeFolder);
    }

    [RelayCommand]
    private void Clear()
    {
        ViewModel.SelectedConfiguration.CartridgeImage = String.Empty;
    }
    
}