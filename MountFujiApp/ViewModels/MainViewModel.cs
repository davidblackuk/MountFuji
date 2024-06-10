/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Collections.ObjectModel;
using AsyncAwaitBestPractices;
using Microsoft.Extensions.Logging;
using MountFuji.Controls;
using MountFuji.Services.UpdatesService;
using MountFuji.ViewModels.MainViewModelCommands;
using MountFuji.Views;

namespace MountFuji.ViewModels;

public partial class MainViewModel : TinyViewModel
{
    public IRomCommands RomCommands { get; set; }
    public ICartridgeCommands CartridgeCommands { get; set; }
    public IGemdosCommands GemdosCommands { get; set; }
    public IFloppyCommands FloppyCommands { get; set; }
    public IAcsiCommands AcsiCommands { get; set; }
    public IScsiCommands ScsiCommands { get; set;  }
    public IIdeCommands IdeCommands { get; set;  }
    public IToolbarCommands ToolbarCommands { get; set;  }

    private readonly IConfigFileService configFileService;
    private readonly IPopupNavigation popupNavigation;
    private readonly IServiceProvider serviceProvider;
    private readonly IPreferencesService preferencesService;
    private readonly ISystemsService systemsService;
    private readonly ILogger<MainViewModel> log;
    private readonly IAvailableUpdatesService updateService;
    
    public MainViewModel(IConfigFileService configFileService,
        IPopupNavigation popupNavigation,
        IServiceProvider serviceProvider,
        IPreferencesService preferencesService,
        ISystemsService systemsService,
        ILogger<MainViewModel> log,         
        IAvailableUpdatesService updateService,
        
        IRomCommands romCommands,
        ICartridgeCommands cartridgeCommands,
        IGemdosCommands gemdosCommands,
        IFloppyCommands floppyCommands,
        IAcsiCommands acsiCommands,
        IScsiCommands scsiCommands,
        IIdeCommands ideCommands,
        IToolbarCommands toolbarCommands
        )
    {
        RomCommands = romCommands;
        CartridgeCommands = cartridgeCommands;
        GemdosCommands = gemdosCommands;
        FloppyCommands = floppyCommands;
        AcsiCommands = acsiCommands;
        ScsiCommands = scsiCommands;
        IdeCommands = ideCommands;
        ToolbarCommands = toolbarCommands;
        this.configFileService = configFileService;
        this.popupNavigation = popupNavigation;
        this.serviceProvider = serviceProvider;
        this.preferencesService = preferencesService;
        this.systemsService = systemsService;
        this.log = log;
        this.updateService = updateService;
        
        UpdateSystemsFromService();
        CheckForUpdate().SafeFireAndForget();

    }
    
    
    private async Task CheckForUpdate()
    {
        var updateInfo = await updateService.CheckForUpdate();
        UpdateAvailable = updateInfo.IsUpdateAvailable;
    }

    public string ThemeIcon => preferencesService.GetTheme() == AppTheme.Dark ? IconFont.Dark_mode : IconFont.Light_mode;
    
    public ObservableCollection<AtariConfiguration> Systems { get; } = new();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasSelectedConfig))]
    private AtariConfiguration selectedConfiguration = AtariConfiguration.Empty;

    [ObservableProperty] private SectionVisibility sectionVisibility = new SectionVisibility();

    [ObservableProperty] private int numberOfSystems;

    [ObservableProperty] private bool updateAvailable;
    
    [ObservableProperty]  private bool isDirty;

    
    public bool HasSelectedConfig =>
        SelectedConfiguration is not null && SelectedConfiguration.Id != AtariConfiguration.Empty.Id;


    private IDispatcherTimer singleShotTimer;
    private IDispatcherTimer isDirtyTimer;

    public override Task OnFirstAppear()
    {
        if (Systems is not null && Systems.Count > 0)
        {
            SelectedConfiguration = Systems.First();
        }

        ToolbarCommands.RunCommand.NotifyCanExecuteChanged();

        SetupSingleShotTimer();
        SetupIsDirtyTimer();
        return base.OnFirstAppear();
    }
    

    #region ----- APPLICATION TOOL BAR -----

   

    [RelayCommand]
    private async Task ImportHatariConfig()
    {
        ImportSystemPopup popup = serviceProvider.GetService<ImportSystemPopup>();

        await popupNavigation.PushAsync(popup);

        popup.Disappearing += async (sender, args) =>
        {
            if (!popup.ViewModel.Confirmed) return;
           AtariConfiguration clone =
                await systemsService.Import(popup.ViewModel.FileName, popup.ViewModel.DisplayName); 
            UpdateSystemsFromService();
            SelectedConfiguration = clone;
        };
    }
    
    [RelayCommand]
    private async Task OpenGlobalKeyboardConfigPopup()
    { 
        GlobalKeyboardConfigurationPopup popup = serviceProvider.GetService<GlobalKeyboardConfigurationPopup>();
        await popupNavigation.PushAsync(popup);
    }
    
    [RelayCommand]
    private Task Reordered()
    {
        ReorderServicesFromDisplayOrder();
        return Task.CompletedTask;
    }

    [RelayCommand]
    private void SwapTheme()
    {
        preferencesService.ToggleTheme();
        OnPropertyChanged(nameof(ThemeIcon));
    }
    
   
    
    #endregion 
    
    #region ---- HELPERS ----

    /// <summary>
    /// Updates the systems in the MainViewModel from the service.
    /// </summary>
    internal void UpdateSystemsFromService()
    {
        Systems.Clear();
        int count = 0;
        foreach (var system in systemsService.All())
        {
            Systems.Add(system);
            count++;
        }

        NumberOfSystems = count;
    }


    /// <summary>
    /// Reorders the systems in the store based on their display order on screen.
    /// </summary>
    private void ReorderServicesFromDisplayOrder()
    {
        var ids = Systems.Select<AtariConfiguration, string>(s => s.Id).ToList();
        systemsService.ReorderByIds(ids);
    }

    /// <summary>
    /// Every second or so update the disclosure indicator for unsaved changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IsDirtyTimerTick(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            IsDirty = systemsService.IsDirty;
        });
    }

    private void SingleShotSingleShotTimerTick(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            // this should be backed out when MAUI fixes initial selection on windows
            if (Systems is not null && Systems.Count > 0 && SelectedConfiguration is null)
            {
                SelectedConfiguration = Systems.First();
            }
        });
    }
    
    /// <summary>
    /// Initialize the one time timer, that sets the selected item to the first in the list
    /// </summary>
    private void SetupSingleShotTimer()
    {
        singleShotTimer = Application.Current.Dispatcher.CreateTimer();
        singleShotTimer.Interval = TimeSpan.FromSeconds(1);
        singleShotTimer.IsRepeating = false;
        singleShotTimer.Tick += SingleShotSingleShotTimerTick;
        singleShotTimer.Start();
    }

    /// <summary>
    /// Set up a timer that periodically checks if the is dirty timer needs to be updated
    /// </summary>
    private void SetupIsDirtyTimer()
    {
        isDirtyTimer = Application.Current.Dispatcher.CreateTimer();
        isDirtyTimer.Interval = TimeSpan.FromSeconds(1);
        isDirtyTimer.IsRepeating = true;
        isDirtyTimer.Tick += IsDirtyTimerTick;
        isDirtyTimer.Start();
    }


    #endregion
}