using MyAtariCollection.Controls;

namespace MyAtariCollection.Models;

public class FileSystemEntry
{
    private readonly string path;

    public bool IsDirectory { get; }

    
    public FileSystemEntry(string path, bool isDirectory)
    {
        IsDirectory = isDirectory;
        this.path = path;
        this.IsDirectory = isDirectory;
    }

    public string DisplayName => new DirectoryInfo(path).Name;
    
    public string Icon => IsDirectory ? IconFont.Folder_open : IconFont.Description;
}