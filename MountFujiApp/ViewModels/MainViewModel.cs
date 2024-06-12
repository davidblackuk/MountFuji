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
using MountFuji.Controls;
using MountFuji.Services.UpdatesService;
using MountFuji.ViewModels.MainViewModelCommands;

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
    
    private readonly IPreferencesService preferencesService;
    private readonly ISystemsService systemsService;
    private readonly IAvailableUpdatesService updateService;
    
    public MainViewModel(
        IPreferencesService preferencesService,
        ISystemsService systemsService,
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
        this.preferencesService = preferencesService;
        this.systemsService = systemsService;
        this.updateService = updateService;
        
        UpdateSystemsFromService();
        CheckForUpdate().SafeFireAndForget();
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

    /// <summary>
    /// Handles initialization of the MainView, sets the first system in the system selector as the current one
    /// and initilizes various timers.
    /// </summary>
    /// <returns></returns>
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
    
   /// <summary>
   /// Handle the end of a drag and drop operation on the system selector by reordering
   /// the systems in the store based on their display order on screen.
   /// </summary>
    [RelayCommand]
    private Task Reordered()
    {
        var ids = Systems.Select<AtariConfiguration, string>(s => s.Id).ToList();
        systemsService.ReorderByIds(ids);
        return Task.CompletedTask;
    }
   
    /// <summary>
    /// Toggle the application theme between light and dak modes
    /// </summary>
    [RelayCommand()]
    private void SwapTheme()
    {
        preferencesService.ToggleTheme();
        OnPropertyChanged(nameof(ThemeIcon));
    }
    
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

    /// <summary>
    /// Checks if an update is available based on the current version by making a call to a GitHub API.
    /// </summary>
    /// <returns>A tuple reflecting whether an update is available and, if so, the latest version number.</returns>
    private async Task CheckForUpdate()
    {
        var updateInfo = await updateService.CheckForUpdate();
        UpdateAvailable = updateInfo.IsUpdateAvailable;
    }
    
    #endregion
}