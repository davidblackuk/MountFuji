using Mopups.Pages;

namespace MyAtariCollection.Views;


public partial class NewSystemPopup: PopupPage
{

    public NewSystemPopup(NewSystemViewModelViewModel viewModelViewModel)
	{
		InitializeComponent();
        BindingContext = viewModelViewModel;
        ViewModelViewModel = viewModelViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModelViewModel.SelectFirstTemplate();
    }

    public NewSystemViewModelViewModel ViewModelViewModel { get; }
}
