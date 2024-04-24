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

namespace MountFuji.ViewModels;

public partial class SetShortcutPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;

    [ObservableProperty] private HatariShortcut originalShortcut;

    [ObservableProperty] private string currentShortcut;

    [ObservableProperty] private string key;
    
    public bool Confirmed { get; private set; }
    
    
    public SetShortcutPopupViewModel(IPopupNavigation popupNavigation)
    {
        this.popupNavigation = popupNavigation;
    }

    public override Task OnFirstAppear()
    {
        OnPropertyChanged(nameof(OriginalShortcut));
        return base.OnFirstAppear();
    }

    public void SetInitialState(HatariShortcut shortcut)
    {
        CurrentShortcut = shortcut.DisplayValue;    
        OriginalShortcut = shortcut;
    }
    
    [RelayCommand]
    private async Task Cancel()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async Task Ok()
    {
        Confirmed = true;
        //originalShortcut.DisplayValue = CurrentShortcut;
        await popupNavigation.PopAsync();
    }
}