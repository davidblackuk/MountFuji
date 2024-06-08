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

public interface ICartridgeCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand{T}"/> instance wrapping <see cref="CartridgeCommands.Clear"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand<global::MountFuji.ViewModels.MainViewModel> ClearCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="CartridgeCommands.Browse"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::MountFuji.ViewModels.MainViewModel> BrowseCommand { get; }
}

public partial class CartridgeCommands : ICartridgeCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;
    
    public CartridgeCommands(IFujiFilePickerService fujiFilePicker, 
        IPreferencesService preferencesService)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;

    }
    
    [RelayCommand()]
    private async Task Browse(MainViewModel vm)
    {
        await fujiFilePicker.PickFile("Cartridge Image", (filename) => vm.SelectedConfiguration.CartridgeImage = filename,
            preferencesService.Preferences.CartridgeFolder);
    }

    [RelayCommand]
    private void Clear(MainViewModel vm)
    {
        vm.SelectedConfiguration.CartridgeImage = String.Empty;
    }
    
}