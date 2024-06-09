namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IScsiCommands
{
    IRelayCommand<MainViewModelDiskId> ClearCommand { get; }

    IAsyncRelayCommand<MainViewModelDiskId> BrowseCommand { get; }
}