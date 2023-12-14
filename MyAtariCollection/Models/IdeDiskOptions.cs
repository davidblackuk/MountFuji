namespace MyAtariCollection.Models;


public enum IdeByteSwap
{
    Off,
    On, 
    Auto
}

public partial class IdeDiskOptions: ObservableObject
{
    [ObservableProperty] private string disk0 = String.Empty;
    [ObservableProperty] private IdeByteSwap byteSwapDrive0 = IdeByteSwap.Off;
    
    [ObservableProperty] private string disk1 = String.Empty;
    [ObservableProperty] private IdeByteSwap byteSwapDrive1 = IdeByteSwap.Off;
}

public enum DiskWriteProtection
{
    Off = 0,
    On = 1,
    Auto = 2
}


public partial class FloppyDriveOptions: ObservableObject
{
    [ObservableProperty] private string driveAPath = String.Empty;
    [ObservableProperty] private bool driveAEnabled = true;
    [ObservableProperty] private bool driveADoubleSided = true;

    [ObservableProperty] private string driveBPath = String.Empty;
    [ObservableProperty] private bool driveBEnabled = true;
    [ObservableProperty] private bool driveBDoubleSided = true;

    [ObservableProperty] private bool autoInsertB = true;
    [ObservableProperty] private bool fastFloppyAccess = false;

    [ObservableProperty] private DiskWriteProtection writeProtection = DiskWriteProtection.Off;


}


public partial class GdosDriveOptions: ObservableObject
{
    [ObservableProperty] private string gdosFolder = String.Empty;
    [ObservableProperty] private bool addGemdosAfterPhysicalDrives = true;
    [ObservableProperty] private bool atariHostFilenameConversion = true;
    [ObservableProperty] private DiskWriteProtection writeProtection = DiskWriteProtection.Off;


}