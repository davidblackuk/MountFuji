using CommunityToolkit.Mvvm.ComponentModel;

namespace MountFuji.Models;

public partial class GdosDriveOptions: ObservableObject
{
    [ObservableProperty] private string gemdosFolder = String.Empty;
    [ObservableProperty] private bool addGemdosAfterPhysicalDrives = false;
    [ObservableProperty] private bool atariHostFilenameConversion = false;
    [ObservableProperty] private DiskWriteProtection writeProtection = DiskWriteProtection.Off;
    [ObservableProperty] private bool bootFromHardDisk = false;
}