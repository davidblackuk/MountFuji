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

namespace MountFujiTests.ViewModels;

public class ImportSystemPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    private Mock<IPreferencesService> preferencesServiceMock;

    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
        preferencesServiceMock = new Mock<IPreferencesService>();

        preferencesServiceMock.Setup(x => x.Preferences).Returns(new ApplicationPreferences
        {
            HatariConfigFile = "c:\\foo"
        });
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
    public void OkCommand_WhenNameIsNotSet_ShouldBeDisabled()
    {
        var sut = CreateSut();
        sut.OkCommand.CanExecute(null).Should().BeFalse();
    }

    [Test]
    public void ClearHatariConfig_WhenInvoked_ShouldSetFilenameToEmptyString()
    {
        var sut = CreateSut();
        string expectedValue = "Hello";
        sut.FileName = expectedValue;
        sut.FileName.Should().Be(expectedValue);
        sut.ClearHatariConfigFileCommand.Execute(null);
        sut.FileName.Should().BeEmpty();
    }


    [Test]
    public async Task BrowseHatariConfigFile_WhenInvoked_ShouldOpenTheFilePicker()
    {
        var sut = CreateSut();
        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(), It.IsAny<Action<string>>(), ""))
            .ReturnsAsync("hello");
        await sut.BrowseHatariConfigFileCommand.ExecuteAsync(null);
        fujiFilePickerMock.Verify(x => x.PickFile(It.IsAny<string>(), It.IsAny<Action<string>>(), It.IsAny<string>()),
            Times.Once);
    }

    [Test]
    public async Task BrowseHatariConfigFile_WhenOkPressed_ShouldStoreTheFileName()
    {
        var expectedValue = "my filename";

        var sut = CreateSut();
        sut.DisplayName = "A display name";
        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(), It.IsAny<Action<string>>(), It.IsAny<string>()))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        sut.OkCommand.CanExecute(null).Should().BeFalse();
        await sut.BrowseHatariConfigFileCommand.ExecuteAsync(null);
        sut.FileName.Should().Be(expectedValue);
        sut.OkCommand.CanExecute(null).Should().BeTrue();
    }


    [Test]
    [TestCase("", "", false)]
    [TestCase("XX", "", false)]
    [TestCase("", "XX", false)]
    [TestCase("XX", "XX", true)]
    public void OkCommand_WhenAllValuesSet_ShouldBeEnabled(string filename, string displayName, bool expectedResult)
    {
        var sut = CreateSut();
        sut.FileName = filename;
        sut.DisplayName = displayName;
        sut.OkCommand.CanExecute(null).Should().Be(expectedResult);
    }

    private ImportSystemPopupViewModel CreateSut()
    {
        return new ImportSystemPopupViewModel(popupNavigationMock.Object, fujiFilePickerMock.Object,
            preferencesServiceMock.Object);
    }
}