using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using MountFuji.Models;
using TinyMvvm;

namespace MountFuji.ViewModels;

public partial class DeleteSystemPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;

    public bool Confirmed { get; set; }

    [ObservableProperty] AtariConfiguration system;
    
    
    public DeleteSystemPopupViewModel(IPopupNavigation popupNavigation)
    {   
        this.popupNavigation = popupNavigation;
    }
    
    
    [RelayCommand]
    private async Task No()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async Task Yes()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

   
}