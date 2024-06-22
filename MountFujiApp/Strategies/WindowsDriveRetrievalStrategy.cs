namespace MountFuji.Strategies;

public class WindowsDriveRetrievalStrategy : IDriveRetrievalStrategy
{
    private const string DocumentsFolder = "Documents";
    public IEnumerable<FileSystemDrive> RetrieveDrives()
    {
        
        var res = new List<FileSystemDrive>();
        
        
        string homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        if (Path.Exists(homePath))
        {
            res.Add(new FileSystemDrive(homePath, "Home"));
            var documentsPath = Path.Combine(homePath, DocumentsFolder);
            if (Path.Exists(documentsPath))
            {
                res.Add(new FileSystemDrive(documentsPath, "Documents"));
            }
        }
        
        var systemDrives = DriveInfo.GetDrives();
        foreach (var drive in systemDrives)
        {
            if (drive.IsReady && !drive.Name.StartsWith("/"))
            {
                res.Add(new FileSystemDrive(drive.Name, drive.Name));
            }
        }

        return res;
    }
}