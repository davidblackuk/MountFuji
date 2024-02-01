using MountFuji;
using MountFuji.Services.Filesystem;

namespace MountFuji.Platforms;

public class AppSelectorStrategy: IAppSelectorStrategy
{
    private readonly IFujiFilePickerService pickerService;

    public AppSelectorStrategy(IFujiFilePickerService pickerService)
    {
        this.pickerService = pickerService;
    }
    
    public async Task<string> SelectApplication(string title, Action<string> complete)
    {
        return await pickerService.PickFile(title, complete,
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
    }
}