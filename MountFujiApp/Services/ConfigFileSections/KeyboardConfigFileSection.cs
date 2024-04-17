// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public class KeyboardConfigFileSection: ConfigFileSection, IKeyboardConfigFileSection
{
    public const string ConfigSectionName = "Keyboard";

    private const string DisableRepeatKey = "bDisableKeyRepeat";
    private const string KeyMapTypeKey = "nKeymapType";
    private const string MappingFileKey = "szMappingFileName";

    
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder, (string)DisableRepeatKey, config.KeyboardOptions.DisableRepeat);
        AddFlag(builder, (string)KeyMapTypeKey, (int)config.KeyboardOptions.Mapping);
        AddFlag(builder, (string)MappingFileKey, config.KeyboardOptions.MappingFile);
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];
        to.KeyboardOptions.DisableRepeat = ParseBool(DisableRepeatKey, section);
        to.KeyboardOptions.Mapping = ParseEnumValue<KeyboardMapping>(KeyMapTypeKey, section);
        to.KeyboardOptions.MappingFile = section[MappingFileKey];
    }
}

/*


   [Keyboard]
   nCountryCode = -1
   nKbdLayout = -1
   nLanguage = -1
   
   [KeyShortcutsWithMod]
   kOptions = O
   kFullScreen = F
   kBorders = B
   kMouseMode = M
   kColdReset = C
   kWarmReset = R
   kScreenShot = G
   kBossKey = I
   kCursorEmu = J
   kFastForward = X
   kRecAnim = A
   kRecSound = Y
   kSound = S
   kPause =
   kDebugger = Pause
   kQuit = Q
   kLoadMem = L
   kSaveMem = K
   kInsertDiskA = D
   kSwitchJoy0 = F1
   kSwitchJoy1 = F2
   kSwitchPadA = F3
   kSwitchPadB = F4
   
   [KeyShortcutsWithoutMod]
   kOptions = F12
   kFullScreen = F11
   kBorders =
   kMouseMode =
   kColdReset =
   kWarmReset =
   kScreenShot =
   kBossKey =
   kCursorEmu =
   kFastForward =
   kRecAnim =
   kRecSound =
   kSound =
   kPause = Pause
   kDebugger =
   kQuit =
   kLoadMem =
   kSaveMem =
   kInsertDiskA =
   kSwitchJoy0 =
   kSwitchJoy1 =
   kSwitchPadA =
   kSwitchPadB =
   


*/