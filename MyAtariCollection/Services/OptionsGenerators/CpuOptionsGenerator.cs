namespace MyAtariCollection.Services.OptionsGenerators;

public class CpuOptionsGenerator: OptionsGenerator, ICpuOptionsGenerator
{

    public void Generate(AtariConfiguration config, StringBuilder builder)
    {
        AddCpuType(config, builder);
        AddCpuClock(config, builder);
        AddFpuType(config, builder);
        AddRam(config, builder);
        AddFlag(builder,"compatible", config.PrefetchEmulation);
        AddFlag(builder,"cpu-exact", config.CycleExact);
        AddFlag(builder,"mmu", config.MmuEmulation);
        AddFlag(builder,"addr24", config.Use24BitAddressing);
        AddFlag(builder,"fpu-softfloat", config.AccurateFpuEmulation);
    }


    private void AddCpuType(AtariConfiguration config, StringBuilder builder)
    {
        string val = String.Empty;
        switch (config.CpuType)
        {
            case CpuType.MC68000:
                val = "0";
                break;
            case CpuType.MC68010:
                val = "1";
                break;
            case CpuType.MC68020:
                val = "2";
                break;
            case CpuType.MC68030:
                val = "3";
                break;
            case CpuType.MC68040:
                val = "4";
                break;
            case CpuType.MC68060:
                val = "6";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.CpuType));
        }
        AddFlag(builder, "cpulevel", val);
    }

    private void AddCpuClock(AtariConfiguration config, StringBuilder builder)
    {
        string val = String.Empty;
        switch (config.CpuClock)
        {
            case CpuClock.Clock8Mhz:
                val = "8";
                break;
            case CpuClock.Clock16Mhz:
                val = "16";
                break;
            case CpuClock.Clock32Mhz:
                val = "32";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.CpuClock));
        }
        AddFlag(builder, "cpuclock", val);
    }

    private void AddFpuType(AtariConfiguration config, StringBuilder builder)
    {
        string val = String.Empty;
        switch (config.FpuType)
        {
            case FpuType.None:
                val = "none";
                break;
            case FpuType.MC68881:
                val = "68881";
                break;
            case FpuType.MC68882:
                val = "68882";
                break;
            case FpuType.Internal:
                val = "internal";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.FpuType));
        }
        AddFlag(builder, "fpu", val);
    }
    
    private void AddRam(AtariConfiguration config, StringBuilder builder)
    {
        AddFlag(builder, "memsize", config.StMemorySize);
        AddFlag(builder, "ttram", config.TtMemorySize);
    }

    
}