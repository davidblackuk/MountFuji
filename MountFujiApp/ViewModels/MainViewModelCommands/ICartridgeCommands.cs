namespace MountFuji.ViewModels.MainViewModelCommands;

public interface ICartridgeCommands
{
    IRelayCommand ClearCommand { get; }

    IAsyncRelayCommand BrowseCommand { get; }
}