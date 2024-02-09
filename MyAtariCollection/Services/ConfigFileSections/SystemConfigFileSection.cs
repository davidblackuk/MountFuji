/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Text;

namespace MountFuji.Services.ConfigFileSections;


public class SystemConfigFileSection: ConfigFileSection, ISystemConfigFileSection
{
    public const string ConfigSectionName = "System";

    private const string PatchTimerDKey = "bPatchTimerD";
    private const string FastBootKey = "bFastBoot";
    private const string MmuEmulationKey = "bMMU";
    private const string AccurateFpuKey = "bSoftFloatFPU";
    private const string Use24BitAddressingKey = "bAddressSpace24";
    private const string CycleExactKey = "bCycleExactCpu";
    private const string PrefetchEmulationKey = "bCompatibleCpu";
    private const string FpuTypeKey = "n_FPUType";
    private const string BlitterInStModeKey = "bBlitter";
    private const string CpuLevelKey = "nCpuLevel";
    private const string CpuFreqKey = "nCpuFreq";
    private const string ModelTypeKey = "nModelType";
    private const string VideoTimingKey = "VideoTiming";
    private const string DspTypeKey = "nDSPType";
    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder, CpuLevelKey, (int)config.CpuType);
        AddFlag(builder, CpuFreqKey, (int)config.CpuClock);
        AddFlag(builder, ModelTypeKey, (int)config.SystemType);
        AddFlag(builder, VideoTimingKey, (int)config.StVideoTiming);
        AddFlag(builder, DspTypeKey, (int)config.FalconDsp);
        AddFlag(builder, (string)BlitterInStModeKey, (bool)config.BlitterInStMode);
        AddFlag(builder, FpuTypeKey, (int)config.FpuType);
        
        AddFlag(builder, (string)PatchTimerDKey, (bool)config.PatchTimerD);
        AddFlag(builder, (string)FastBootKey, (bool)config.FastBootPatch);
        AddFlag(builder, (string)MmuEmulationKey, (bool)config.MmuEmulation);
        AddFlag(builder, (string)AccurateFpuKey, (bool)config.AccurateFpuEmulation);
        AddFlag(builder, (string)Use24BitAddressingKey, (bool)config.Use24BitAddressing);
        AddFlag(builder, (string)CycleExactKey, (bool)config.CycleExact);
        AddFlag(builder, (string)PrefetchEmulationKey, (bool)config.PrefetchEmulation);
        
        // nVMEType = 1
        // bFastForward = FALSE
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];
        
        to.CpuType = ParseEnumValue<CpuType>(CpuLevelKey, section);
        to.CpuClock = ParseEnumValue<CpuClock>(CpuFreqKey, section);
        to.SystemType = ParseEnumValue<AtariSystemType>(ModelTypeKey, section);
        to.StVideoTiming = ParseEnumValue<VideoTiming>(VideoTimingKey, section);
        to.FalconDsp = ParseEnumValue<FalconDspEmulation>(DspTypeKey, section);
        to.FpuType = ParseEnumValue<FpuType>(FpuTypeKey, section);

        to.BlitterInStMode = ParseBool(BlitterInStModeKey, section);
        to.PatchTimerD = ParseBool(PatchTimerDKey, section);
        to.FastBootPatch = ParseBool(FastBootKey, section);
        to.MmuEmulation = ParseBool(MmuEmulationKey, section);
        to.AccurateFpuEmulation= ParseBool(AccurateFpuKey, section);
        to.Use24BitAddressing = ParseBool(Use24BitAddressingKey, section);
        to.CycleExact = ParseBool(CycleExactKey, section);
        to.PrefetchEmulation = ParseBool(PrefetchEmulationKey, section);
    }
}