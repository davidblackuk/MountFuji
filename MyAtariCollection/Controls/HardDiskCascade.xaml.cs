using System.ComponentModel;
using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;
using MountFuji.Models;

namespace MountFuji.Controls;

public partial class HardDiskCascade 
{
    
#pragma warning disable CS0169
    
    [AutoBindable]
    private readonly string title;

    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly string drivePrefix;

    [AutoBindable] private readonly string diskImagePath;
    
    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly AcsiScsiDiskOptions diskPaths;
    
    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly int diskId;
    
    [AutoBindable]
    private readonly ICommand clearDiskImageCommand;

    [AutoBindable]
    private readonly ICommand browseDiskImageCommand;

#pragma warning restore CS0169

    public HardDiskCascade()
    {
        InitializeComponent();
        SetDisplayValues();
    }

    
    protected override void OnPropertyChanged(string propertyName = null)
    {
        // we check for null here as it fails on windows when we delete a system
        // but not on MAC. Write once, fail everywhere else!
        if (propertyName == nameof(DiskPaths) && DiskPaths is not null)
        {
            DiskPaths.PropertyChanged -= OnDiskPathsChanged;
            DiskPaths.PropertyChanged += OnDiskPathsChanged;
        }
        
        base.OnPropertyChanged(propertyName);
    }

    private void OnDiskPathsChanged(object sender, PropertyChangedEventArgs args)
    {
       SetDiskImagePath();
    }

    private void NextClicked(object sender, EventArgs e)
    {
        DiskId = (DiskId + 1) % 8;
    }

    private void PreviousClicked(object sender, EventArgs e)
    {
        DiskId = (DiskId - 1) < 0 ? 7: DiskId - 1;
    }
    
    private void SetDisplayValues()
    {
        SetTitle();

        SetDiskImagePath();
    }

    private void SetTitle()
    {
        if (DrivePrefix != null)
        {
            Title = $"{DrivePrefix} {DiskId}";
        }
    }

    private void SetDiskImagePath()
    {
        if (DiskPaths is not null)
        {
            DiskImagePath = DiskId switch
            {
                0 => DiskPaths.Disk0,
                1 => DiskPaths.Disk1,
                2 => DiskPaths.Disk2,
                3 => DiskPaths.Disk3,
                4 => DiskPaths.Disk4,
                5 => DiskPaths.Disk5,
                6 => DiskPaths.Disk6,
                7 => DiskPaths.Disk7,
                _ => ""
            };
        }
    }

    private void BrowseHddImage(object sender, EventArgs e)
    {
        if (BrowseDiskImageCommand != null && BrowseDiskImageCommand.CanExecute(DiskId))
        {
            BrowseDiskImageCommand.Execute(DiskId);
        }
    }

    private void ClearClicked(object sender, EventArgs e)
    {
        if (ClearDiskImageCommand != null && ClearDiskImageCommand.CanExecute(DiskId))
        {
            ClearDiskImageCommand.Execute(DiskId);
            SetDiskImagePath();
        }
    }
}