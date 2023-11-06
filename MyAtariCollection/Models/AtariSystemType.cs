namespace MyAtariCollection.Models;

/// <summary>
/// Main Atari System types supported by Hatari. At the time of creation the STacy
/// is not a supported platform. it would be cool if it were, though of use to
/// no one other tan hard core ST enthusiasts :-)
/// </summary>
public enum AtariSystemType
{
    /// <summary>
    /// Atari ST, STF or STFM
    /// </summary>
    ST,
    
    /// <summary>
    /// Mega St and ST in a box
    /// </summary>
    MegaST,
    
    /// <summary>
    /// STE, more palette colors, a blitter and DMA access, a better ST
    /// </summary>
    STE,
    
    /// <summary>
    /// Mega STE in a box
    /// </summary>
    MegaSTE,
    
    /// <summary>
    /// UNix workstation compatible beast
    /// </summary>
    TT,
    
    /// <summary>
    /// Falcon030: The last of it's line. If you're a billionaire you can own one of these
    /// </summary>
    Falcon
}