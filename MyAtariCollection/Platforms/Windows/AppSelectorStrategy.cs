using MyAtariCollection.Services.Filesystem;

namespace MyAtariCollection.Platforms;

public class AppSelectorStrategy: IAppSelectorStrategy
{
    private readonly FujiFilePickerService filePickerService;

    public AppSelectorStrategy(FujiFilePickerService filePickerService)
    {
        this.filePickerService = filePickerService;
    }

    public string Pick()
    {
        return filePickerService.PickFile()
    }
}