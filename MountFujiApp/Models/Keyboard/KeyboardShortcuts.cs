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

namespace MountFuji.Models.Keyboard;

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