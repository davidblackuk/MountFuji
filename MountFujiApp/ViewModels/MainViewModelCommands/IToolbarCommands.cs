namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IToolbarCommands
{
    IAsyncRelayCommand<string> CloneCommand { get; }

    IAsyncRelayCommand SaveCommand { get; }

    IAsyncRelayCommand CreateCommand { get; }

    IAsyncRelayCommand<string> DeleteCommand { get; }
    
    IAsyncRelayCommand AboutCommand { get; }
    
    IAsyncRelayCommand RunCommand { get; }

    IAsyncRelayCommand EditPreferencesCommand { get; }
}