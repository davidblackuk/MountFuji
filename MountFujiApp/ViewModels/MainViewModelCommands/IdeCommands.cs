using MountFuji.Extensions;

namespace MountFuji.ViewModels.MainViewModelCommands;

public partial class IdeCommands : IIdeCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;

    public IdeCommands(IFujiFilePickerService fujiFilePicker, IPreferencesService preferencesService)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;
    }
    
    [RelayCommand()]
    private async Task Browse(MainViewModelItemId item)
    {
        await fujiFilePicker.PickFile("IDE Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath(item.ViewModel.SelectedConfiguration.IdeOptions, item.Id,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void Clear(MainViewModelItemId item) =>
        DiskImagePathsExtensions.ClearImagePath(item.ViewModel.SelectedConfiguration.IdeOptions, item.Id);

}