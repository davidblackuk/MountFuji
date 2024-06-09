using MountFuji.Views;

namespace MountFuji.ViewModels.MainViewModelCommands;

/// <summary>
/// Application toolbar commands for CRUD operations on systems.
/// </summary>
public partial class ToolbarCrudMainViewModelCommands: MainViewModelCommandsBase, IToolbarCrudCommands
{
    private readonly ISystemsService systemsService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IServiceProvider serviceProvider;
    
    /// <summary>
    /// Application toolbar commands for CRUD operations on systems.
    /// </summary>
    /// <param name="systemsService">The system service for operations</param>
    /// <param name="popupNavigation">popup navigation servicde</param>
    /// <param name="serviceProvider">A service provider to source our popup views from</param>
    public ToolbarCrudMainViewModelCommands(ISystemsService systemsService,  
        IPopupNavigation popupNavigation,  
        IServiceProvider serviceProvider): base(serviceProvider)
    {
        this.systemsService = systemsService;
        this.popupNavigation = popupNavigation;
        this.serviceProvider = serviceProvider;
    }
    
    /// <summary>
    /// Creates and registers a new Atari system.
    /// </summary>
    [RelayCommand]
    private async Task Create()
    {
        var popup = serviceProvider.GetService<INewSystemPopup>();
        await popupNavigation.PushAsync(popup.AsPopUp());

        popup.Disappearing += (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            var system = popup.ViewModel.GetConfiguration();
            systemsService.Add(system);
            ViewModel.UpdateSystemsFromService();
            ViewModel.SelectedConfiguration = system;
        };
    }
    
    /// <summary>
    /// Saves current systems, this will clear the dirty indicator
    /// was causing crashes
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        await systemsService.Save();
    }

    /// <summary>
    /// Deletes a system from the collection and updates the collectionview after a dialog
    /// querying the operation is surfaced and confirmed
    /// </summary>
    /// <param name="id">THe id of the system to delete</param>
    [RelayCommand]
    public async Task Delete(string id)
    {
        AtariConfiguration system = systemsService.Find(id);
        if (system is null) return;
        
        IDeleteSystemPopup popup = serviceProvider.GetService<IDeleteSystemPopup>();
        popup.ViewModel.System = system;
        await popupNavigation.PushAsync(popup.AsPopUp());
        
        popup.Disappearing += (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            int currentIndex = ViewModel.Systems.IndexOf(system);
            int newIndex = currentIndex > 0 ? currentIndex - 1 : 0;
            systemsService.Delete(system.Id);
            ViewModel.UpdateSystemsFromService();
        
            ViewModel.SelectedConfiguration = ViewModel.Systems.Count > 0 ? ViewModel.Systems[newIndex] : AtariConfiguration.Empty;
        };
    }

    /// <summary>
    /// Pops up a dialog to get the name of the cloned system, executes the clone and updates the display
    /// </summary>
    /// <param name="id"></param>
    [RelayCommand]
    private async Task Clone(string id)
    {
        AtariConfiguration system = systemsService.Find(id);
        if (system is null) return;
        
        ICloneSystemPopup popup = serviceProvider.GetService<ICloneSystemPopup>();
        popup.ViewModel.System = system;

        await popupNavigation.PushAsync(popup.AsPopUp());

        popup.Disappearing += (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            AtariConfiguration clone = systemsService.Clone(system, popup.ViewModel.NewName);
            ViewModel.UpdateSystemsFromService();
            ViewModel.SelectedConfiguration = clone;
        };
    }
}