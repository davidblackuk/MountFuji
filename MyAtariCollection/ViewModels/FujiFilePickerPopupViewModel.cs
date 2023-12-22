using UIKit;

namespace MyAtariCollection.ViewModels;


public partial class FujiFilePickerPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;

    public bool Confirmed { get; set; }

    [ObservableProperty] public string selectedFile;

    [ObservableProperty] private IEnumerable<FileSystemEntry> entries = new List<FileSystemEntry>();
    [ObservableProperty] private FileSystemEntry selectedEntry;


    public FujiFilePickerPopupViewModel(IPopupNavigation popupNavigation)
    {   
        this.popupNavigation = popupNavigation;
    }
    
    public void SetInitialFolder(string initialFolder)
    {
        List<FileSystemEntry> all = new List<FileSystemEntry>();
        var dirs = Directory.GetDirectories(initialFolder, "*", new EnumerationOptions()
        {
            ReturnSpecialDirectories = false,
            IgnoreInaccessible = true,
            RecurseSubdirectories = false,
        });
        
        Array.Sort(dirs, String.Compare);
        all.AddRange(dirs.Select(dir => new FileSystemEntry(dir, true)));
        
        var files = Directory.GetFiles(initialFolder, "*.*", new EnumerationOptions()
        {
            ReturnSpecialDirectories = false,
            IgnoreInaccessible   = true,
            RecurseSubdirectories = false,
        });
        Array.Sort(files, String.Compare);
        all.AddRange(files.Select(dir => new FileSystemEntry(dir, false)));

        Entries = all.ToArray();


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
        return true;
    }


}