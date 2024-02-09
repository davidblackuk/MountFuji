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


using System.Diagnostics.CodeAnalysis;
using MountFuji.ViewModels;
using MountFuji.Views;

namespace MountFuji.Services.Filesystem;

public class FujiFilePickerService : IFujiFilePickerService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IPopupNavigation popupNavigation;

    private string lastFolderAccessed;
    
    public FujiFilePickerService(IServiceProvider serviceProvider, IPopupNavigation popupNavigation)
    {
        this.serviceProvider = serviceProvider;
        this.popupNavigation = popupNavigation;
    }
    public async Task<string> PickFolder(string title, Action<string> complete, string initialFolder = null)
    {
        return await Initialize(PickerType.Folder, title, complete, initialFolder);
    }
    
    public async Task<string> PickFile([NotNull]string title, [NotNull] Action<string> complete, string initialFolder = null)
    {
        return await Initialize(PickerType.File, title, complete, initialFolder);
    }

    private async Task<string> Initialize(PickerType pickerType, string title, Action<string> complete, string initialFolder)
    {
        string res = null;

        if (String.IsNullOrWhiteSpace(initialFolder))
        {
            initialFolder = String.IsNullOrWhiteSpace(lastFolderAccessed) ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) : lastFolderAccessed;
        }
        
        var popup = serviceProvider.GetService<FujiFilePickerPopup>();
        popup.ViewModel.Title = title;
        popup.ViewModel.PickerType = pickerType;
        popup.ViewModel.SetInitialFolder(initialFolder);
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (complete is not null & popup.ViewModel.Confirmed && pickerType == PickerType.File)
            {
                lastFolderAccessed = popup.ViewModel.CurrentFolder;
                complete(popup.ViewModel.SelectedEntry.Path);
            }
            else  if (complete is not null & popup.ViewModel.Confirmed && pickerType == PickerType.Folder)
            {
                lastFolderAccessed = popup.ViewModel.CurrentFolder;
                complete(popup.ViewModel.CurrentFolder);
            }
        }; 
        
        return res;
    }
}