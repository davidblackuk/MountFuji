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

/// <summary>
/// Specifies the Emulation quality of the DSP on the falcon, i'll flesh this out when I know more.
/// </summary>
public enum FalconDspEmulation
{
    None,
    
    Dummy,
    
    Full
}

/// <summary>
/// Processor type in use, notably MC68000 everywhere, except for TT & Falcon (030),
/// however third party hardware modules existed to allow more impressive CPus on humble STs
/// </summary>
public enum CpuType
{
    MC68000,
    MC68010,
    MC68020,
    MC68030,
    MC68040,
    MC68060,
}

/// <summary>
///  The the speed we clock at
/// </summary>
public enum CpuClock
{
    /// <summary>
    /// Stock speed of ST, STE, pluse mega varieties
    /// </summary>
    Clock8Mhz,
    
    /// <summary>
    /// Stock Falcon speed
    /// </summary>
    Clock16Mhz,
    
    /// <summary>
    /// Stock TT speed
    /// </summary>
    Clock32Mhz,
}

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