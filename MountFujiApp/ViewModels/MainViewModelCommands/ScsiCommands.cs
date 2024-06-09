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
    private async Task Browse(MainViewModelDiskId disk)
    {
        await fujiFilePicker.PickFile("SCSI Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath(disk.ViewModel.SelectedConfiguration.ScsiImagePaths, disk.Id,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void Clear(MainViewModelDiskId disk) =>
        DiskImagePathsExtensions.ClearImagePath(disk.ViewModel.SelectedConfiguration.ScsiImagePaths, disk.Id);

    
}