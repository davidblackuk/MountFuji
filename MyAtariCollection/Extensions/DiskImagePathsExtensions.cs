namespace MyAtariCollection.Extensions;

public static class DiskImagePathsExtensions
{
    

    public static void SetImagePath(this AcsiScsiDiskOptions paths, int diskId, string fileFullPath)
    {
        switch (diskId)
        {
            case 0:
                paths.Disk0 = fileFullPath;
                break;
            case 1:
                paths.Disk1 = fileFullPath;
                break;
            case 2:
                paths.Disk2 = fileFullPath;
                break;
            case 3:
                paths.Disk3 = fileFullPath;
                break;
            case 4:
                paths.Disk4 = fileFullPath;
                break;
            case 5:
                paths.Disk5 = fileFullPath;
                break;
            case 6:
                paths.Disk6 = fileFullPath;
                break;
            case 7:
                paths.Disk7 = fileFullPath;
                break;
        }
    }
    
    public static void ClearImagePath(this AcsiScsiDiskOptions paths, int diskId)
    {
        switch (diskId)
        {
            case 0:
                paths.Disk0 = "";
                break;
            case 1:
                paths.Disk1 = "";
                break;
            case 2:
                paths.Disk2 = "";
                break;
            case 3:
                paths.Disk3 = "";
                break;
            case 4:
                paths.Disk4 = "";
                break;
            case 5:
                paths.Disk5 = "";
                break;
            case 6:
                paths.Disk6 = "";
                break;
            case 7:
                paths.Disk7 = "";
                break;
        }
    }
    
    
    public static void SetImagePath(this IdeDiskOptions options, int diskId, string fileFullPath)
    {
        if (diskId == 0)
        {
            options.Disk0 = fileFullPath;
        }
        else
        {
            options.Disk1 = fileFullPath;
        }
    }
    
    public static void ClearImagePath(this IdeDiskOptions options, int diskId)
    {
        if (diskId == 0)
        {
            options.Disk0 = "";
        }
        else
        {
            options.Disk1 = "";
        }
    }
}