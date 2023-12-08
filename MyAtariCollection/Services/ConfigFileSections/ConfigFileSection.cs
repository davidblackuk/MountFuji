using System.Globalization;

namespace MyAtariCollection.Services.ConfigFileSections;

public class ConfigFileSection
{
    public void AddSection(StringBuilder builder, string name)
    {
        builder.AppendLine($"[{name}]");
    }

    public void AddFlag(StringBuilder builder, string flag, string value)
    {
        builder.AppendLine($"{flag} = {value}");
    }
    
    public void AddFlag(StringBuilder builder, string flag, bool value)
    {
        builder.AppendLine($"{flag} = {value.ToString(CultureInfo.InvariantCulture).ToUpper()}");
    }
    public void AddFlag(StringBuilder builder, string flag, Int32 value)
    {
        builder.AppendLine($"{flag} = {value.ToString(CultureInfo.InvariantCulture).ToUpper()}");
    }
}

public interface ILogConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class LogConfigFileSection: ConfigFileSection, ILogConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Log");
        
        AddFlag(builder, "sLogFileName", "stderr");
        AddFlag(builder, "sTraceFileName", "stderr");
        AddFlag(builder, "nTextLogLevel", "3");
        AddFlag(builder, "nAlertDlgLogLevel", "1");
        AddFlag(builder, "bConfirmQuit", true);
        AddFlag(builder, "bNatFeats", false);
        AddFlag(builder, "bConsoleWindow", false);
    }
}

public interface IMemoryConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class MemoryConfigFileSection: ConfigFileSection, IMemoryConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Memory");
        
        AddFlag(builder, "nMemorySize", config.StMemorySize);
        AddFlag(builder, "nTTRamSize", config.TtMemorySize * 1024);
        AddFlag(builder, "bAutoSave", false);
        // AddFlag(builder, "szMemoryCaptureFileName");
        // AddFlag(builder, "szAutoSaveFileName");

    }
}


public interface ISystemConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

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
