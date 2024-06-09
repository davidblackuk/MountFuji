namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IGemdosCommands
{
    IAsyncRelayCommand<MainViewModel> BrowseCommand { get; }

    IRelayCommand<MainViewModel> ClearCommand { get; }
}