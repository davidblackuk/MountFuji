namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IGemdosCommands
{
    IAsyncRelayCommand BrowseCommand { get; }

    IRelayCommand ClearCommand { get; }
}