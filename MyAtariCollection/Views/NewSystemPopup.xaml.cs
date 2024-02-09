/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Mopups.Pages;
using MountFuji.ViewModels;

namespace MountFuji.Views;


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
