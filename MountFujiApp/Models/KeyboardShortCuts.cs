// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY, without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

namespace MountFuji.Models;

public enum ShortcutModifier
{
    /// <summary>
    /// The shortcut runs with no modifier
    /// </summary>
    WithoutModifier,

    /// <summary>
    /// The short cut works only when the modifier key is pressed
    /// </summary>
    WithModifier,
    
    /// <summary>
    /// Used in the null object only
    /// </summary>
    Null
}

/// <summary>
/// Short cut keys
/// </summary>
public enum ShortcutKey
{
    Options,
    FullScreen,
    Borders,
    MouseMode,
    ColdReset,
    WarmReset,
    ScreenShot,
    BossKey,
    CursorEmu,
    FastForward,
    RecAnim,
    RecSound,
    Sound,
    Pause,
    Debugger,
    Quit,
    LoadMem,
    SaveMem,
    InsertDiskA,
    SwitchJoy0,
    SwitchJoy1,
    SwitchPadA,
    SwitchPadB,
    Null
}

public class HatariShortcut(ShortcutModifier modifier, ShortcutKey key, String description, string displayValue)
{
    public static HatariShortcut Null = new(ShortcutModifier.Null, ShortcutKey.Null, "Null", "");

    /// <summary>
    /// Indication if modifier os chorded with the key to perform the action
    /// </summary>
    public ShortcutModifier Modifier { get; } = modifier;
    
    /// <summary>
    /// The short cut key 
    /// </summary>
    public ShortcutKey Key { get; } = key;
    
    /// <summary>
    /// The display description of the keys function
    /// </summary>
    public string Description { get; } = description;

    /// <summary>
    /// The display value for the shortcut key (eg 'B' 'F12' 'Pause')
    /// </summary>
    public string DisplayValue { get; } = displayValue;

    public override string ToString()
    {
        return $"{Description} - {Modifier} - {Key} = {Description}";
    }
}