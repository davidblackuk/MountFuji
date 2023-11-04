

namespace MyAtariCollection.Services;

public class MachineTemplateService : IMachineTemplateService
{
    private List<AtariConfigurationTemplate> allTemplates = new()
    {
        new ()
        {
            DisplayName = "ST",
            SystemType = AtariSystemType.ST,
            StVideoTiming = VideoTiming.Three,
            FalconDsp = FalconDspEmulation.Dummy,
            FastBootPatch = false,
            PatchTimerD = false,
            BlitterInStMode = false,
            
            CpuClock = CpuClock.Clock8Mhz,
            CpuType = CpuType.MC68000,
            FpuType = FpuType.None,
            CycleExact = true,
            PrefetchEmulation = true,
            MmuEmulation = false,
            Use24BitAddressing = true,
            AccurateFpuEmulation = true,
            
            RomImage = "",
        },
        new ()
        {
            DisplayName = "Mega ST",
            SystemType = AtariSystemType.MegaST,
            StVideoTiming = VideoTiming.Three,
            FalconDsp = FalconDspEmulation.Dummy,
            FastBootPatch = false,
            PatchTimerD = false,
            BlitterInStMode = false,
            
            CpuClock = CpuClock.Clock8Mhz,
            CpuType = CpuType.MC68000,
            FpuType = FpuType.None,
            CycleExact = true,
            PrefetchEmulation = true,
            MmuEmulation = false,
            Use24BitAddressing = true,
            AccurateFpuEmulation = true,
            
            RomImage = "",

        },
        new ()
        {
            DisplayName = "STE",
            SystemType = AtariSystemType.STE,
            StVideoTiming = VideoTiming.Three,
            FalconDsp = FalconDspEmulation.Dummy,
            FastBootPatch = false,
            PatchTimerD = false,
            BlitterInStMode = false,
            
            CpuClock = CpuClock.Clock8Mhz,
            CpuType = CpuType.MC68000,
            FpuType = FpuType.None,
            CycleExact = true,
            PrefetchEmulation = true,
            MmuEmulation = false,
            Use24BitAddressing = true,
            AccurateFpuEmulation = true,
            
            RomImage = "",

        },
        new ()
        {
            DisplayName = "Mega STE",
            SystemType = AtariSystemType.MegaSTE,
            StVideoTiming = VideoTiming.Three,
            FalconDsp = FalconDspEmulation.Dummy,
            FastBootPatch = false,
            PatchTimerD = false,
            BlitterInStMode = false,
            
            CycleExact = true,
            PrefetchEmulation = true,
            MmuEmulation = false,
            Use24BitAddressing = true,
            AccurateFpuEmulation = true,
            CpuClock = CpuClock.Clock16Mhz,
            CpuType = CpuType.MC68000,
            FpuType = FpuType.None,
            
            RomImage = "",

        },
        new ()
        {
            DisplayName = "TT030",
            SystemType = AtariSystemType.TT,
            StVideoTiming = VideoTiming.Three,
            FalconDsp = FalconDspEmulation.Dummy,
            FastBootPatch = false,
            PatchTimerD = false,
            BlitterInStMode = false,
            
            CpuClock = CpuClock.Clock32Mhz,
            CpuType = CpuType.MC68030,
            FpuType = FpuType.MC68882,
            CycleExact = true,
            PrefetchEmulation = true,
            MmuEmulation = false,
            Use24BitAddressing = true,
            AccurateFpuEmulation = true,
            
            RomImage = "",

        },
        new ()
        {
            DisplayName = "Falcon030",
            SystemType = AtariSystemType.Falcon,
            StVideoTiming = VideoTiming.Three,
            FalconDsp = FalconDspEmulation.Dummy,
            FastBootPatch = false,
            PatchTimerD = false,
            BlitterInStMode = false,
            
            CpuClock = CpuClock.Clock16Mhz,
            CpuType = CpuType.MC68030,
            FpuType = FpuType.None,
            CycleExact = true,
            PrefetchEmulation = true,
            MmuEmulation = false,
            Use24BitAddressing = true,
            AccurateFpuEmulation = true,
            
            RomImage = "",
        },
    };

    public IEnumerable<AtariConfigurationTemplate> All() => allTemplates;

    public AtariConfiguration CreateConfigurationFromTemplate(string name)
    {
        var template =
            allTemplates.FirstOrDefault(t => t.DisplayName.ToLowerInvariant() == name.ToLowerInvariant());
        if (template == null)
        {
            throw new ArgumentException($"System '{name}' is not recognised");
        }

        AtariConfiguration config = new()
        {
            DisplayName = template.DisplayName,
            SystemType = template.SystemType,
            StVideoTiming = template.StVideoTiming,
            FalconDsp = template.FalconDsp,
            FastBootPatch = template.FastBootPatch,
            PatchTimerD = template.FastBootPatch,
            BlitterInStMode = template.BlitterInStMode,
            CpuType = template.CpuType,
            CpuClock = template.CpuClock,
            FpuType = template.FpuType,
            
            MmuEmulation = template.MmuEmulation,
            AccurateFpuEmulation = template.AccurateFpuEmulation,
            Use24BitAddressing = template.Use24BitAddressing,
            PrefetchEmulation = template.PrefetchEmulation,
            CycleExact = template.CycleExact,
            
            RomImage = template.RomImage,
        };

        return config;
    }
    
}