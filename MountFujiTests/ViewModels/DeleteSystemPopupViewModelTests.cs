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

[TestOf(typeof(DeleteSystemPopupViewModel))]
public class DeleteSystemPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
        
        
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
    }

    [Test]
    public async Task NoCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.NoCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task NoCommand_WhenInvoked_ShouldMarkThePopupAsNotConfirmed()
    {
        var sut = CreateSut();
        await sut.NoCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeFalse();
    }

    [Test]
    public async Task YesCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.YesCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task YesCommand_WhenInvoked_ShouldMarkThePopupAsConfirmed()
    {
        var sut = CreateSut();
        await sut.YesCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeTrue();
    }
    
    private DeleteSystemPopupViewModel CreateSut()
    {
        return new DeleteSystemPopupViewModel(popupNavigationMock.Object);
    }
    
}

