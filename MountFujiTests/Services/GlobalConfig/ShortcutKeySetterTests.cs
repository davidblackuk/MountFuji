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
using MountFuji.Services.GlobalConfig;

namespace MountFujiTests.Services.GlobalConfig;

[TestFixture]
public class ShortcutKeySetterTests
{
    private SortcutKeySetter Sut { get; set; }
    private KeyboardShortcuts shortcuts;

    [SetUp]
    public void SetUp()
    {
        Sut = new SortcutKeySetter();
        shortcuts = new KeyboardShortcuts();
    }

    [Test]
    public void When_SetShortcutKeyOptions_ShouldSet_expectedResult()
    {
        const string expectedResult = "test";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.Options, expectedResult);
        shortcuts.Options.Should().Be(expectedResult);
    }

    [Test]
    public void When_SetShortcutKeyFullScreen_ShouldSet_expectedResult()
    {
        const string expectedResult = "testFullScreen";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.FullScreen, expectedResult);
        shortcuts.FullScreen.Should().Be(expectedResult);
        
    }
    
  [Test]
    public void When_SetShortcutKeyBorders_ShouldSet_expectedResult()
    {
        const string expectedResult = "testBorders";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.Borders, expectedResult);
        shortcuts.Borders.Should().Be(expectedResult);
        
    }
    
    [Test]
    public void When_SetShortcutKeyMouseMode_ShouldSet_expectedResult()
    {
        const string expectedResult = "MouseMode";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.MouseMode, expectedResult);
        shortcuts.MouseMode.Should().Be(expectedResult);
        
    }
    

     [Test]
    public void When_SetShortcutKeyColdReset_ShouldSet_expectedResult()
    {
        const string expectedResult = "ColdReset";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.ColdReset, expectedResult);
        shortcuts.ColdReset.Should().Be(expectedResult);
        
    }

     [Test]
    public void When_SetShortcutKeyWarmReset_ShouldSet_expectedResult()
    {
        const string expectedResult = "WarmReset";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.WarmReset, expectedResult);
        shortcuts.WarmReset.Should().Be(expectedResult);
        
    }
   [Test]
    public void When_SetShortcutKey_ShouldSetScreenShot_expectedResult()
    {
        const string expectedResult = "ScreenShot";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.ScreenShot, expectedResult);
        shortcuts.ScreenShot.Should().Be(expectedResult);
        
    }
    
       
    [Test]
    public void When_SetShortcutKeSound_ShouldSet_expectedResult()
    {
        const string expectedResult = "Sound";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.Sound, expectedResult);
        shortcuts.Sound.Should().Be(expectedResult);
        
    }
    
        
    [Test]
    public void When_SetShortcutKeyPause_ShouldSet_expectedResult()
    {
        const string expectedResult = "Pause";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.Pause, expectedResult);
        shortcuts.Pause.Should().Be(expectedResult);
        
    }
    
        
    [Test]
    public void When_SetShortcutKeyDebugger_ShouldSet_expectedResult()
    {
        const string expectedResult = "Debugger";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.Debugger, expectedResult);
        shortcuts.Debugger.Should().Be(expectedResult);
        
    }
    
    
    
   [Test]
    public void When_SetShortcutKeyBossKey_ShouldSet_expectedResult()
    {
        const string expectedResult = "BossKey";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.BossKey, expectedResult);
        shortcuts.BossKey.Should().Be(expectedResult);
        
    }
   [Test]
    public void When_SetShortcutKeyCursorEmu_ShouldSet_expectedResult()
    {
        const string expectedResult = "CursorEmu";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.CursorEmu, expectedResult);
        shortcuts.CursorEmu.Should().Be(expectedResult);
        
    } 
    
    [Test]
    public void When_SetShortcutKeyQuit_ShouldSet_expectedResult()
    {
        const string expectedResult = "testQuit";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.Quit, expectedResult);
        shortcuts.Quit.Should().Be(expectedResult);
        
    }

    [Test]
    public void When_SetShortcutKeyLoadMem_ShouldSet_expectedResult()
    {
        const string expectedResult = "testLoadMem";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.LoadMem, expectedResult);
        shortcuts.LoadMem.Should().Be(expectedResult);

    }

    [Test]
    public void When_SetShortcutKeyFastForward_ShouldSet_expectedResult()
    {
        var expectedResult = "testFastForward";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.FastForward, expectedResult);
        shortcuts.FastForward.Should().Be(expectedResult);

    }

    [Test]
    public void When_SetShortcutKeyRecAnim_ShouldSet_expectedResult()
    {
        var expectedResult = "testRecAnim";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.RecAnim, expectedResult);
        shortcuts.RecAnim.Should().Be(expectedResult);

    }

    [Test]
    public void When_SetShortcutKeyRecSound_ShouldSet_expectedResult()
    {
        var expectedResult = "testRecSound";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.RecSound, expectedResult);
        shortcuts.RecSound.Should().Be(expectedResult);

    }
    
            
    [Test] 
    public void When_SetShortcutKeySaveMem_ShouldSet_expectedResult()
    {
        const string expectedResult = "SaveMem";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.SaveMem, expectedResult);
        shortcuts.SaveMem.Should().Be(expectedResult);
        
    }

    [Test] 
    public void When_SetShortcutKeyInsertDiskA_ShouldSet_expectedResult()
    {
        const string expectedResult = "InsertDiskA";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.InsertDiskA, expectedResult);
        shortcuts.InsertDiskA.Should().Be(expectedResult);
        
    }

    [Test] 
    public void When_SetShortcutKeySwitchJoy0_ShouldSet_expectedResult()
    {
        const string expectedResult = "SwitchJoy0";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.SwitchJoy0, expectedResult);
        shortcuts.SwitchJoy0.Should().Be(expectedResult);
        
    }

    [Test] 
    public void When_SetShortcutKeySwitchJoy1_ShouldSet_expectedResult()
    {
        const string expectedResult = "SwitchJoy1";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.SwitchJoy1, expectedResult);
        shortcuts.SwitchJoy1.Should().Be(expectedResult);
        
    }

    [Test] 
    public void When_SetShortcutKeySwitchPadA_ShouldSet_expectedResult()
    {
        const string expectedResult = "SwitchPadA";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.SwitchPadA, expectedResult);
        shortcuts.SwitchPadA.Should().Be(expectedResult);
        
    }

    [Test] 
    public void When_SetShortcutKeySwitchPadB_ShouldSet_expectedResult()
    {
        const string expectedResult = "SwitchPadB";
        Sut.SetShortcutKey(shortcuts, ShortcutKey.SwitchPadB, expectedResult);
        shortcuts.SwitchPadB.Should().Be(expectedResult);
        
    }

      [Test] 
    public void When_SetShortcutKeyCalledWithUnknownShortcutKey_ShouldSet_ThrowArgumentExceptiopn()
    {
        const string expectedResult = "Boom";
        Action act = () => Sut.SetShortcutKey(shortcuts, ShortcutKey.Null, expectedResult);
        act.Should().Throw<ArgumentException>();
    }

    
}