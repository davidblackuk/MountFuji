using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using MountFuji.Models;
using TinyMvvm;

namespace MountFuji.ViewModels;

public partial class CloneSystemPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;

    public bool Confirmed { get; set; }
    
    [ObservableProperty] AtariConfiguration system;

    [NotifyCanExecuteChangedFor(nameof(MountFuji.ViewModels.CloneSystemPopupViewModel.OkCommand))]
    [ObservableProperty] private string newName;
    
    public CloneSystemPopupViewModel(IPopupNavigation popupNavigation)
    {   
        this.popupNavigation = popupNavigation;
    }
    
    
    [RelayCommand]
    private async Task Cancel()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(OkEnabled))]
    private async Task Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

    private bool OkEnabled()
    {
        return !string.IsNullOrWhiteSpace(NewName);
    }
}