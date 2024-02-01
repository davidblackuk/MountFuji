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