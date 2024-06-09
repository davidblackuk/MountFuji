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

using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class GemDosCommandsTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }


    [Test]
    public void ClearGemdosFolder_WhenInvoked_ShouldSetTheGemDosFolderValueInTheSelectedConfigurationToEmptyString()
    {
        string expectedValue = "A GDOS Folder";
        SelectedConfiguration.GdosDriveOptions.GemdosFolder = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.GdosDriveOptions.GemdosFolder.Should().Be(expectedValue);

        sut.ClearCommand.Execute(MainViewModel);

        SelectedConfiguration.GdosDriveOptions.GemdosFolder.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseGemdosFolder_WhenInvoked_ShouldSetTheGemdosFolderPathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk folder"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseCommand.ExecuteAsync(MainViewModel);

        SelectedConfiguration.GdosDriveOptions.GemdosFolder.Should().Be(expectedValue); 
    }

    
    private GemdosCommands CreateSut()
    {
        var sut = new GemdosCommands(preferencesServiceMock.Object, fujiFilePickerMock.Object);
        MainViewModel.GemdosCommands = sut;
        return sut;
    }
}