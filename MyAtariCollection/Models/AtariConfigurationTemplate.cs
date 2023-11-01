namespace MyAtariCollection.Models;

public class AtariConfigurationTemplate
{
    public string DisplayName { get; init; }
    public AtariSystemType SystemType { get; init; }
    
    public VideoTiming StVideoTiming { get; init; }
}