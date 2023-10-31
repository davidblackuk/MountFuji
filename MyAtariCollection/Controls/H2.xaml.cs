using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Controls;

public partial class H2 
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(H2), propertyChanged: (OnPropertyChanged));
    
    private static void OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is H2 h)
        {
            h.TextLabel.Text = newvalue as string ?? "";
        }
    }
    
    public H2() => InitializeComponent();

    public string Text
    {
        get => GetValue(TextProperty) as string;
        set => SetValue(TextProperty, value);
    }
}