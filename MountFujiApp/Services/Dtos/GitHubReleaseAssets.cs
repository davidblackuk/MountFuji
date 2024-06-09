using System.Diagnostics.CodeAnalysis;

namespace MountFuji.Services.Dtos;

[ExcludeFromCodeCoverage]
public class GitHubReleaseAssets
{
    public string url { get; set; }
    public int id { get; set; }
    public string node_id { get; set; }
    public string name { get; set; }
    public object label { get; set; }
    public GitHubReleaseUploader GitHubReleaseUploader { get; set; }
    public string content_type { get; set; }
    public string state { get; set; }
    public int size { get; set; }
    public int download_count { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public string browser_download_url { get; set; }
}