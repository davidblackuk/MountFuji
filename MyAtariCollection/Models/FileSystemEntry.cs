using MyAtariCollection.Controls;

namespace MyAtariCollection.Models;

public enum EntryType  
{

    ParentNavigation,
    Folder,
    File
    
}

public class FileSystemEntry
{
    public string Path { get; set; }
    public EntryType EntryType { get; private set; }
    
    public FileSystemEntry(string path, EntryType entryType)
    {
        this.Path = path;
        this.EntryType = entryType;
    }

    public string DisplayName => (EntryType == EntryType.ParentNavigation) ? ".." : new DirectoryInfo(Path).Name;

    public bool IsDirectory => EntryType == EntryType.Folder;

    public bool IsParentNavigation => EntryType == EntryType.ParentNavigation;
    
    public string Icon => (EntryType == EntryType.Folder || EntryType == EntryType.ParentNavigation) ? IconFont.Folder_open : IconFont.Description;
}