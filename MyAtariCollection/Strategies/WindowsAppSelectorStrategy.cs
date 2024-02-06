using MountFuji;

namespace MountFuji.Strategies;

public class WindowsAppSelectorStrategy : IAppSelectorStrategy
{
    private readonly IFujiFilePickerService pickerService;

    public WindowsAppSelectorStrategy(IFujiFilePickerService pickerService)
    {
        this.pickerService = pickerService;
    }
    
    public async Task<string> SelectApplication(string title, Action<string> complete)
    {
        return await pickerService.PickFile(title, complete,
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
    }
}