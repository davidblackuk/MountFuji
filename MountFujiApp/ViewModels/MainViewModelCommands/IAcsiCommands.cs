namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IAcsiCommands
{
    IRelayCommand<int> ClearCommand { get; }

    IAsyncRelayCommand<int> BrowseCommand { get; }
}