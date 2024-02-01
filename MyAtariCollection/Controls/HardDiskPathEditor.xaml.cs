


using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;

namespace MountFuji.Controls;

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

    [AutoBindable] 
    private readonly string pickerOpenIcon = IconFont.Folder_open;
    
#pragma warning restore CS0169
    
    public HardDiskPathEditor()
    {
        InitializeComponent();
    }
}