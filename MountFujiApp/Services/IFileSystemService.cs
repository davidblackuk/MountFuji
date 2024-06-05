namespace MountFuji.Services;

public interface IFileSystemService
{
    /// <summary>
    /// Retrieves the sub folders of the specified parent folder, this includes the uplevel node for non-root folders
    /// </summary>
    /// <param name="parent">The file system path of the parent</param>
    /// <returns>An enumerable list of FileSystemEntries for the child folders</returns>
    IEnumerable<FileSystemEntry> GetChildFolders(string parent);

    /// <summary>
    /// Retrieves the files contained in the specified parent folder
    /// </summary>
    /// <param name="parent">The file system path of the parent</param>
    /// <returns>An enumerable list of FileSystemEntries for the child files</returns>
    IEnumerable<FileSystemEntry> GetChildFiles(string parent);

    /// <summary>
    /// For a specified folder, gets a list of file system entries for all folders from the root,
    /// upto and including the specified folder
    /// </summary>
    /// <param name="folder">The full path of the folder to form the breadcrumb for</param>
    /// <returns>an enumerable of BreadcrumbEntry for the folders in the breadcrumb</returns>
    IEnumerable<BreadcrumbEntry> GetBreadCrumbFromRoot(string folder);
}