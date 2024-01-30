using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Views;

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