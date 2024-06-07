// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maui.BindableProperty.Generator.Core;

namespace MountFuji.Controls;

public partial class BasicDisclosureIndicator : ContentView
{
#pragma warning disable CS0169
    
    [AutoBindable] private bool indicatorVisible;
    
    [AutoBindable] private string toolTipText;

    [AutoBindable(DefaultValue = "new SolidColorBrush(Colors.Red)") ] private Brush indicatorColor;
    
#pragma warning restore CS0169
    
    public BasicDisclosureIndicator()
    {
        InitializeComponent();
    }
}