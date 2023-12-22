namespace MyAtariCollection.Services.Filesystem;

public interface IFujiFilePickerService
{
    Task<string> Pick(Action<string> complete, string initialFolder = null);
}