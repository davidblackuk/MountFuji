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

using Microsoft.Extensions.Logging;
using MountFuji.Services.UpdatesService;

namespace MountFujiTests.Services.UpdateService;

public class AvailableUpdatesServiceTests
{
    private Mock<IGitHubVersionApi> versionApiMock;
    private Mock<ILogger<AvailableUpdatesService>> loggerMock;
    private Mock<IApplicationVersion> applicationVersionMock;
    
    [SetUp]
    public void SetUp()
    {
        versionApiMock = new Mock<IGitHubVersionApi>();
        loggerMock = new Mock<ILogger<AvailableUpdatesService>>();
        applicationVersionMock = new Mock<IApplicationVersion>();
    }

    [Test]
    [TestCase(2, 1, true)]
    [TestCase(1, 1, false)]
    [TestCase(0, 1, false)]
    public async Task CheckForUpdate_WhenInvoked_WorksAsExpected(int serverMajor, int ourMajor, bool updateRequired)
    {
        var serverVersion = new Version(serverMajor, 0, 0);
        var ourVersion = new Version(ourMajor, 0, 0);

        applicationVersionMock.SetupGet(mock => mock.Current).Returns(ourVersion);
        versionApiMock.Setup(api => api.GetMountFujiPublicVersions(It.IsAny<HttpClient>()))
            .ReturnsAsync(new List<Version> { serverVersion });

        var sut = CreateSut();

        var result = await sut.CheckForUpdate();

        result.IsUpdateAvailable.Should().Be(updateRequired);
        if (result.IsUpdateAvailable)
        {
            result.ToVersion.Should().Be(serverVersion);
        }
    }
    

    private AvailableUpdatesService CreateSut()
    {
        return new AvailableUpdatesService(versionApiMock.Object, loggerMock.Object, applicationVersionMock.Object);
    } 
}