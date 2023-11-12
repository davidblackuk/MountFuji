


namespace MyAtariCollection.Controls;

public partial class HardDiskPathEditor : ContentView
{
#pragma warning disable CS0169
    
    [AutoBindable]
    private readonly string title;
    
    [AutoBindable]
    private readonly string diskImagePath;
    
    [AutoBindable]
    private readonly int diskId;
    
    [AutoBindable]
    private readonly ICommand clearDiskImageCommand;

    [AutoBindable]
    private readonly ICommand browseDiskImageCommand;

#pragma warning restore CS0169
    
    public HardDiskPathEditor()
    {
        InitializeComponent();
    }
}