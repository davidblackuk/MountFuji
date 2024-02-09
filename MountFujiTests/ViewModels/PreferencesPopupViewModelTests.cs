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

using MountFuji;

namespace MountFujiTests.ViewModels;

public class PreferencesPopupViewModelTests
{
    private  Mock<IPreferencesService> preferencesServiceMock;
    private  Mock<IPopupNavigation> popupNavigationMock;
    private  Mock<IFujiFilePickerService> fujiFilePickerMock;
    private  Mock<IAppSelectorStrategy> appSelectorMock;

    private ApplicationPreferences Prefs { get; set; } 


    [SetUp]
    public void Initialze()
    {
        Prefs = new ApplicationPreferences();

        preferencesServiceMock = new Mock<IPreferencesService>();
        preferencesServiceMock.SetupGet(p => p.Preferences).Returns(Prefs);

        
        popupNavigationMock = new Mock<IPopupNavigation>();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
        appSelectorMock = new Mock<IAppSelectorStrategy>();
    }
    
    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldMarkThePopupAsNotConfirmed()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeFalse();
    }

    [Test]
    public async Task OkCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task OkCommand_WhenInvoked_ShouldMarkThePopupAsConfirmed()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeTrue();
    }

    [Test]
    [TestCase(null, false)]
    [TestCase("", false)]
    [TestCase("/path/to/file", true)]
    public void OkCommand_CanExecute_ShouldNotBeTrueIfThereIsNoAppPathPresentInThePreferences(string path, bool expectedValue)
    {
        Prefs.HatariApplication = path;
        var sut = CreateSut();
        sut.OkCommand.CanExecute(null).Should().Be(expectedValue);
    }

    [Test]
    public void ClearRomFolder_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.RomFolder = $"A test of {nameof(Prefs.RomFolder)}";
        
        var sut = CreateSut();

        sut.ClearRomFolderCommand.Execute(null);
        Prefs.RomFolder.Should().BeEmpty();
    }

    [Test]
    public async Task BrowseRomFolder_WhenInvoked_ShouldSetTheRomFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseRomFolderCommand.ExecuteAsync(null);
        
        Prefs.RomFolder.Should().Be(expectedValue);
    }
    
    [Test]
    public void ClearCartridgeFolder_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.CartridgeFolder = $"A test of {nameof(Prefs.CartridgeFolder)}";
        
        var sut = CreateSut();

        sut.ClearCartridgeFolderCommand.Execute(null);
        Prefs.CartridgeFolder.Should().BeEmpty();
    }

    [Test]
    public async Task BrowseCartridgeFolder_WhenInvoked_ShouldSetTheFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseCartridgeFolderCommand.ExecuteAsync(null);
        
        Prefs.CartridgeFolder.Should().Be(expectedValue);
    }

    
    [Test]
    public void ClearFloppyDiskFolder_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.FloppyDiskFolder = $"A test of {nameof(Prefs.FloppyDiskFolder)}";
        
        var sut = CreateSut();

        sut.ClearFloppyDiskFolderCommand.Execute(null);
        Prefs.FloppyDiskFolder.Should().BeEmpty();
    }

    [Test]
    public async Task BrowseFloppyDiskFolder_WhenInvoked_ShouldSetTheFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseFloppyDiskFolderCommand.ExecuteAsync(null);
        
        Prefs.FloppyDiskFolder.Should().Be(expectedValue);
    }
    
    [Test]
    public void ClearHardDiskFolder_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.HardDiskFolder = $"A test of {nameof(Prefs.HardDiskFolder)}";
        
        var sut = CreateSut();

        sut.ClearHardDiskFolderCommand.Execute(null);
        Prefs.HardDiskFolder.Should().BeEmpty();
    }

    [Test]
    public async Task BrowseHardDiskFolder_WhenInvoked_ShouldSetTheFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseHardDiskFolderCommand.ExecuteAsync(null);
        
        Prefs.HardDiskFolder.Should().Be(expectedValue);
    }

    
   [Test]
    public void ClearGemDosDiskFolder_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.GemDosFolder = $"A test of {nameof(Prefs.GemDosFolder)}";
        
        var sut = CreateSut();

        sut.ClearGemDosDiskFolderCommand.Execute(null);
        Prefs.GemDosFolder.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseGemDosDiskFolder_WhenInvoked_ShouldSetTheFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseGemDosDiskFolderCommand.ExecuteAsync(null);
        
        Prefs.GemDosFolder.Should().Be(expectedValue);
    }
    
   [Test]
    public void ClearHatariApp_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.HatariApplication = $"A test of {nameof(Prefs.HatariApplication)}";
        
        var sut = CreateSut();

        sut.ClearHatariAppCommand.Execute(null);
        Prefs.HatariApplication.Should().BeEmpty();
    }

    [Test]
    public async Task BrowseHatariApp_WhenInvoked_ShouldSetTheFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        appSelectorMock.Setup(x => x.SelectApplication(It.IsAny<string>(), 
                It.IsAny<Action<string>>()))
            .Callback((string title, Action<string> action) => action(expectedValue));

        await sut.BrowseHatariAppCommand.ExecuteAsync(null);
        
        Prefs.HatariApplication.Should().Be(expectedValue);
    }
    
    
   [Test]
    public void ClearHatariConfigFile_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        Prefs.HatariConfigFile = $"A test of {nameof(Prefs.HatariConfigFile)}";
        
        var sut = CreateSut();

        sut.ClearHatariConfigFileCommand.Execute(null);
        Prefs.HatariConfigFile.Should().BeEmpty();
    }

    
    [Test]
    public async Task BrowseHatariConfigFile_WhenInvoked_ShouldSetTheFolderInPrefsFromTheFilePicker()
    {
        var expectedValue = "my filename";
        
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseHatariConfigFileCommand.ExecuteAsync(null);
        
        Prefs.HatariConfigFile.Should().Be(expectedValue);
    }

    
    
    private PreferencesPopupViewModel CreateSut()
    {
        return new PreferencesPopupViewModel(preferencesServiceMock.Object, popupNavigationMock.Object,
            fujiFilePickerMock.Object, appSelectorMock.Object);
    }

}