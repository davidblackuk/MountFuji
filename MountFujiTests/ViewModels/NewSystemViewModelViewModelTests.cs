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

public class NewSystemViewModelViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IMachineTemplateService> templateServiceMock;
    
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        templateServiceMock = new Mock<IMachineTemplateService>();
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
        sut.SelectedTemplate = new AtariConfigurationTemplate();
        sut.OkCommand.CanExecute(null).Should().BeFalse();
    }

    [Test]
    public void OkCommand_WhenSelectedTemplateIsNotSet_ShouldBeDisabled()
    {
        var sut = CreateSut();
        sut.Name = "Hello";
        sut.OkCommand.CanExecute(null).Should().BeFalse();
    }

    [Test]
    public void OkCommand_WhenNameAndSelectedTemplateAreSet_ShouldBeEnabled()
    {
        var sut = CreateSut();
        sut.SelectedTemplate = new AtariConfigurationTemplate();
        sut.Name = "this is a test";
        sut.OkCommand.CanExecute(null).Should().BeTrue();
    }
    
    
    [Test]
    public async Task SelectionChangedCommand_WhenInvoked_ShouldSetNameToTheSelectedTemplateName()
    {
        var sut = CreateSut();
        var expectedValue = "The name";
        sut.SelectedTemplate = new AtariConfigurationTemplate { DisplayName = expectedValue};
        sut.SelectionChangedCommand.Execute(null);
        sut.Name.Should().Be(expectedValue);
    }

    [Test]
    public void SelectFirstTemplate_WhenInvoked_ShouldSetTHeFirstTemplateInTheServiceAsTheSelectedOne()
    {
        var expectedName = "The name";
        
        AtariConfigurationTemplate template = new() { DisplayName = expectedName};
        templateServiceMock.Setup(s => s.All()).Returns([template]);
        
        var sut = CreateSut();
        
        sut.SelectFirstTemplate();
        
        sut.SelectedTemplate.Should().NotBeNull();
        sut.SelectedTemplate.DisplayName.Should().Be(expectedName);
    }

    [Test]
    public void GetConfiguration_WhenCalled_ShouldCreateAConfigFromTemplateAndInitializeTheNameAndDescription()
    {
        var expectedName = "The name";
        var expectedDescription = "The description";
        var expectedTemplate = new AtariConfigurationTemplate { DisplayName = expectedName };
        templateServiceMock.Setup(s => s.CreateConfigurationFromTemplate(expectedName))
            .Returns(new AtariConfiguration { DisplayName = expectedName, Description = expectedDescription });
        var sut = CreateSut();
        sut.SelectedTemplate = expectedTemplate;
        sut.Name = expectedName;
        sut.Description = expectedDescription;
        var result = sut.GetConfiguration();
        result.Should().NotBeNull();
        result.DisplayName.Should().Be(expectedName);
        result.Description.Should().Be(expectedDescription);
    }
    
    
    
    private NewSystemViewModelViewModel CreateSut()
    {
        return new NewSystemViewModelViewModel( templateServiceMock.Object, popupNavigationMock.Object);
    }
}