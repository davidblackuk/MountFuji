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

using Microsoft.Extensions.Logging;

namespace MountFuji.ViewModels;

public enum PickerType
{
    File,
    Folder
}

/// <summary>
/// The view model for the FujiFilePickerPopup View.
/// </summary>
public partial class FujiFilePickerPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;
    private readonly ILogger<FujiFilePickerPopupViewModel> log;

    public bool Confirmed { get; set; }

    [ObservableProperty] public string currentFolder;
    [ObservableProperty] private string title;
    [ObservableProperty] private PickerType pickerType;
    [ObservableProperty] private IEnumerable<FileSystemEntry> entries = new List<FileSystemEntry>();
    
    [NotifyCanExecuteChangedFor(nameof(MountFuji.ViewModels.FujiFilePickerPopupViewModel.OkCommand))]
    [ObservableProperty] private FileSystemEntry selectedEntry;


    /// <summary>
    /// The view model for the FujiFilePickerPopup View.
    /// </summary>
    public FujiFilePickerPopupViewModel(IPopupNavigation popupNavigation, ILogger<FujiFilePickerPopupViewModel> log)
    {
        this.popupNavigation = popupNavigation;
        this.log = log;
    }

    /// <summary>
    /// Sets the initial folder for the FujiFilePickerPopupViewModel.
    /// </summary>
    /// <param name="initialFolder">The initial folder to set.</param>
    public void SetInitialFolder(string initialFolder)
    {
        SetCurrentWorkingDirectory(initialFolder);
    }


    /// <summary>
    /// Sets the current working directory and updates the file/folder entries.
    /// </summary>
    /// <param name="folder">The path of the folder to set as the current working directory.</param>
    private void SetCurrentWorkingDirectory(string folder)
    {
        CurrentFolder = folder;
        List<FileSystemEntry> all = new List<FileSystemEntry>();

        string[] dirs = [];
        try
        {
            dirs = Directory.GetDirectories(folder);
        }
        catch (Exception e)
        {
            log.LogError(e, "Could not iterate folders in the Fuji picker");
        }

        DirectoryInfo info = new DirectoryInfo(folder);
        if (info.Parent != null)
        {
            all.Add(new FileSystemEntry(folder, EntryType.ParentNavigation));
        }
        
        
        Array.Sort(dirs, String.Compare);
        all.AddRange(dirs.Select(dir => new FileSystemEntry(dir, EntryType.Folder)));

        if (PickerType == PickerType.File)
        {
            string [] files = [];
            // TODO is the *.* on windows and * on mac???

            try
            {
                files = Directory.GetFiles(folder, "*", new EnumerationOptions()
                {
                    ReturnSpecialDirectories = false,
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = false,
                });
                files = Directory.GetFiles(folder);

            }
            catch (Exception e)
            {
                log.LogError(e, "Could not iterate files in the Fuji picker");
            }
            
  
            Array.Sort(files, String.Compare);
            all.AddRange(files.Select(dir => new FileSystemEntry(dir, EntryType.File)));
        }
        Entries = all.ToArray();
    }

    /// <summary>
    /// Handles the selection changed event for the file picker.
    /// </summary>
    /// <returns>A task representing the completion of the selection changed event.</returns>
    [RelayCommand]
    private Task SelectionChanged()
    {
        switch (SelectedEntry.EntryType)
        {
            case EntryType.ParentNavigation:
                DirectoryInfo info = new DirectoryInfo(SelectedEntry.Path);
                if (info.Parent != null)
                {
                    SetCurrentWorkingDirectory(info.Parent.FullName);
                }
                break;
            case EntryType.Folder:
                SetCurrentWorkingDirectory(SelectedEntry.Path);
                break;
            case EntryType.File:
                break;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Cancels the current operation and closes the popup.
    /// </summary>
    /// <returns>A task representing the async operation.</returns>
    [RelayCommand]
    private async Task Cancel()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    /// <summary>
    /// Executes the Ok command to confirm the selection and close the file picker popup.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [RelayCommand(CanExecute = nameof(HasValidData))]
    private async Task Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }


    /// <summary>
    /// Checks if the data is valid for the file picker popup and that the OK Button should be enabled.
    /// </summary>
    /// <returns>True if the data is valid; otherwise, false.</returns>
    private bool HasValidData()
    {
        if (this.PickerType == PickerType.File)
        {
            return SelectedEntry != null && SelectedEntry.EntryType == EntryType.File;
        }
        return true;
    }


}