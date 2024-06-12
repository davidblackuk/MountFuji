namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IScsiCommands
{
    IRelayCommand<int> ClearCommand { get; }

    IAsyncRelayCommand<int> BrowseCommand { get; }
}