using Microsoft.Extensions.Logging;
using MountFuji.Views;

namespace MountFuji.ViewModels.MainViewModelCommands;

/// <summary>
/// Application toolbar commands for CRUD operations on systems.
/// </summary>
public partial class ToolbarCommands: MainViewModelCommandsBase, IToolbarCommands
{
    private readonly ISystemsService systemsService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IServiceProvider serviceProvider;
    private readonly IPreferencesService preferencesService;
    private readonly IConfigFileService configFileService;
    private readonly ILogger<MainViewModel> log;

    /// <summary>
    /// Application toolbar commands for CRUD operations on systems.
    /// </summary>
    /// <param name="systemsService">The system service for operations</param>
    /// <param name="popupNavigation">popup navigation servicde</param>
    /// <param name="serviceProvider">A service provider to source our popup views from</param>
    /// <param name="preferencesService">The preferences service to persist system paths etc.</param>
    /// <param name="configFileService">The config file service for saving to hatari.cfg</param>
    /// <param name="log">Q logger to log to.</param>
    public ToolbarCommands(ISystemsService systemsService,  
        IPopupNavigation popupNavigation,  
        IServiceProvider serviceProvider,
        IPreferencesService preferencesService,
        IConfigFileService configFileService,
        ILogger<MainViewModel> log     
        ): base(serviceProvider)
    {
        this.systemsService = systemsService;
        this.popupNavigation = popupNavigation;
        this.serviceProvider = serviceProvider;
        this.preferencesService = preferencesService;
        this.configFileService = configFileService;
        this.log = log;
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
    
    /// <summary>
    /// Display the about dialog.
    /// </summary>
    [RelayCommand]
    public async Task About()
    {
        AboutPopup popup = serviceProvider.GetService<AboutPopup>();

        await popupNavigation.PushAsync(popup);
    }
    
    /// <summary>
    /// Save the currently selected system to hatari.cfg and run Hatari.
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanRun))]
    public async Task Run()
    {
        await configFileService.Save(ViewModel.SelectedConfiguration);
        
        var app = preferencesService.Preferences.HatariApplication;
        var applicationUrl = $"file://{app}";
        log.LogInformation("Launching application: {Url}", applicationUrl);
        await Launcher.Default.OpenAsync(applicationUrl);
    }
    
    /// <summary>
    /// Open a dialog to edit the application preferences
    /// </summary>
    [RelayCommand]
    private async Task EditPreferences()
    {
        var popup = serviceProvider.GetService<IPreferencesPopup>();
        await popupNavigation.PushAsync(popup.AsPopUp());

        popup.Disappearing += async (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
            await preferencesService.SaveAsync();
            RunCommand.NotifyCanExecuteChanged();
        };
    }


    
    private bool CanRun()
    {
        return !string.IsNullOrWhiteSpace(preferencesService.Preferences.HatariApplication) &&
               !string.IsNullOrWhiteSpace(preferencesService.Preferences.HatariConfigFile) &&
               ViewModel.SelectedConfiguration is not null &&
               !string.IsNullOrWhiteSpace(ViewModel.SelectedConfiguration.RomImage);
    }
}