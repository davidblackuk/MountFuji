namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IIdeCommands
{
    IRelayCommand<int> ClearCommand { get; }

    IAsyncRelayCommand<int> BrowseCommand { get; }
}