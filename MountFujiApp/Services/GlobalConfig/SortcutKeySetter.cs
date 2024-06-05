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

using MountFuji.Models.Keyboard;
using KeyboardShortcuts = MountFuji.Models.Keyboard.KeyboardShortcuts;

namespace MountFuji.Services.GlobalConfig;

public class SortcutKeySetter : ISortcutKeySetter
{

    public void SetShortcutKey(KeyboardShortcuts shortcuts, ShortcutKey key, string newValue)
    {
        switch (key)
        {
            case ShortcutKey.Options:
                shortcuts.Options = newValue;
                break;
            case ShortcutKey.FullScreen:
                shortcuts.FullScreen = newValue;
                break;
            case ShortcutKey.Borders:
                shortcuts.Borders = newValue;
                break;
            case ShortcutKey.MouseMode:
                shortcuts.MouseMode = newValue;
                break;
            case ShortcutKey.ColdReset:
                shortcuts.ColdReset = newValue;
                break;
            case ShortcutKey.WarmReset:
                shortcuts.WarmReset = newValue;
                break;
            case ShortcutKey.ScreenShot:
                shortcuts.ScreenShot = newValue;
                break;
            case ShortcutKey.BossKey:
                shortcuts.BossKey = newValue;
                break;
            case ShortcutKey.CursorEmu:
                shortcuts.CursorEmu = newValue;
                break;
            case ShortcutKey.FastForward:
                shortcuts.FastForward = newValue;
                break;
            case ShortcutKey.RecAnim:
                shortcuts.RecAnim = newValue;
                break;
            case ShortcutKey.RecSound:
                shortcuts.RecSound = newValue;
                break;
            case ShortcutKey.Sound:
                shortcuts.Sound = newValue;
                break;
            case ShortcutKey.Pause:
                shortcuts.Pause = newValue;
                break;
            case ShortcutKey.Debugger:
                shortcuts.Debugger = newValue;
                break;
            case ShortcutKey.Quit:
                shortcuts.Quit = newValue;
                break;
            case ShortcutKey.LoadMem:
                shortcuts.LoadMem = newValue;
                break;
            case ShortcutKey.SaveMem:
                shortcuts.SaveMem = newValue;
                break;
            case ShortcutKey.InsertDiskA:
                shortcuts.InsertDiskA = newValue;
                break;
            case ShortcutKey.SwitchJoy0:
                shortcuts.SwitchJoy0 = newValue;
                break;
            case ShortcutKey.SwitchJoy1:
                shortcuts.SwitchJoy1 = newValue;
                break;
            case ShortcutKey.SwitchPadA:
                shortcuts.SwitchPadA = newValue;
                break;
            case ShortcutKey.SwitchPadB:
                shortcuts.SwitchPadB = newValue;
                break;
            default:
                throw new ArgumentException($"Unknown ShortcutKey: {key}");
        }
    }
}