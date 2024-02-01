using MountFuji.ViewModels;

namespace MountFuji.Views;

public partial class PreferencesPopup 
{
    public PreferencesPopup(PreferencesPopupViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
        
    }

    public PreferencesPopupViewModel ViewModel { get; set; }
}