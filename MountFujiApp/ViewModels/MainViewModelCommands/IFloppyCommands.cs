namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IFloppyCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IRelayCommand{T}"/> instance wrapping <see cref="FloppyCommands.Clear"/>.</summary>
    IRelayCommand<MainViewModelDiskId> ClearCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="FloppyCommands.Browse"/>.</summary>
    IAsyncRelayCommand<MainViewModelDiskId> BrowseCommand { get; }
}