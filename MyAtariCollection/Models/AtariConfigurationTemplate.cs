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
    public VideoTiming StVideoTiming { get; init; } = VideoTiming.Three;

    /// <summary>
    /// For falcons the type of Dsp emulation we want.
    /// </summary>
    public FalconDspEmulation FalconDsp { get; init; } = FalconDspEmulation.None;

    /// <summary>
    /// For base (none 'E' STs, solder in a socket and plug a bliter in
    /// </summary>
    public bool BlitterInStMode { get; init; } = false;
    
    /// <summary>
    /// Speed up emulation are the risk of slight 
    /// </summary>
    public bool PatchTimerD { get; init; }
    
    /// <summary>
    /// Patch TOS to boot more quickly
    /// </summary>
    public bool FastBootPatch { get; init; }

    /// <summary>
    /// Processor type in use, notably MC68000 everywhere, except for TT  Falcon (030),
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
    public bool PrefetchEmulation { get; init; } = true;
    /// <summary>
    /// Use cycle exact emulation (uses more host CPU)
    /// </summary>
    public bool CycleExact { get; init; } = true;
    
    /// <summary>
    /// Emulate the memory management unit (uses more host CPU)
    /// </summary>
    public bool MmuEmulation { get; init; }

    /// <summary>
    /// Use 24-bit instead of 32-bit addressing mode (24-bit is enabled by default)
    /// </summary>
    public bool Use24BitAddressing { get; init; } = true;

    /// <summary>
    /// Use full software FPU emulation (Softfloat library)
    /// </summary>
    public bool AccurateFpuEmulation { get; init; } = true;

    /// <summary>
    /// Full path to the rom image to use for this device
    /// </summary>
    public string RomImage { get; init; } = "";


    /// <summary>
    /// Amount of St memory in kilobytes (KiB), valid values are
    /// 256 (rare original ST), 512 (stock ST),vales 1 to 14 are MB sizes!
    /// not sure why) then multiples of 1024 up to a max of 14MB (really only falcon)
    /// </summary>
    public int StMemorySize { get; init; } = 512;

    /// <summary>
    /// Amount of TT memory in the system, must be incremented in blocks of 4*1024 and max of 1024 * 1024
    /// </summary>
    public int TtMemorySize { get; init; } = 0;

    /// <summary>
    /// optional paths to ACSI drive images, not applicable for Falcons
    /// </summary>
    public AcsiScsiDiskOptions AchiDiskImagePaths = new ();

    /// <summary>
    /// optional paths to SCSI drive images
    /// </summary>
    public AcsiScsiDiskOptions ScsiDiskImagePaths = new ();

    /// <summary>
    /// optional paths to ACSI drive images, only applicable for Falcons
    /// </summary>
    public IdeDiskOptions IdeDiskOptions = new ();

    /// <summary>
    /// Options for floppy drives
    /// </summary>
    public FloppyDriveOptions FloppyOptions = new();

    /// <summary>
    /// Atari screen options, video mode, VDI settings etc
    /// </summary>
    public AtariScreenOptions ScreenOptions = new();

}