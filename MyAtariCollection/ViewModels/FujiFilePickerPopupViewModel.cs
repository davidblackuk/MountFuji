using UIKit;

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
        var dirs = Directory.GetDirectories(folder, "*", new EnumerationOptions()
        {
            ReturnSpecialDirectories = false,
            IgnoreInaccessible = true,
            RecurseSubdirectories = false,
        });

        DirectoryInfo info = new DirectoryInfo(folder);
        if (info.Parent != null)
        {
            all.Add(new FileSystemEntry(folder, EntryType.ParentNavigation));
        }
        
        
        Array.Sort(dirs, String.Compare);
        all.AddRange(dirs.Select(dir => new FileSystemEntry(dir, EntryType.Folder)));

        if (PickerType == PickerType.File)
        {
            var files = Directory.GetFiles(folder, "*.*", new EnumerationOptions()
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
    private async void SelectionChanged()
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
    }

    [RelayCommand]
    private async void Cancel()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(HasValidData))]
    private async void Ok()
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