namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IRomCommands
{
    IAsyncRelayCommand<MainViewModel> OpenPickerCommand { get; }

    IAsyncRelayCommand<MainViewModel> BrowseCommand { get; }

    IRelayCommand<MainViewModel> ClearCommand { get; }
}