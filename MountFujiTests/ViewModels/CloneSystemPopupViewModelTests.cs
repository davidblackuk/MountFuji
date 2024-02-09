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

[TestOf(typeof(CloneSystemPopupViewModel))]
public class CloneSystemPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
        
        
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
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
    public void OkCommand_WhenNameIsSet_ShouldBeEnabled()
    {
        var sut = CreateSut();
        sut.NewName = "Fruit";
        sut.OkCommand.CanExecute(null).Should().BeTrue();
    }

    private CloneSystemPopupViewModel CreateSut()
    {
        return new CloneSystemPopupViewModel(popupNavigationMock.Object);
    }

}