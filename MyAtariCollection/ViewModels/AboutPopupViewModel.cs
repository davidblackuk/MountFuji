using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using TinyMvvm;

namespace MountFuji.ViewModels;

public partial class AboutPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;
    
    public AboutPopupViewModel(IPopupNavigation popupNavigation)
    {   
        this.popupNavigation = popupNavigation;
    }
    
    [RelayCommand]
    private async Task Close()
    {
        await popupNavigation.PopAsync();
    }
}