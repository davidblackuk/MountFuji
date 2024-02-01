namespace MountFuji.Models;

/// <summary>
///  The the speed we clock at
/// </summary>
public enum CpuClock
{
    /// <summary>
    /// Stock speed of ST, STE, pluse mega varieties
    /// </summary>
    Clock8Mhz = 8,
    
    /// <summary>
    /// Stock Falcon speed
    /// </summary>
    Clock16Mhz = 16,
    
    /// <summary>
    /// Stock TT speed
    /// </summary>
    Clock32Mhz = 32,
}