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

    public NewSystemPopupViewModel ViewModel { get; }
}
