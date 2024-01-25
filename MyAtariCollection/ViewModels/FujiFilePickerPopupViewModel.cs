
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyAtariCollection.ViewModels;

public enum PickerType
{
    File,
    Folder
}

public partial class FujiFilePickerPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;

    public bool Confirmed { get; set; }

    [ObservableProperty] public string currentFolder;
    //[ObservableProperty] private string selectedFile;
    [ObservableProperty] private string title;
    [ObservableProperty] private PickerType pickerType;
    [ObservableProperty] private IEnumerable<FileSystemEntry> entries = new List<FileSystemEntry>();
    
    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    [ObservableProperty] private FileSystemEntry selectedEntry;

    

    public FujiFilePickerPopupViewModel(IPopupNavigation popupNavigation)
    {   
        this.popupNavigation = popupNavigation;
    }
    
    public void SetInitialFolder(string initialFolder)
    {
        SetCurrentWorkingDirectory(initialFolder);
    }

    private void SetCurrentWorkingDirectory(string folder)
    {
        CurrentFolder = folder;
        List<FileSystemEntry> all = new List<FileSystemEntry>();

        var dirs = Directory.GetDirectories(folder);
        

        DirectoryInfo info = new DirectoryInfo(folder);
        if (info.Parent != null)
        {
            all.Add(new FileSystemEntry(folder, EntryType.ParentNavigation));
        }
        
        
        Array.Sort(dirs, String.Compare);
        all.AddRange(dirs.Select(dir => new FileSystemEntry(dir, EntryType.Folder)));

        if (PickerType == PickerType.File)
        {
            // TODO is the *.* on windows and * on mac???
            var files = Directory.GetFiles(folder, "*", new EnumerationOptions()
            {
                ReturnSpecialDirectories = false,
                IgnoreInaccessible = true,
                RecurseSubdirectories = false,
            });
            
  
            Array.Sort(files, String.Compare);
            all.AddRange(files.Select(dir => new FileSystemEntry(dir, EntryType.File)));
        }
        Entries = all.ToArray();
        
        
    }

    [RelayCommand]
    private Task SelectionChanged()
    {
        Console.WriteLine($"Selection changed: Dir?: {SelectedEntry.IsDirectory} - {SelectedEntry.DisplayName}");
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

    [RelayCommand]
    private async Task Cancel()
    {
        Confirmed = false;
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
        if (this.PickerType == PickerType.File)
        {
            return SelectedEntry != null && SelectedEntry.EntryType == EntryType.File;
        }
        return true;
    }


}