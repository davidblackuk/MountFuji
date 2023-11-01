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

/// <summary>
/// Wakeup State for MMU/GLUE (x=ws1/ws2/ws3/ws4/random, default ws3). When powering on, the STF will randomly
/// choose one of these wake up states. The state will then affect the timings where border removals and other
/// video tricks should be made, which can give different results on screen. For example, WS3 is known to be
/// compatible with many demos, while WS1 can show more problems.
/// </summary>
public enum VideoTiming
{
    /// <summary>
    /// Pick a randon wake state
    /// </summary>
    Random,
    
    /// <summary>
    /// Wake state 1
    /// </summary>
    One,
    
    /// <summary>
    /// Wake state 2, this is the default.
    /// </summary>
    Two,
    
    /// <summary>
    /// Wake state 3
    /// </summary>
    Three,
    
    /// <summary>
    /// Wake state 4
    /// </summary>
    Four
}