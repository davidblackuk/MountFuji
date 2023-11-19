using Mopups.Pages;

namespace MyAtariCollection.Views;

public partial class NewSystemPopup: PopupPage
{

    public NewSystemPopup(NewSystemPopupViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        ViewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.Foo();
    }

    public NewSystemPopupViewModel ViewModel { get; }
}
