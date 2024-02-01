using CommunityToolkit.Mvvm.ComponentModel;

namespace MountFuji.Models;

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