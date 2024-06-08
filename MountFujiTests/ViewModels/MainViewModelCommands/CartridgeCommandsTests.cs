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

public class CartridgeCommandsTests: CommandsTestBase
{
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
    }
    
    [Test]
    public void ClearCartridges_WhenInvoked_ShouldSetTheCartridgeImageValueInTheSelectedConfigurationToEmptyString()
    {
        SelectedConfiguration.CartridgeImage = Guid.NewGuid().ToString();
        var sut = CreateSut();

        sut.ClearCommand.Execute(MainViewModel);

        SelectedConfiguration.CartridgeImage.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseCartridges_WhenInvoked_ShouldSetTheCartridgeImageValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my cartridge filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseCommand.ExecuteAsync(MainViewModel);

        SelectedConfiguration.CartridgeImage.Should()
            .Be(expectedValue); 
    }
    
    private CartridgeCommands CreateSut()
    {
        var sut = new CartridgeCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object);
        MainViewModel.CartridgeCommands = sut;
        return sut;
    }
}