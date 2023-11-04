namespace MyAtariCollection.Models;

public class AtariConfigurationTemplate
{
    /// <summary>
    /// Display name of the system used in lists etc.
    /// </summary>
    public string DisplayName { get; init; }
    
    /// <summary>
    /// The type of system we are emulating (ST, TT etc)
    /// </summary>
    public AtariSystemType SystemType { get; init; }
    
    /// <summary>
    /// For STF type computers, the initial wake state
    /// </summary>
    public VideoTiming StVideoTiming { get; init; }
 
    /// <summary>
    /// For falcons the type of Dsp emulation we want.
    /// </summary>
    public FalconDspEmulation FalconDsp  { get; init; }

    /// <summary>
    /// For base (none 'E' STs, solder in a socket and plug a bliter in
    /// </summary>
    public bool BlitterInStMode { get; init; }
    
    /// <summary>
    /// Speed up emulation are the risk of slight 
    /// </summary>
    public bool PatchTimerD { get; init; }
    
    /// <summary>
    /// Patch TOS to boot more quickly
    /// </summary>
    public bool FastBootPatch { get; init; }

    /// <summary>
    /// Processor type in use, notably MC68000 everywhere, except for TT & Falcon (030),
    /// however third party hardware modules existed to allow more impressive CPus on humble STs
    /// </summary>
    public CpuType CpuType { get; init; }
    
    /// <summary>
    ///  The the speed we clock at
    /// </summary>
    public CpuClock CpuClock { get; init; }
    
    /// <summary>
    /// Floating point unit type 
    /// </summary>
    public FpuType FpuType { get; init; }


    /// <summary>
    /// Use a more compatible 68000 CPU mode with better prefetch accuracy and cycle counting
    /// </summary>
    public bool PrefetchEmulation { get; init; }
    
    /// <summary>
    /// Use cycle exact emulation (uses more host CPU)
    /// </summary>
    public bool CycleExact { get; init; }
    
    /// <summary>
    /// Emulate the memory management unit (uses more host CPU)
    /// </summary>
    public bool MmuEmulation { get; init; }
    
    /// <summary>
    /// Use 24-bit instead of 32-bit addressing mode (24-bit is enabled by default)
    /// </summary>
    public bool Use24BitAddressing { get; init; }
    
    /// <summary>
    /// Use full software FPU emulation (Softfloat library)
    /// </summary>
    public bool AccurateFpuEmulation { get; init; }

    /// <summary>
    /// Full path to the rom image to use for this device
    /// </summary>
    public string RomImage { get; init; } 
    
}