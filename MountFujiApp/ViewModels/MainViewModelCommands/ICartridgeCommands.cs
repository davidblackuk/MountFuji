namespace MountFuji.ViewModels.MainViewModelCommands;

public interface ICartridgeCommands
{
    IRelayCommand<MainViewModel> ClearCommand { get; }

    IAsyncRelayCommand<MainViewModel> BrowseCommand { get; }
}