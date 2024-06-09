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
    private async Task Browse(MainViewModelDiskId disk)
    {
        await fujiFilePicker.PickFile("IDE Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath(disk.ViewModel.SelectedConfiguration.IdeOptions, disk.Id,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void Clear(MainViewModelDiskId disk) =>
        DiskImagePathsExtensions.ClearImagePath(disk.ViewModel.SelectedConfiguration.IdeOptions, disk.Id);

}