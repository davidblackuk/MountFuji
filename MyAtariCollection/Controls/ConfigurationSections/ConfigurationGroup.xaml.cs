namespace MountFuji.Controls.ConfigurationSections;

public partial class ConfigurationGroup : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(SystemEditorExpander));

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