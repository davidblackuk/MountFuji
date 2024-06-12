using MountFuji.Extensions;

namespace MountFuji.ViewModels.MainViewModelCommands;

public partial class IdeCommands :MainViewModelCommandsBase,  IIdeCommands
{
    private readonly IFujiFilePickerService fujiFilePicker;
    private readonly IPreferencesService preferencesService;

    public IdeCommands(IFujiFilePickerService fujiFilePicker, IPreferencesService preferencesService, IServiceProvider serviceProvider): base(serviceProvider)
    {
        this.fujiFilePicker = fujiFilePicker;
        this.preferencesService = preferencesService;
    }
    
    [RelayCommand()]
    private async Task Browse(int diskId)
    {
        await fujiFilePicker.PickFile("IDE Disk Image",
            (filename) =>
                DiskImagePathsExtensions.SetImagePath(ViewModel.SelectedConfiguration.IdeOptions, diskId,
                    filename),
            preferencesService.Preferences.HardDiskFolder);
    }

    [RelayCommand()]
    private void Clear(int diskId) =>
        DiskImagePathsExtensions.ClearImagePath(ViewModel.SelectedConfiguration.IdeOptions, diskId);

}