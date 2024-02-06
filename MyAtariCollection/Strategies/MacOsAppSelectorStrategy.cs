namespace MountFuji.Strategies;

public class MacOsAppSelectorStrategy : IAppSelectorStrategy
{
    private readonly IFujiFilePickerService pickerService;

    public MacOsAppSelectorStrategy(IFujiFilePickerService pickerService)
    {
        this.pickerService = pickerService;
    }
    
    public async Task<string> SelectApplication(string title, Action<string> complete)
    {
        return await pickerService.PickFolder(title, complete,
            Environment.GetFolderPath(Environment.SpecialFolder.Programs));
    }
}
