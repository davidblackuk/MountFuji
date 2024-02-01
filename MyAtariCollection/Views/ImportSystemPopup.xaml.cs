using MountFuji.ViewModels;

namespace MountFuji.Views;

public partial class ImportSystemPopup 
{
    public ImportSystemPopupViewModel ViewModel { get; }
    
    public ImportSystemPopup(ImportSystemPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
    }
}