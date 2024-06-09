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

public class FloppyCommandsTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }


    [Test]
    public void ClearFloppyDiskAImage_WhenInvoked_ShouldSetTheFloppyImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 1";
        SelectedConfiguration.FloppyOptions.DriveAPath = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.FloppyOptions.DriveAPath.Should().Be(expectedValue);
        sut.ClearCommand.Execute(new MainViewModelDiskId{ ViewModel = MainViewModel, Id = 0});

        SelectedConfiguration.FloppyOptions.DriveAPath.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseFloppyDiskAImage_WhenInvoked_ShouldSetTheFloppyAImagePathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseCommand.ExecuteAsync(new MainViewModelDiskId{ ViewModel = MainViewModel, Id = 0});

        SelectedConfiguration.FloppyOptions.DriveAPath.Should().Be(expectedValue); 
    }
    
    private FloppyCommands CreateSut()
    {
        var sut = new FloppyCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object);
        MainViewModel.FloppyCommands = sut;
        return sut;
    }
}