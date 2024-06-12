using MountFuji.Extensions;

namespace MountFuji.ViewModels.MainViewModelCommands;

public partial class ScsiCommands :MainViewModelCommandsBase,  IScsiCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;

    public ScsiCommands(IFujiFilePickerService fujiFilePicker, IPreferencesService preferencesService, IServiceProvider serviceProvider): base(serviceProvider)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;
    }
    
    [RelayCommand()]
    private async Task Browse(int diskId)
    {
        await fujiFilePicker.PickFile("SCSI Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath(ViewModel.SelectedConfiguration.ScsiImagePaths, diskId,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void Clear(int diskId) =>
        DiskImagePathsExtensions.ClearImagePath(ViewModel.SelectedConfiguration.ScsiImagePaths, diskId);

    
}