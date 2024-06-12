namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IRomCommands
{
    IAsyncRelayCommand OpenPickerCommand { get; }

    IAsyncRelayCommand BrowseCommand { get; }

    IRelayCommand ClearCommand { get; }
}