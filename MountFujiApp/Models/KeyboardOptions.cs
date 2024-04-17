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

namespace MountFuji.Models;

public partial class KeyboardOptions: ObservableObject
{
    /// <summary>
    /// What type of mapping to use
    /// </summary>
    [ObservableProperty] private  KeyboardMapping mapping  = KeyboardMapping.Scancode;

    /// <summary>
    /// When the mapping type of fromFile, this value specifies the full path to the mapping file
    /// </summary>
    [ObservableProperty] private string mappingFile = String.Empty;

    /// <summary>
    /// When set, hatari will not use key repeats when the System is in fast forward mode
    /// </summary>
    [ObservableProperty] private bool disableRepeat;

    [ObservableProperty] private KeyboardShortcuts shortcutsWithModifier = new()
    {
        Options = "O",
        FullScreen = "F",
        Borders = "B",
        MouseMode = "M",
        ColdReset = "C",
        WarmReset = "R",
        ScreenShot = "G",
        BossKey = "I",
        CursorEmu = "J",
        FastForward = "X",
        RecAnim = "A",
        RecSound = "Y",
        Sound = "S",
        Pause = "",
        Debugger = "Pause",
        Quit = "Q",
        LoadMem = "L",
        SaveMem = "K",
        InsertDiskA = "D",
        SwitchJoy0 = "F1",
        SwitchJoy1 = "F2",
        SwitchPadA = "F3",
        SwitchPadB = "F4",
    };

    [ObservableProperty] private KeyboardShortcuts shortcutsWithoutModifier = new()
    {
        Options = "F12",
        FullScreen = "F11",
        Pause = "Pause",
    };
}


public partial class KeyboardShortcuts: ObservableObject
{
    [ObservableProperty] private string options;
    [ObservableProperty] private string fullScreen;
    [ObservableProperty] private string borders;
    [ObservableProperty] private string mouseMode;
    [ObservableProperty] private string coldReset;
    [ObservableProperty] private string warmReset;
    [ObservableProperty] private string screenShot;
    [ObservableProperty] private string bossKey;
    [ObservableProperty] private string cursorEmu;
    [ObservableProperty] private string fastForward;
    [ObservableProperty] private string recAnim;
    [ObservableProperty] private string recSound;
    [ObservableProperty] private string sound;
    [ObservableProperty] private string pause;
    [ObservableProperty] private string debugger;
    [ObservableProperty] private string quit;
    [ObservableProperty] private string loadMem;
    [ObservableProperty] private string saveMem;
    [ObservableProperty] private string insertDiskA;
    [ObservableProperty] private string switchJoy0;
    [ObservableProperty] private string switchJoy1;
    [ObservableProperty] private string switchPadA;
    [ObservableProperty] private string switchPadB;
}