using MountFuji.Extensions;

namespace MountFuji.ViewModels.MainViewModelCommands;

public partial class ScsiCommands : IScsiCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;

    public ScsiCommands(IFujiFilePickerService fujiFilePicker, IPreferencesService preferencesService)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;
    }
    
    [RelayCommand()]
    private async Task Browse(MainViewModelItemId item)
    {
        await fujiFilePicker.PickFile("SCSI Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath(item.ViewModel.SelectedConfiguration.ScsiImagePaths, item.Id,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void Clear(MainViewModelItemId item) =>
        DiskImagePathsExtensions.ClearImagePath(item.ViewModel.SelectedConfiguration.ScsiImagePaths, item.Id);

    
}