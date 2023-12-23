namespace MyAtariCollection.Services.Filesystem;

public interface IFujiFilePickerService
{
    Task<string> PickFile(string title, Action<string> complete, string initialFolder = null);
    Task<string> PickFolder(string title, Action<string> complete, string initialFolder = null);
}