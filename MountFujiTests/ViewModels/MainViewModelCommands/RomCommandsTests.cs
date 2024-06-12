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

public class RomCommandsTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }


    [Test]
    public void ClearRom_WhenInvoked_ShouldSetTheRomPathValueInTheSelectedConfigurationToEmptyString()
    {
        SelectedConfiguration.RomImage = Guid.NewGuid().ToString();
        
        var sut = CreateSut();

        sut.ClearCommand.Execute(MainViewModel);
        SelectedConfiguration.RomImage.Should().BeEmpty();
    }

    [Test]
    public async Task BrowseRoms_WhenInvoked_ShouldSetTheRomImageValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my filename";
        var sut = CreateSut();
        
        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(), 
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseCommand.ExecuteAsync(MainViewModel);
        
        SelectedConfiguration.RomImage.Should().Be(expectedValue);
    }

    private RomCommands CreateSut()
    {
        var sut = new RomCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object, serviceProviderMock.Object, 
            popupNavigationMock.Object, logMock.Object);

        toolbarCommandsMock.Setup(tc => tc.RunCommand.NotifyCanExecuteChanged());
        
        MainViewModel.RomCommands = sut;
        return sut;
    }
}