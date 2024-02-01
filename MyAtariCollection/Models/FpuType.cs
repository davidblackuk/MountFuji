namespace MountFuji.Models;

/// <summary>
/// Floating point unit type 
/// </summary>
public enum FpuType
{
    /// <summary>
    /// ST/STE + Mega varieties did not come with an FPU in the case
    /// </summary>
    None = 0,
    
    MC68881 = 68881,
    
    /// <summary>
    /// Included by default for TT computers
    /// </summary>
    MC68882 = 68882,
    
    Internal = 68040
}