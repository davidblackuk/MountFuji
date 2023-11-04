using System.Text;

namespace MyAtariCollection.Services;

public class SystemOptionsGenerator: OptionsGenerator, ISystemOptionsGenerator
{
    public void Generate(AtariConfiguration config, StringBuilder builder)
    {
        AddMachineType(config, builder);

        if (config.SystemType == AtariSystemType.ST)
        {
            AddVideoTiming(config, builder);
            AddFlag(builder, "blitter", config.BlitterInStMode);
        }

        if (config.SystemType == AtariSystemType.Falcon)
        {
            AddFalconDsp(config, builder);
        }

        AddFlag(builder, "timer-d", config.PatchTimerD);
        AddFlag(builder, "fast-boot", config.FastBootPatch);
        
    }

    private void AddMachineType(AtariConfiguration config, StringBuilder builder)
    {
        string system = String.Empty;
        switch (config.SystemType)
        {
            case AtariSystemType.ST:
                system = "st";
                break;
            case AtariSystemType.MegaST:
                system = "megast";
                break;
            case AtariSystemType.STE:
                system = "ste";
                break;
            case AtariSystemType.MegaSTE:
                system = "megaste";
                break;
            case AtariSystemType.TT:
                system = "tt";
                break;
            case AtariSystemType.Falcon:
                system = "falcon";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.SystemType));
        }

        AddFlag(builder, $"machine",system);
    }

    private void AddVideoTiming(AtariConfiguration config, StringBuilder builder)
    {
        string timing = String.Empty;
        switch (config.StVideoTiming)
        {
            case VideoTiming.Random:
                timing = "random";
                break;
            case VideoTiming.One:
                timing = "ws1";
                break;
            case VideoTiming.Two:
                timing = "ws2";
                break;
            case VideoTiming.Three:
                timing = "ws3";
                break;
            case VideoTiming.Four:
                timing = "ws4";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.StVideoTiming));
        }
        AddFlag(builder, "video-timing", timing);
    }

    private void AddFalconDsp(AtariConfiguration config, StringBuilder builder)
    {
        string mode = String.Empty;
        
        switch (config.FalconDsp)
        {
            case FalconDspEmulation.None:
                mode = "none";
                break;
            case FalconDspEmulation.Dummy:
                mode = "dummy";
                break;
            case FalconDspEmulation.Full:
                mode = "emu";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(config.FalconDsp));
        }
        
        AddFlag(builder, "dsp", mode);
    }
}