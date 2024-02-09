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

namespace MountFuji.Controls;

public partial class SystemEditorExpander : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), 
            typeof(string), 
            typeof(SystemEditorExpander));
 
    public static readonly BindableProperty ExpandedButtonIconProperty =
        BindableProperty.Create(nameof(ExpandedButtonIcon), 
            typeof(string), 
            typeof(SystemEditorExpander));

    public static readonly BindableProperty ExpandedProperty =
        BindableProperty.Create(nameof(Expanded), 
            typeof(bool), 
            typeof(SystemEditorExpander));
    
    public SystemEditorExpander() => InitializeComponent();


    public string Title
    {
        get => GetValue(TitleProperty) as string;
        set => SetValue(TitleProperty, value);
    }

    public string ExpandedButtonIcon
    {
        get => GetValue(ExpandedButtonIconProperty) as string;
        set => SetValue(ExpandedButtonIconProperty, value);
    }

    
    public bool Expanded
    {
        get => (bool)GetValue(ExpandedProperty);

        set
        {
            SetIconFromValue(value);
            SetValue(ExpandedProperty, value);
        }
    }

    private void SetIconFromValue(bool value)
    {
        ExpandedButtonIcon = value ? IconFont.Arrow_circle_down : IconFont.Arrow_circle_right;
    }

    private void ToggleExpandedClicked(object sender, EventArgs e)
    {
        Expanded = !Expanded;
    }

    private void ConfigurationPanelSection_OnLoaded(object sender, EventArgs e)
    {
        SetIconFromValue(Expanded);
    }
}