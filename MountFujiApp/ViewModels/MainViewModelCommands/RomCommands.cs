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
using MountFuji.Views;

namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IRomCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="RomCommands.OpenPicker"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::MountFuji.ViewModels.MainViewModel> OpenPickerCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="RomCommands.Browse"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::MountFuji.ViewModels.MainViewModel> BrowseCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand{T}"/> instance wrapping <see cref="RomCommands.Clear"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand<global::MountFuji.ViewModels.MainViewModel> ClearCommand { get; }
}

public partial class RomCommands : IRomCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;
    private readonly IServiceProvider serviceProvider;
    private readonly IPopupNavigation popupNavigation;
    private readonly ILogger<MainViewModel> log;

    public RomCommands(IFujiFilePickerService fujiFilePicker, 
        IPreferencesService preferencesService, IServiceProvider serviceProvider, IPopupNavigation popupNavigation, 
        ILogger<MainViewModel> log)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;
        this.serviceProvider = serviceProvider;
        this.popupNavigation = popupNavigation;
        this.log = log;
    }
    
    [RelayCommand]
    private void Clear(MainViewModel viewModel)
    {
        viewModel.SelectedConfiguration.RomImage = String.Empty;
        viewModel.RunCommand.NotifyCanExecuteChanged();
    }
    
    [RelayCommand()]
    private async Task Browse(MainViewModel viewModel)
    {
        await fujiFilePicker.PickFile("ROM Image", (filename) =>
            {
                viewModel.SelectedConfiguration.RomImage = filename;
                viewModel.RunCommand.NotifyCanExecuteChanged();
            },
            preferencesService.Preferences.RomFolder);
    }

    [RelayCommand()]
    private async Task OpenPicker(MainViewModel viewModel)
    {
        RomPickerPopup popup = serviceProvider.GetService<RomPickerPopup>();

        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            Rom rom = popup.ViewModel.SelectedRom;
            viewModel.SelectedConfiguration.RomImage = rom!.Path;
            viewModel.RunCommand.NotifyCanExecuteChanged();
            log.LogInformation("Rom Selected via the ROM Picker {ROM}", rom);
        };
    }
}