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

namespace MountFuji.Services;

public class MachineTemplateService : IMachineTemplateService
{
    private List<AtariConfigurationTemplate> allTemplates = new()
    {
        new ()
        {
            DisplayName = "ST",
            SystemType = AtariSystemType.ST,
            
        },        
        new ()
        {
            DisplayName = "1040 ST",
            SystemType = AtariSystemType.ST,
            StMemorySize = 1024,
        },
        new ()
        {
            DisplayName = "Mega ST",
            SystemType = AtariSystemType.MegaST,
            StMemorySize = 1024,
        },
        new ()
        {
            DisplayName = "Mega 2",
            SystemType = AtariSystemType.MegaST,
            StMemorySize = 2048,
        },
        new ()
        {
            DisplayName = "Mega 4",
            SystemType = AtariSystemType.MegaST,
            StMemorySize = 4096,
        },
        new ()
        {
            DisplayName = "STE",
            SystemType = AtariSystemType.STE,
            StVideoTiming = VideoTiming.Three,
        },
        new ()
        {
            DisplayName = "Mega STE",
            SystemType = AtariSystemType.MegaSTE,
            CpuClock = CpuClock.Clock16Mhz,
            StMemorySize = 1024,
        },
        new ()
        {
            DisplayName = "TT030 - 2MB",
            SystemType = AtariSystemType.TT,
            
            CpuClock = CpuClock.Clock32Mhz,
            CpuType = CpuType.MC68030,
            FpuType = FpuType.MC68882,

            Use24BitAddressing = false,
            StMemorySize = 2048,
            
            
        },
        new ()
        {
            DisplayName = "Falcon030",
            SystemType = AtariSystemType.Falcon,
            FalconDsp = FalconDspEmulation.Dummy,
            // TODO: Add constants for this
            StMemorySize = 1024,
            CpuClock = CpuClock.Clock16Mhz,
            CpuType = CpuType.MC68030,
            
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
            CartridgeImage = template.CartridgeImage,
            
            StMemorySize = template.StMemorySize,
            TtMemorySize = template.TtMemorySize,
            
                        
        };

        return config;
    }
    
}