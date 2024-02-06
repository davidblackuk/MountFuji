namespace MountFuji.Models;

public partial class IdeDiskOptions: ObservableObject
{
    [ObservableProperty] private string disk0 = String.Empty;
    [ObservableProperty] private IdeByteSwap byteSwapDrive0 = IdeByteSwap.Off;
    
    [ObservableProperty] private string disk1 = String.Empty;
    [ObservableProperty] private IdeByteSwap byteSwapDrive1 = IdeByteSwap.Off;
}