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

using MountFuji.Services.UpdatesService;

namespace MountFujiTests.ViewModels;

[TestOf(typeof(AboutPopupViewModel))]
public class AboutPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IPersistence> persistanceMock;
    private Mock<IAvailableUpdatesService> updateServiceMock;
    private Mock<IApplicationVersion> applicationVersionMock;
    
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        persistanceMock = new Mock<IPersistence>();
        updateServiceMock = new Mock<IAvailableUpdatesService>();
        applicationVersionMock = new Mock<IApplicationVersion>();
    }

    [Test]
    public async Task CloseCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        updateServiceMock.Setup(us => us.CheckForUpdate()).ReturnsAsync((IsUpdateAvailable: false, ToVersion: new Version()));
        var sut = CreateSut();
        await sut.CloseCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public void Ctor_WhenInvoked_ShouldCheckForUpdate()
    {
        var sut = CreateSut();
        updateServiceMock.Verify(us => us.CheckForUpdate(), Times.Once);
    }

    [Test]
    public void Ctor_WhenInvokedAndUpdateIsAvailable_ShouldStoreTheUpdateInfo()
    {
        
        updateServiceMock
            .Setup(us => us.CheckForUpdate())
            .ReturnsAsync((IsUpdateAvailable: true, ToVersion: new Version(1, 1)));
        var sut = CreateSut(); 
        sut.UpdateAvailable.Should().BeTrue();
        sut.UpdateVersion.Should().Be("1.1");
    }   
        
    private AboutPopupViewModel CreateSut()
    {
        return new AboutPopupViewModel(popupNavigationMock.Object, persistanceMock.Object, updateServiceMock.Object, applicationVersionMock.Object);
    }
}