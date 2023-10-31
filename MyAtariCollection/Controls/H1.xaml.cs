namespace MyAtariCollection.Controls;

public partial class H1 
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(H1), propertyChanged: (OnPropertyChanged));
    
    private static void OnPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is H1 h1)
        {
             h1.TextLabel.Text = newvalue as string ?? "";
        }
    }
    
    public H1() => InitializeComponent();

    public string Text
    {
        get => GetValue(TextProperty) as string;
        set => SetValue(TextProperty, value);
    }
}