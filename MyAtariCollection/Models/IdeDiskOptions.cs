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

public enum FloppyWriteProtection
{
    Off,
    On,
    Auto
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

    [ObservableProperty] private FloppyWriteProtection writeProtection = FloppyWriteProtection.Off;


}