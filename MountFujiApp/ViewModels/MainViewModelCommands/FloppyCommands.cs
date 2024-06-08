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

using MountFuji.Extensions;

namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IFloppyCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand{T}"/> instance wrapping <see cref="FloppyCommands.Clear"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand<global::MountFuji.ViewModels.MainViewModelCommands.MainViewModelItemId> ClearCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="FloppyCommands.Browse"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::MountFuji.ViewModels.MainViewModelCommands.MainViewModelItemId> BrowseCommand { get; }
}

public partial class FloppyCommands : IFloppyCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;
    
    public FloppyCommands(IFujiFilePickerService fujiFilePicker, IPreferencesService preferencesService)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;
    }
    
      
    [RelayCommand()]
    private async Task Browse(MainViewModelItemId item)
    {
        await fujiFilePicker.PickFile("Floppy Disk Image",
            (filename) => DiskImagePathsExtensions.SetImagePath((FloppyDriveOptions)item.ViewModel.SelectedConfiguration.FloppyOptions,
                item.Id, filename),
            preferencesService.Preferences.FloppyDiskFolder);
    }

    [RelayCommand()]
    private void Clear(MainViewModelItemId item) =>
        DiskImagePathsExtensions.ClearImagePath((FloppyDriveOptions)item.ViewModel.SelectedConfiguration.FloppyOptions, item.Id);

    
}