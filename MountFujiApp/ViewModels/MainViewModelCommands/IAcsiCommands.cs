namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IAcsiCommands
{
    IRelayCommand<MainViewModelDiskId> ClearCommand { get; }

    IAsyncRelayCommand<MainViewModelDiskId> BrowseCommand { get; }
}