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

using System.ComponentModel;
using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;

namespace MountFuji.Controls;

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
    
    [AutoBindable(OnChanged = nameof(SetDisplayValues))]
    private readonly IdeByteSwap byteSwap;
    
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
        // we check for null here as it fails on windows when we delete a system
        // but not on MAC. Write once, fail everywhere else!
        if (propertyName == nameof(DiskPaths) && DiskPaths is not null)
        {

            DiskPaths.PropertyChanged -= OnDiskPathsChanged;
            DiskPaths.PropertyChanged += OnDiskPathsChanged;
        }
        
        if (propertyName == nameof(ByteSwap))
        {

            if (DiskId == 0)
            {
                DiskPaths.ByteSwapDrive0 = ByteSwap;
            }
            else
            {
                DiskPaths.ByteSwapDrive1 = ByteSwap;
            }
            
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

        SetByteSwap();
    }

    private void SetTitle()
    {
        if (DrivePrefix is not null)
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
 
    private void SetByteSwap()
    {
         if (DiskPaths is not null)
         {
             var newValue = (DiskId == 0) ? DiskPaths.ByteSwapDrive0 : DiskPaths.ByteSwapDrive1;
             if (newValue != ByteSwap)
             {
                 ByteSwap = newValue;
             }
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