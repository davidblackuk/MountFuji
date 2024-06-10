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
using Mopups.Pages;
using MountFuji.ViewModels.MainViewModelCommands;
using MountFuji.Views;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class ApplicationBarCrudCommandsTests: CommandsTestBase
{
    private Mock<ISystemsService> systemsServiceMock;
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IServiceProvider> serviceProviderMock;
    private Mock<IMachineTemplateService> templatesServiceMock;
    private Mock<IPreferencesService> preferencesServiceMock;
    private Mock<IConfigFileService> configFileServiceMock;
    private Mock<ILogger<MainViewModel>> logMock;
    private Mock<IFujiFilePickerService> filePickerMock;
    
    
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        systemsServiceMock = new Mock<ISystemsService>();
        popupNavigationMock = new Mock<IPopupNavigation>();
        serviceProviderMock = new Mock<IServiceProvider>();
        templatesServiceMock = new Mock<IMachineTemplateService>();
        preferencesServiceMock = new Mock<IPreferencesService>();
        configFileServiceMock = new Mock<IConfigFileService>();
        logMock = new Mock<ILogger<MainViewModel>>();
        filePickerMock = new Mock<IFujiFilePickerService>();
    }

    [Test]
    public async Task SaveSystemsCommand_WhenExecuted_ShouldCallTheSystemServiceSaveMethod()
    {
        var sut = CreateSut();

        await sut.SaveCommand.ExecuteAsync(null);

        systemsServiceMock.Verify(m => m.Save(), Times.Once());
    }

    [Test]
    public async Task Delete_WhenInvokedAndThePopUpIsAccepted_ShouldDeleteTheSystem()
    {
        string expectdId = "An Id";
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var deletePopUpMock = new Mock<IDeleteSystemPopup>();
        DeleteSystemPopupViewModel deletePopUpViewModel = new DeleteSystemPopupViewModel(popupNavigationMock.Object);
        deletePopUpViewModel.Confirmed = true;
        deletePopUpMock.SetupGet(p => p.ViewModel).Returns(deletePopUpViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(IDeleteSystemPopup))).Returns(deletePopUpMock.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        systemsServiceMock.Setup(ss => ss.Find(expectdId)).Returns(expectedConfig);
        
        var sut = CreateSut();
        
        await sut.DeleteCommand.ExecuteAsync(expectdId);
        
        deletePopUpMock.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        systemsServiceMock.Verify(p => p.Delete(expectdId), Times.Once);
    }
    
    [Test]
    public async Task Delete_WhenInvokedAndThePopUpIsDeclined_ShouldNotDeleteTheSystem()
    {
        string expectdId = "An Id";
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var deletePopUpMock = new Mock<IDeleteSystemPopup>();
        DeleteSystemPopupViewModel deletePopUpViewModel = new DeleteSystemPopupViewModel(popupNavigationMock.Object);
        deletePopUpViewModel.Confirmed = false;
        deletePopUpMock.SetupGet(p => p.ViewModel).Returns(deletePopUpViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(IDeleteSystemPopup))).Returns(deletePopUpMock.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        systemsServiceMock.Setup(ss => ss.Find(expectdId)).Returns(expectedConfig);
        
        var sut = CreateSut();
        
        await sut.DeleteCommand.ExecuteAsync(expectdId);
        
        deletePopUpMock.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        systemsServiceMock.Verify(p => p.Delete(expectdId), Times.Never);
    }
    
       [Test]
    public async Task Delete_WhenInvokedAndTheSystemDoesNotExist_ShouldNotPopupADialog()
    {
        string expectdId = "An Id";
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var deletePopUpMock = new Mock<IDeleteSystemPopup>();
        DeleteSystemPopupViewModel deletePopUpViewModel = new DeleteSystemPopupViewModel(popupNavigationMock.Object);
        deletePopUpViewModel.Confirmed = false;
        deletePopUpMock.SetupGet(p => p.ViewModel).Returns(deletePopUpViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(IDeleteSystemPopup))).Returns(deletePopUpMock.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        var sut = CreateSut();
        
        await sut.DeleteCommand.ExecuteAsync(expectdId);

        systemsServiceMock.Verify(p => p.Delete(expectdId), Times.Never);
        popupNavigationMock.Verify(p => p.PushAsync(It.IsAny<PopupPage>(), It.IsAny<bool>()), Times.Never());
    }
    
    [Test]
    public async Task Clone_WhenInvokedAndThePopUpIsAccepted_ShouldCloneTheSystem()
    {
        string expectdId = "An Id";
        string expectedName = "A name";
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var cloneSystemPopup = new Mock<ICloneSystemPopup>();
        CloneSystemPopupViewModel clonePopUpViewModel = new CloneSystemPopupViewModel(popupNavigationMock.Object);
        clonePopUpViewModel.NewName = expectedName;
        
        clonePopUpViewModel.Confirmed = true;
        cloneSystemPopup.SetupGet(p => p.ViewModel).Returns(clonePopUpViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(ICloneSystemPopup))).Returns(cloneSystemPopup.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        systemsServiceMock.Setup(ss => ss.Find(expectdId)).Returns(expectedConfig);
        
        var sut = CreateSut();
        
        await sut.CloneCommand.ExecuteAsync(expectdId);
        
        cloneSystemPopup.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        systemsServiceMock.Verify(ss => ss.Clone(expectedConfig, expectedName), Times.Once);
    }

   [Test]
    public async Task Clone_WhenInvokedAndThePopUpIsDeclined_ShouldNotCloneTheSystem()
    {
        string expectdId = "An Id";
        string expectedName = "A name";
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var cloneSystemPopup = new Mock<ICloneSystemPopup>();
        CloneSystemPopupViewModel clonePopUpViewModel = new CloneSystemPopupViewModel(popupNavigationMock.Object);
        clonePopUpViewModel.NewName = expectedName;
        
        clonePopUpViewModel.Confirmed = false;
        cloneSystemPopup.SetupGet(p => p.ViewModel).Returns(clonePopUpViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(ICloneSystemPopup))).Returns(cloneSystemPopup.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        systemsServiceMock.Setup(ss => ss.Find(expectdId)).Returns(expectedConfig);
        
        var sut = CreateSut();
        
        await sut.CloneCommand.ExecuteAsync(expectdId);
        
        cloneSystemPopup.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        systemsServiceMock.Verify(ss => ss.Clone(expectedConfig, expectedName), Times.Never);
    }

  [Test]
    public async Task Clone_WhenInvokedAndTheSystemToCloneIsNotFound_ShouldNotCloneTheSystem()
    {
        string expectdId = "An Id";
        string expectedName = "A name";
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var cloneSystemPopup = new Mock<ICloneSystemPopup>();
        CloneSystemPopupViewModel clonePopUpViewModel = new CloneSystemPopupViewModel(popupNavigationMock.Object);
        clonePopUpViewModel.NewName = expectedName;
        
        clonePopUpViewModel.Confirmed = true;
        cloneSystemPopup.SetupGet(p => p.ViewModel).Returns(clonePopUpViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(ICloneSystemPopup))).Returns(cloneSystemPopup.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        
        var sut = CreateSut();
        
        await sut.CloneCommand.ExecuteAsync(expectdId);
        
        cloneSystemPopup.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        systemsServiceMock.Verify(ss => ss.Clone(expectedConfig, expectedName), Times.Never);
    }

    
    [Test]
    public async Task Create_WhenInvokedAndThePopUpIsAccepted_ShouldCreateTheSystem()
    {
        string expectdId = "An Id";
        string expectedName = "A name";


        var expectedTemplate = new AtariConfigurationTemplate{};
        
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var newSystemPopup = new Mock<INewSystemPopup>();
        NewSystemViewModel newPopUpViewModel = new NewSystemViewModel(templatesServiceMock.Object, popupNavigationMock.Object) { Name = expectedName};
        
        newPopUpViewModel.Confirmed = true;
        newPopUpViewModel.SelectedTemplate = expectedTemplate;
        newSystemPopup.SetupGet(p => p.ViewModel).Returns(newPopUpViewModel);

        templatesServiceMock.Setup(ts => ts.CreateConfigurationFromTemplate(It.IsAny<string>()))
            .Returns(expectedConfig);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(INewSystemPopup))).Returns(newSystemPopup.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        systemsServiceMock.Setup(ss => ss.Find(expectdId)).Returns(expectedConfig);
        
        var sut = CreateSut();
        
        await sut.CreateCommand.ExecuteAsync(null);
        
        newSystemPopup.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        MainViewModel.SelectedConfiguration.Should().Be(expectedConfig);
        
    }
    
    [Test]
    public async Task Create_WhenInvokedAndThePopUpIsDeclined_ShouldNotCreateFromTemplate()
    {
        string expectdId = "An Id";
        string expectedName = "A name";


        var expectedTemplate = new AtariConfigurationTemplate{};
        
        AtariConfiguration expectedConfig = new AtariConfiguration() { Id = expectdId };
        MainViewModel.Systems.Add(expectedConfig);
        
        var newSystemPopup = new Mock<INewSystemPopup>();
        NewSystemViewModel newPopUpViewModel = new NewSystemViewModel(templatesServiceMock.Object, popupNavigationMock.Object) { Name = expectedName};
        
        newPopUpViewModel.Confirmed = false;
        newPopUpViewModel.SelectedTemplate = expectedTemplate;
        newSystemPopup.SetupGet(p => p.ViewModel).Returns(newPopUpViewModel);
        
        templatesServiceMock.Setup(ts => ts.CreateConfigurationFromTemplate(It.IsAny<string>()))
            .Returns(expectedConfig);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(INewSystemPopup))).Returns(newSystemPopup.Object);
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);
        
        systemsServiceMock.Setup(ss => ss.Find(expectdId)).Returns(expectedConfig);
        
        var sut = CreateSut();
        
        await sut.CreateCommand.ExecuteAsync(null);
        
        newSystemPopup.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        systemsServiceMock.Verify(s => s.Add(It.IsAny<AtariConfiguration>()), Times.Never);        
    }
    
    [Test]
    public async Task About_WhenInvoked_ShouldGetThePopupAndPushIt()
    {

        await CreateSut().AboutCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(p => p.PushAsync(It.IsAny<AboutPopup>(), true), Times.Once);
    }
    
    private ApplicationBarCrudCommands CreateSut()
    {
        var sut = new ApplicationBarCrudCommands(systemsServiceMock.Object, popupNavigationMock.Object, 
            serviceProviderMock.Object, preferencesServiceMock.Object, configFileServiceMock.Object, logMock.Object);
        MainViewModel.CrudCommands = sut;
        return sut;
    }
 
        
    [Test]
    public async Task EditPreferences_WhenInvoked_ShouldPushThePopupOnTheNavigationStack()
    {
        var prefsPopupMock = new Mock<IPreferencesPopup>();
        serviceProviderMock.Setup(s => s.GetService(typeof(IPreferencesPopup))).Returns(prefsPopupMock.Object);
        
        var sut = CreateSut();
        
        await sut.EditPreferencesCommand.ExecuteAsync(null);
        
        popupNavigationMock.Verify(n => n.PushAsync(It.IsAny<PopupPage>(), true), Times.Once);
    }

    [Test]
    public async Task EditPreferences_WhenNotConfirmed_ShouldNotSave()
    {
        var prefsPopupMock = new Mock<IPreferencesPopup>();

        var prefsPopupViewModel = new PreferencesPopupViewModel(preferencesServiceMock.Object, popupNavigationMock.Object,
            filePickerMock.Object, appSelectorMock.Object);
       
        prefsPopupMock.SetupGet(p => p.ViewModel).Returns(prefsPopupViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(IPreferencesPopup))).Returns(prefsPopupMock.Object);
        
        var sut = CreateSut();
        
        await sut.EditPreferencesCommand.ExecuteAsync(null);
        
        prefsPopupMock.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        preferencesServiceMock.Verify(p => p.SaveAsync(), Times.Never);
    }

    
    [Test]
    public async Task EditPreferences_WhenConfirmed_ShouldStoreThePreferencesViaTheService()
    {
        var prefsPopupMock = new Mock<IPreferencesPopup>();

        var prefsPopupViewModel = new PreferencesPopupViewModel(preferencesServiceMock.Object, popupNavigationMock.Object,
            filePickerMock.Object, appSelectorMock.Object);
        prefsPopupViewModel.Confirmed = true;
        
        prefsPopupMock.SetupGet(p => p.ViewModel).Returns(prefsPopupViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(IPreferencesPopup))).Returns(prefsPopupMock.Object);
        
        var sut = CreateSut();
        
        await sut.EditPreferencesCommand.ExecuteAsync(null);
        
        prefsPopupMock.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        preferencesServiceMock.Verify(p => p.SaveAsync(), Times.Once);

        
    }
   
}