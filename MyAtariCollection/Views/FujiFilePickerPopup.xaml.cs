using MountFuji.ViewModels;

namespace MountFuji.Views;

public partial class FujiFilePickerPopup 
{
    public FujiFilePickerPopupViewModel ViewModel { get; }

    public FujiFilePickerPopup(FujiFilePickerPopupViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = viewModel;
    }
}