using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAtariCollection.Controls;

public partial class IdeCascade : ContentView
{
   
#pragma warning disable CS0169
    
    [AutoBindable]
    private readonly string title;

    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly string drivePrefix;

    [AutoBindable] private readonly string diskImagePath;
    
    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly IdeDiskOptions diskPaths;
    
    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly int diskId;
    
    [AutoBindable]
    private readonly ICommand clearDiskImageCommand;

    [AutoBindable]
    private readonly ICommand browseDiskImageCommand;

#pragma warning restore CS0169

    public IdeCascade()
    {
        InitializeComponent();
        SetDisplayValues();
    }

    
    protected override void OnPropertyChanged(string propertyName = null)
    {
        if (propertyName == nameof(DiskPaths))
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
        DiskId = (DiskId + 1) % 2;
    }

    private void PreviousClicked(object sender, EventArgs e)
    {
        DiskId = (DiskId == 0) ? 1: 0;
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
            DiskImagePath = (DiskId == 0) ? DiskPaths.Disk0 : DiskPaths.Disk1;
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