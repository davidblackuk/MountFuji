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

public partial class AboutPopupViewModel: TinyViewModel
{
    private readonly IPersistance persistance;
    private readonly IPopupNavigation popupNavigation;
    
    public string Version => AppInfo.VersionString;
    public string BuildInfo => AppInfo.BuildString;
    public string MountFujiFolder => persistance.MountFujiFolder;
    
    public string P => Path.Combine(FileSystem.AppDataDirectory, "fuji");
    
    public AboutPopupViewModel(IPopupNavigation popupNavigation, IPersistance persistance)
    {
        this.popupNavigation = popupNavigation;
        this.persistance = persistance;
    }
    
    [RelayCommand]
    private async Task Close()
    {
        await popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async Task OpenUrl(string url)
    {
        await Launcher.OpenAsync(new Uri(url));
    }
    
    [RelayCommand]
    private async Task OpenDataFolder()
    {
        await Launcher.OpenAsync($"file://{persistance.MountFujiFolder}");
    }
}

