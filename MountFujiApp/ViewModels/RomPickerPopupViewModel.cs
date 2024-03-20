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

using System.Collections.ObjectModel;

namespace MountFuji.ViewModels;

public partial class RomPickerPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;
    private readonly IRomService romService;
    public bool Confirmed { get; set; }

    public IEnumerable<Rom> Roms { get; private set; }

#nullable enable
    [NotifyCanExecuteChangedFor(nameof(MountFuji.ViewModels.CloneSystemPopupViewModel.OkCommand))]
    [ObservableProperty]
    private Rom? selectedRom;
#nullable disable
    
    
    public RomPickerPopupViewModel( IPopupNavigation popupNavigation, IRomService romService)
    {
        this.popupNavigation = popupNavigation;
        this.romService = romService;

        this.Roms = this.romService.GetRoms();

    }

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


    private bool HasValidData()
    {
        return SelectedRom != null;
    }
}