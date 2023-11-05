using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Controls.ConfiurationSections;

public partial class ConfigurationGroup : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(ConfigurationPanelSection));

    public ConfigurationGroup()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty) as string;
        set => SetValue(TitleProperty, value);
    }
}