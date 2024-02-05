using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MountFuji.ViewModels;

namespace MountFuji.Views;

public partial class AboutPopup 
{
    public AboutPopup(AboutPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}