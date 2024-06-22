namespace MountFuji.Strategies;

public interface IDriveRetrievalStrategy
{
    IEnumerable<FileSystemDrive> RetrieveDrives();
}