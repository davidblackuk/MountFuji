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


public partial class FloppyDriveOptions: ObservableObject
{
    [ObservableProperty] private string driveAPath = String.Empty;
    [ObservableProperty] private bool driveAEnabled = true;

    [ObservableProperty] private string driveBPath = String.Empty;
    [ObservableProperty] private bool driveBEnabled = true;



}