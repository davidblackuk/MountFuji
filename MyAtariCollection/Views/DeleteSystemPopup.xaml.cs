using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Views;

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