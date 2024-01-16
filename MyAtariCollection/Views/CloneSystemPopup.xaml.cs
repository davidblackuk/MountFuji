using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Views;

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