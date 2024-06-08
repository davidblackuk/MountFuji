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

namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IGemdosCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="GemdosCommands.Browse"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::MountFuji.ViewModels.MainViewModel> BrowseCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand{T}"/> instance wrapping <see cref="GemdosCommands.Clear"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand<global::MountFuji.ViewModels.MainViewModel> ClearCommand { get; }
}

public partial class GemdosCommands : IGemdosCommands
{
    private readonly IPreferencesService preferencesService;
    private readonly IFujiFilePickerService fujiFilePicker;

    public GemdosCommands(IPreferencesService preferencesService,
        IFujiFilePickerService fujiFilePicker)
    {
        this.preferencesService = preferencesService;
        this.fujiFilePicker = fujiFilePicker;
    }

    [RelayCommand()]
    private async Task Browse(MainViewModel vm)
    {
        await fujiFilePicker.PickFolder("GEMDOS Folder",
            (filename) => vm.SelectedConfiguration.GdosDriveOptions.GemdosFolder = filename,
            preferencesService.Preferences.GemDosFolder);
    }

    [RelayCommand()]
    private void Clear(MainViewModel vm) => vm.SelectedConfiguration.GdosDriveOptions.GemdosFolder = string.Empty;
}