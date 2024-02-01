using MountFuji.ViewModels;

namespace MountFuji.Views;

public partial class DeleteSystemPopup 
{
    public DeleteSystemPopupViewModel ViewModel { get; }
    
    public DeleteSystemPopup(DeleteSystemPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
    }
}