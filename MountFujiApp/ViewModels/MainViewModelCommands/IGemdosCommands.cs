namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IGemdosCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="GemdosCommands.Browse"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand<global::MountFuji.ViewModels.MainViewModel> BrowseCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand{T}"/> instance wrapping <see cref="GemdosCommands.Clear"/>.</summary>
    global::CommunityToolkit.Mvvm.Input.IRelayCommand<global::MountFuji.ViewModels.MainViewModel> ClearCommand { get; }
}