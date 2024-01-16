
using CommunityToolkit.Mvvm.ComponentModel;


namespace MyAtariCollection.Models;

public partial class AtariConfiguration: ObservableObject
{
    public static AtariConfiguration Empty = new AtariConfiguration { Id = Guid.Empty.ToString() };
    
    /// <summary>
    /// ID needed for CRUD operations
    /// </summary>
    [ObservableProperty] private string id;
    
    
    /// <summary>
    /// Display name of the system used in lists etc.
    /// </summary>
    [ObservableProperty] private string displayName;

    /// <summary>
    /// Description of the system.
    /// </summary>
    [ObservableProperty] private string description;

    /// <summary>
    /// The type of system we are emulating (ST, TT etc)
    /// </summary>
    [ObservableProperty] private AtariSystemType systemType;

    /// <summary>
    /// For STF type computers, the initial wake state
    /// </summary>
    [ObservableProperty] private VideoTiming stVideoTiming;

    /// <summary>
    /// For falcons the type of Dsp emulation we want.
    /// </summary>
    [ObservableProperty] private FalconDspEmulation falconDsp;
    
    /// <summary>
    /// For base (none 'E' STs, solder in a socket and plug a bliter in
    /// </summary>
    [ObservableProperty] private bool blitterInStMode;

    /// <summary>
    /// Speed up emulation are the risk of slight 
    /// </summary>
    [ObservableProperty] private bool patchTimerD;

    /// <summary>
    /// Patch TOS to boot more quickly
    /// </summary>
    [ObservableProperty] private bool fastBootPatch;

    /// <summary>
    /// Processor type in use, notably MC68000 everywhere, except for TT  Falcon (030),
    /// however third party hardware modules existed to allow more impressive CPus on humble STs
    /// </summary>
    [ObservableProperty] private CpuType cpuType;

    /// <summary>
    ///  The the speed we clock at
    /// </summary>
    [ObservableProperty] private CpuClock cpuClock;

    /// <summary>
    /// Floating point unit type 
    /// </summary>
    [ObservableProperty] private FpuType fpuType;
    
    /// <summary>
    /// Use a more compatible 68000 CPU mode with better prefetch accuracy and cycle counting
    /// </summary>
    [ObservableProperty] private  bool prefetchEmulation;
    
    /// <summary>
    /// Use cycle exact emulation (uses more host CPU)
    /// </summary>
    [ObservableProperty] private  bool cycleExact;
    
    /// <summary>
    /// Emulate the memory management unit (uses more host CPU)
    /// </summary>
    [ObservableProperty] private  bool mmuEmulation;
    
    /// <summary>
    /// Use 24-bit instead of 32-bit addressing mode (24-bit is enabled by default)
    /// </summary>
    [ObservableProperty] private  bool use24BitAddressing;
    
    /// <summary>
    /// Use full software FPU emulation (Softfloat library)
    /// </summary>
    [ObservableProperty] private  bool accurateFpuEmulation;

    /// <summary>
    /// Full path to the rom image to use for this device
    /// </summary>
    [ObservableProperty] private string romImage;

    /// <summary>
    /// Full path to the cartridge image to use for this device
    /// </summary>
    [ObservableProperty] private string cartridgeImage;


    
    
    /// <summary>
    /// Amount of St memory in kilobytes (KiB), valid values are
    /// 256 (rare original ST), 512 (stock ST), 1020 (1040ST), 2048 (2.5 Meg
    /// not sure why) then multiples of 1024 up to a max of 14MB (really only falcon)
    /// </summary>
    [ObservableProperty] private int stMemorySize;

    /// <summary>
    /// Amount of TT memory in the system, must be incremented in blocks of 4*1024 and max of 1024 * 1024
    /// </summary>
    [ObservableProperty] private int ttMemorySize;

    /// <summary>
    /// optional paths to ACSI drive images, not applicable for Falcons
    /// </summary>
    [ObservableProperty] private AcsiScsiDiskOptions acsiImagePaths = new();

    /// <summary>
    /// optional paths to SCSI drive images
    /// </summary>
    [ObservableProperty] private AcsiScsiDiskOptions scsiImagePaths = new();
    
    /// <summary>
    /// optional paths to IDE primary / secondary drive images, only applicable for Falcons
    /// </summary>
    [ObservableProperty] private IdeDiskOptions ideOptions = new();

    /// <summary>
    /// optional drive path and options for a GEMDOS drive
    /// </summary>
    [ObservableProperty] private GdosDriveOptions gdosDriveOptions = new();

    /// <summary>
    /// Floppy disk configuration
    /// </summary>
    [ObservableProperty] private FloppyDriveOptions floppyOptions = new();

    /// <summary>
    /// Should we boot from hard disk automatically
    /// </summary>
    [ObservableProperty] private bool bootFromHardDisk = false;

    /// <summary>
    /// Atari screen options, video mode, VDI settings etc
    /// </summary>
    [ObservableProperty] private AtariScreenOptions screenOptions = new();
    
    /// <summary>
    /// Sound options, playback quality etc
    /// </summary>
    [ObservableProperty] private SoundOptions soundOptions = new();
    
    
    
}