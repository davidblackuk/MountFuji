namespace MyAtariCollection.Models;

/// <summary>
/// Specifies the Emulation quality of the DSP on the falcon, i'll flesh this out when I know more.
/// </summary>
public enum FalconDspEmulation
{
    None = 0,
    
    Dummy = 1,
    
    Full = 2
}