using MountFuji.ViewModels;

namespace MountFuji.Views;

public partial class CloneSystemPopup 
{
    public CloneSystemPopupViewModel ViewModel { get; }
    
    public CloneSystemPopup(CloneSystemPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
    }
}