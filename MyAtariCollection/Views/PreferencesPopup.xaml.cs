using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Views;

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