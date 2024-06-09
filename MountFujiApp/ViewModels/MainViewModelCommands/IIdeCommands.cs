namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IIdeCommands
{
    IRelayCommand<MainViewModelDiskId> ClearCommand { get; }

    IAsyncRelayCommand<MainViewModelDiskId> BrowseCommand { get; }
}