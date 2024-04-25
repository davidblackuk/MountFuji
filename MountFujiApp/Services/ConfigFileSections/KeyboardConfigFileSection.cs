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
using MountFuji.Models.Keyboard;
using KeyboardOptions = MountFuji.Models.Keyboard.KeyboardOptions;
using KeyboardShortcuts = MountFuji.Models.Keyboard.KeyboardShortcuts;

namespace MountFuji.Services.ConfigFileSections;

public class KeyboardConfigFileSection: ConfigFileSection, IKeyboardConfigFileSection
{
    public const string KeyboardConfigSectionName = "Keyboard";
    public const string KeyboardShortcutsWithModSectionName = "KeyShortcutsWithMod";
    public const string KeyboardShortcutsWithoutModSectionName = "KeyShortcutsWithoutMod";

    private const string DisableRepeatKey = "bDisableKeyRepeat";
    private const string KeyMapTypeKey = "nKeymapType";
    private const string MappingFileKey = "szMappingFileName";

    private const string OptionsKey = "kOptions";
    private const string FullScreenKey = "kFullScreen";
    private const string BordersKey = "kBorders";
    private const string MouseModeKey = "kMouseMode";
    private const string ColdResetKey = "kColdReset";
    private const string WarmResetKey = "kWarmReset";
    private const string ScreenShotKey = "kScreenShot";
    private const string BossKeyKey = "kBossKey";
    private const string CursorEmuKey = "kCursorEmu";
    private const string FastForwardKey = "kFastForward";
    private const string RecAnimKey = "kRecAnim";
    private const string RecSoundKey = "kRecSound";
    private const string SoundKey = "kSound";
    private const string PauseKey = "kPause";
    private const string DebuggerKey = "kDebugger";
    private const string QuitKey = "kQuit";
    private const string LoadMemKey = "kLoadMem";
    private const string SaveMemKey = "kSaveMem";
    private const string InsertDiskAKey = "kInsertDiskA";
    private const string SwitchJoy0Key = "kSwitchJoy0";
    private const string SwitchJoy1Key = "kSwitchJoy1";
    private const string SwitchPadAKey = "kSwitchPadA";
    private const string SwitchPadBKey = "kSwitchPadB";

    
    public void ToHatariConfig(StringBuilder builder, GlobalSystemConfiguration config)
    {
        WriteGeneralSettings(builder, config);
        WriteShortcuts(KeyboardShortcutsWithModSectionName, config.KeyboardOptions.ShortcutsWithModifier, builder);
        WriteShortcuts(KeyboardShortcutsWithoutModSectionName, config.KeyboardOptions.ShortcutsWithoutModifier, builder);
    }

    
    public void FromHatariConfig(KeyboardOptions to, Dictionary<string, Dictionary<string, string>> sections)
    {
        ReadGeneralSettings(to, sections);
        ReadShortcuts(to.ShortcutsWithModifier, sections[KeyboardShortcutsWithModSectionName]);
        ReadShortcuts(to.ShortcutsWithoutModifier, sections[KeyboardShortcutsWithoutModSectionName]);
    }

    
    private void WriteGeneralSettings(StringBuilder builder, GlobalSystemConfiguration config)
    {
        AddSection(builder, KeyboardConfigSectionName);
        
        AddFlag(builder, (string)DisableRepeatKey, config.KeyboardOptions.DisableRepeat);
        AddFlag(builder, (string)KeyMapTypeKey, (int)config.KeyboardOptions.Mapping);
        AddFlag(builder, (string)MappingFileKey, config.KeyboardOptions.MappingFile);
        
        builder.AppendLine();
    }

    private void WriteShortcuts(string sectionName, KeyboardShortcuts shortcuts, StringBuilder builder)
    {
        AddSection(builder, sectionName);
    
        AddFlag(builder, OptionsKey, shortcuts.Options);
        AddFlag(builder, FullScreenKey, shortcuts.FullScreen);
        AddFlag(builder, BordersKey, shortcuts.Borders);
        AddFlag(builder, MouseModeKey, shortcuts.MouseMode);
        AddFlag(builder, ColdResetKey, shortcuts.ColdReset);
        AddFlag(builder, WarmResetKey, shortcuts.WarmReset);
        AddFlag(builder, ScreenShotKey, shortcuts.ScreenShot);
        AddFlag(builder, BossKeyKey, shortcuts.BossKey);
        AddFlag(builder, CursorEmuKey, shortcuts.CursorEmu);
        AddFlag(builder, FastForwardKey, shortcuts.FastForward);
        AddFlag(builder, RecAnimKey, shortcuts.RecAnim);
        AddFlag(builder, RecSoundKey, shortcuts.RecSound);
        AddFlag(builder, SoundKey, shortcuts.Sound);
        AddFlag(builder, PauseKey, shortcuts.Pause);
        AddFlag(builder, DebuggerKey, shortcuts.Debugger);
        AddFlag(builder, QuitKey, shortcuts.Quit);
        AddFlag(builder, LoadMemKey, shortcuts.LoadMem);
        AddFlag(builder, SaveMemKey, shortcuts.SaveMem);
        AddFlag(builder, InsertDiskAKey, shortcuts.InsertDiskA);
        AddFlag(builder, SwitchJoy0Key, shortcuts.SwitchJoy0);
        AddFlag(builder, SwitchJoy1Key, shortcuts.SwitchJoy1);
        AddFlag(builder, SwitchPadAKey, shortcuts.SwitchPadA);
        AddFlag(builder, SwitchPadBKey, shortcuts.SwitchPadB);

        builder.AppendLine();
    }


    private void ReadGeneralSettings(KeyboardOptions to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[KeyboardConfigSectionName];

        to.DisableRepeat = ParseBool(DisableRepeatKey, section);
        to.Mapping = ParseEnumValue<KeyboardMapping>(KeyMapTypeKey, section);
        to.MappingFile = section[MappingFileKey];
    }

    private void ReadShortcuts(KeyboardShortcuts shortcuts, Dictionary<string,string> section)
    {
        shortcuts.Options = section[OptionsKey];
        shortcuts.FullScreen = section[FullScreenKey];
        shortcuts.Borders = section[BordersKey];
        shortcuts.MouseMode = section[MouseModeKey];
        shortcuts.ColdReset = section[ColdResetKey];
        shortcuts.WarmReset = section[WarmResetKey];
        shortcuts.ScreenShot = section[ScreenShotKey];
        shortcuts.BossKey = section[BossKeyKey];
        shortcuts.CursorEmu = section[CursorEmuKey];
        shortcuts.FastForward = section[FastForwardKey];
        shortcuts.RecAnim = section[RecAnimKey];
        shortcuts.RecSound = section[RecSoundKey];
        shortcuts.Sound = section[SoundKey];
        shortcuts.Pause = section[PauseKey];
        shortcuts.Debugger = section[DebuggerKey];
        shortcuts.Quit = section[QuitKey];
        shortcuts.LoadMem = section[LoadMemKey];
        shortcuts.SaveMem = section[SaveMemKey];
        shortcuts.InsertDiskA = section[InsertDiskAKey];
        shortcuts.SwitchJoy0 = section[SwitchJoy0Key];
        shortcuts.SwitchJoy1 = section[SwitchJoy1Key];
        shortcuts.SwitchPadA = section[SwitchPadAKey];
        shortcuts.SwitchPadB = section[SwitchPadBKey];
    }
}
/*
   [Keyboard]
   nCountryCode = -1
   nKbdLayout = -1
   nLanguage = -1
*/