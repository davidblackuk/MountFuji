using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Views;

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