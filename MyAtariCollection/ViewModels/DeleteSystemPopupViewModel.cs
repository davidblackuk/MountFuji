namespace MyAtariCollection.ViewModels;

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
    private async void No()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand]
    private async void Yes()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

   
}