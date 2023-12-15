namespace MyAtariCollection.Models;

public partial class GdosDriveOptions: ObservableObject
{
    [ObservableProperty] private string gemdosFolder = String.Empty;
    [ObservableProperty] private bool addGemdosAfterPhysicalDrives = true;
    [ObservableProperty] private bool atariHostFilenameConversion = true;
    [ObservableProperty] private DiskWriteProtection writeProtection = DiskWriteProtection.Off;
    [ObservableProperty] private bool bootFromHardDisk = true;
}