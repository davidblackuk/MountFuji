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

using MountFuji.Services.UpdatesService;

using NUnit.Framework;
using Moq.Protected;
using Moq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using FluentAssertions;
using System;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace MountFujiTests.Services.UpdateService;

[TestFixture]
public class GitHubVersionApiTests
{
    private Mock<ILogger<GitHubVersionApi>> loggerMock;

    [SetUp]
    public void SetUpAttribute()
    {
        loggerMock = new Mock<ILogger<GitHubVersionApi>>();
    }
    
    [Test]
    public async Task GetMountFujiPublicVersions_WhenInvoked_ShouldCorrectlyParseTheDataFromTheService()
    {
        var sut = CreateSut();

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[{\"tag_name\":\"0.1.2\"},{\"tag_name\":\"0.2.1\"}]"),
            });
    
        var httpClient = new HttpClient(handlerMock.Object){BaseAddress = new Uri("https://api.github.com")};

        var result = await sut.GetMountFujiPublicVersions(httpClient);

        result.Should().HaveCount(2);
        result.Should().Contain(new Version("0.1.2"));
        result.Should().Contain(new Version("0.2.1"));
    }
    
    private GitHubVersionApi CreateSut()
    {
        return new GitHubVersionApi(loggerMock.Object);
    }
}


