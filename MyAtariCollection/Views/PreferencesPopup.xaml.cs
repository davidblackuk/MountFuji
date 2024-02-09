using Mopups.Pages;
using MountFuji.ViewModels;

namespace MountFuji.Views;

public interface IPreferencesPopup
{
    
    PreferencesPopupViewModel ViewModel { get; set; }
    
    event EventHandler Disappearing;

    PopupPage AsPopUp();

}

public partial class PreferencesPopup : IPreferencesPopup
{
    public PreferencesPopup(PreferencesPopupViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
        
    }

    public PreferencesPopupViewModel ViewModel { get; set; }
    
    public PopupPage AsPopUp()
    {
        return this;
    }
}