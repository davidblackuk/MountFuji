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

using MountFuji.Services.UpdatesService;
using AsyncAwaitBestPractices;
namespace MountFuji.ViewModels;

public partial class AboutPopupViewModel: TinyViewModel
{
    private readonly IPersistence persistence;
    private readonly IAvailableUpdatesService updatesService;
    private readonly IApplicationVersion applicationVersion;
    private readonly IPopupNavigation popupNavigation;
    
    public string Version => applicationVersion.Current.ToString();

    [ObservableProperty] private bool updateAvailable;
    [ObservableProperty] private string updateVersion;
    
    public AboutPopupViewModel(IPopupNavigation popupNavigation, 
        IPersistence persistence, 
        IAvailableUpdatesService updatesService,
        IApplicationVersion applicationVersion)
    {
        this.popupNavigation = popupNavigation;
        this.persistence = persistence;
        this.updatesService = updatesService;
        this.applicationVersion = applicationVersion;
        CheckForUpdate().SafeFireAndForget();
    }

    private async Task CheckForUpdate()
    {
        var updateInfo = await updatesService.CheckForUpdate();
        UpdateAvailable = updateInfo.IsUpdateAvailable;
        if (updateInfo.IsUpdateAvailable)
        {
            UpdateVersion = updateInfo.ToVersion.ToString();
        }
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
        await Launcher.OpenAsync($"file://{persistence.MountFujiFolder}");
    }
    
    
}

