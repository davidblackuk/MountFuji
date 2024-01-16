namespace MyAtariCollection.ViewModels;

public partial class CloneSystemPopupViewModel: TinyViewModel
{
    private readonly IPopupNavigation popupNavigation;

    public bool Confirmed { get; set; }
    
    [ObservableProperty] AtariConfiguration system;

    [NotifyCanExecuteChangedFor(nameof(OkCommand))]
    [ObservableProperty] private string newName;
    
    public CloneSystemPopupViewModel(IPopupNavigation popupNavigation)
    {   
        this.popupNavigation = popupNavigation;
    }
    
    
    [RelayCommand]
    private async void Cancel()
    {
        Confirmed = false;
        await popupNavigation.PopAsync();
    }

    [RelayCommand(CanExecute = nameof(OkEnabled))]
    private async void Ok()
    {
        Confirmed = true;
        await popupNavigation.PopAsync();
    }

    private bool OkEnabled()
    {
        return !string.IsNullOrWhiteSpace(NewName);
    }
}