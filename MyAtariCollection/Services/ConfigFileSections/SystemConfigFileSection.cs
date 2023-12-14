namespace MyAtariCollection.Services.ConfigFileSections;

public class SystemConfigFileSection: ConfigFileSection, ISystemConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "System");
        
        AddFlag(builder, "nCpuLevel", (int)config.CpuType);
        AddFlag(builder, "nCpuFreq", (int)config.CpuClock);
        AddFlag(builder, "nModelType", (int)config.SystemType);
        AddFlag(builder, "VideoTiming", (int)config.StVideoTiming);
        AddFlag(builder, "nDSPType", (int)config.FalconDsp);
        AddFlag(builder, "bBlitter", config.BlitterInStMode);
        AddFlag(builder, "bPatchTimerD", config.PatchTimerD);
        AddFlag(builder, "bFastBoot", config.FastBootPatch);
        AddFlag(builder, "n_FPUType", (int)config.FpuType);
        AddFlag(builder, "bMMU", config.MmuEmulation);
        AddFlag(builder, "bSoftFloatFPU", config.AccurateFpuEmulation);
        AddFlag(builder, "bAddressSpace24", config.Use24BitAddressing);
        AddFlag(builder, "bCycleExactCpu", config.CycleExact);
        AddFlag(builder, "bCompatibleCpu", config.PrefetchEmulation);
        
        // nVMEType = 1
        // bFastForward = FALSE



    }
}