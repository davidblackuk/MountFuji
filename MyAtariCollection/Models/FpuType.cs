namespace MyAtariCollection.Models;

/// <summary>
/// Floating point unit type 
/// </summary>
public enum FpuType
{
    /// <summary>
    /// ST/STE + Mega varieties did not come with an FPU in the case
    /// </summary>
    None,
    
    MC68881,
    
    /// <summary>
    /// Included by default for TT computers
    /// </summary>
    MC68882,
    
    Internal
}