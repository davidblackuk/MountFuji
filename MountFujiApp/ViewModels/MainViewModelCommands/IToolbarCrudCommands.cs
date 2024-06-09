namespace MountFuji.ViewModels.MainViewModelCommands;

public interface IToolbarCrudCommands
{
    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="ToolbarCrudMainViewModelCommands.Clone"/>.</summary>
    IAsyncRelayCommand<string> CloneCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand"/> instance wrapping <see cref="ToolbarCrudMainViewModelCommands.Save"/>.</summary>
    IAsyncRelayCommand SaveCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="ToolbarCrudMainViewModelCommands.Create"/>.</summary>
    IAsyncRelayCommand CreateCommand { get; }

    /// <summary>Gets an <see cref="global::CommunityToolkit.Mvvm.Input.IAsyncRelayCommand{T}"/> instance wrapping <see cref="ToolbarCrudMainViewModelCommands.Delete"/>.</summary>
    IAsyncRelayCommand<string> DeleteCommand { get; }
}