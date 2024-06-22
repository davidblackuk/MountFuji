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

namespace MountFujiTests.ViewModels;

public class RomPickerPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigateionMock;
    private Mock<IRomService> romServiceMock;

    [SetUp]
    public void SetUp()
    {
        popupNavigateionMock = new Mock<IPopupNavigation>();
        romServiceMock = new Mock<IRomService>();
        romServiceMock.Setup(rs => rs.GetRoms()).Returns(Enumerable.Empty<Rom>());
    }

    [Test]
    public async Task CanceCommand_WhenInvoked_ShouldPopThePopupOffTheStack()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        popupNavigateionMock.Verify(pn => pn.PopAsync(true), Times.Once);
    }
    
    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldSetConfirmedToFalse()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeFalse();
    }
    
       [Test]
    public async Task OkCommand_WhenInvoked_ShouldPopThePopupOffTheStack()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        popupNavigateionMock.Verify(pn => pn.PopAsync(true), Times.Once);
    }
    
    [Test]
    public async Task OkCommand_WhenInvoked_ShouldSetConfirmedToFalse()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeTrue();
    }
    
    [Test]
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void OkCommand_ShouldBeEnabledOrDisabled_AccordingToState(bool hasRom, bool expectedState)
    {
        var expectedRom = hasRom ? new Rom() : null;
        var sut = CreateSut();
        sut.SelectedRom = expectedRom;
        sut.OkCommand.CanExecute(null).Should().Be(expectedState);
    }
    
    private RomPickerPopupViewModel CreateSut()
    {
        return new RomPickerPopupViewModel(popupNavigateionMock.Object, romServiceMock.Object);
    }
    
}