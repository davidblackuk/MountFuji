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

using System.Configuration;
using Microsoft.Extensions.Logging;
using Mopups.Pages;
using MountFuji;
using MountFuji.Views;

namespace MountFujiTests.ViewModels;

public class MainViewModelTests
{
    private Mock<IConfigFileService> configFileServiceMock;
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IServiceProvider> serviceProviderMock;
    private Mock<IPreferencesService> preferencesServiceMock;
    private Mock<ISystemsService> systemsServiceMock;
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    private Mock<ILogger<MainViewModel>> logMock;
    private Mock<IAppSelectorStrategy> appSelectorMock;
    
    private ApplicationPreferences Preferences { get; set; }
    private AtariConfiguration SelectedConfiguration { get; set; }

    
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
        preferencesServiceMock = new Mock<IPreferencesService>();
        configFileServiceMock = new Mock<IConfigFileService>();
        serviceProviderMock = new Mock<IServiceProvider>();
        systemsServiceMock = new Mock<ISystemsService>();
        logMock = new Mock<ILogger<MainViewModel>>();
        Preferences = new ApplicationPreferences();
        SelectedConfiguration = new AtariConfiguration();
        appSelectorMock = new Mock<IAppSelectorStrategy>();
        preferencesServiceMock.Setup(x => x.Preferences).Returns(Preferences);
    }

    #region ----- ROMS -----
    [Test]
    public void ClearRom_WhenInvoked_ShouldSetTheRomPathValueInTheSelectedConfigurationToEmptyString()
    {
        SelectedConfiguration.RomImage = Guid.NewGuid().ToString();
        
        var sut = CreateSut();

        sut.ClearRomCommand.Execute(null);
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

        await sut.BrowseRomsCommand.ExecuteAsync(null);
        
        SelectedConfiguration.RomImage.Should().Be(expectedValue);
    }
#endregion
    
    #region ----- CARTS -----
    [Test]
    public void ClearCartridges_WhenInvoked_ShouldSetTheCartridgeImageValueInTheSelectedConfigurationToEmptyString()
    {
        SelectedConfiguration.CartridgeImage = Guid.NewGuid().ToString();
        var sut = CreateSut();

        sut.ClearCartridgeCommand.Execute(null);

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

        await sut.BrowseCartridgesCommand.ExecuteAsync(null);

        SelectedConfiguration.CartridgeImage.Should()
            .Be(expectedValue); 
    }
    #endregion
    
    #region ----- ACSI -----
    
    [Test]
    public void ClearAcsiDiskImage_WhenInvoked_ShouldSetTheAsciImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 2";
        SelectedConfiguration.AcsiImagePaths.Disk2 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue);
        sut.ClearAcsiDiskImageCommand.Execute(2);

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseAcsiDiskImage_WhenInvoked_ShouldSetTheAcsiImagePathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseAcsiDiskImageCommand.ExecuteAsync(2);

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue); 
    }
    
    #endregion
       
    #region ----- SCSI -----
    
    [Test]
    public void ClearScsiDiskImage_WhenInvoked_ShouldSetTheSsciImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 2";
        SelectedConfiguration.ScsiImagePaths.Disk2 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().Be(expectedValue);
        sut.ClearScsiDiskImageCommand.Execute(2);

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseScsiDiskImage_WhenInvoked_ShouldSetTheScsiImagePathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseScsiDiskImageCommand.ExecuteAsync(2);

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().Be(expectedValue); 
    }

    
    #endregion
    
    #region ----- IDE -----
    
    [Test]
    public void ClearIdeDiskImage_WhenInvoked_ShouldSetTheIdeImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 1";
        SelectedConfiguration.IdeOptions.Disk1 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.IdeOptions.Disk1.Should().Be(expectedValue);
        sut.ClearIdeDiskImageCommand.Execute(1);

        SelectedConfiguration.IdeOptions.Disk1.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseIdeDiskImage_WhenInvoked_ShouldSetTheIdeImagePathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseAcsiDiskImageCommand.ExecuteAsync(2);

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue); 
    }

    #endregion
    
    #region ----- FLOPPY -----
    
    [Test]
    public void ClearFloppyDiskAImage_WhenInvoked_ShouldSetTheFloppyImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 1";
        SelectedConfiguration.FloppyOptions.DriveAPath = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.FloppyOptions.DriveAPath.Should().Be(expectedValue);
        sut.ClearFloppyDiskImageCommand.Execute(0);

        SelectedConfiguration.FloppyOptions.DriveAPath.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseFloppyDiskAImage_WhenInvoked_ShouldSetTheFloppyAImagePathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseFloppyDiskImageCommand.ExecuteAsync(0);

        SelectedConfiguration.FloppyOptions.DriveAPath.Should().Be(expectedValue); 
    }
    #endregion
    
    #region ----- GEMDOS -----
    
    [Test]
    public void ClearGemdosFolder_WhenInvoked_ShouldSetTheGemDosFolderValueInTheSelectedConfigurationToEmptyString()
    {
        string expectedValue = "A GDOS Folder";
        SelectedConfiguration.GdosDriveOptions.GemdosFolder = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.GdosDriveOptions.GemdosFolder.Should().Be(expectedValue);

        sut.ClearGemdosFolderCommand.Execute(null);

        SelectedConfiguration.GdosDriveOptions.GemdosFolder.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseGemdosFolder_WhenInvoked_ShouldSetTheGemdosFolderPathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk folder"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFolder(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseGemdosFolderCommand.ExecuteAsync(null);

        SelectedConfiguration.GdosDriveOptions.GemdosFolder.Should().Be(expectedValue); 
    }
    
    #endregion
    
    #region ----- CONTEXT MENU -----

    [Test]
    public void DeleteSystem_WhenConfirmed_ShouldCallTHeSystemServiceToDeleteTheSystem()
    {
        
    }
    
    #endregion

    
    #region ----- APPLICATION TOOL BAR -----
    
    // *about*, settings, new, import, *save*, play

    [Test]
    public async Task About_WhenInvoked_ShouldGetThePopupAndPushIt()
    {

        await CreateSut().AboutCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(p => p.PushAsync(It.IsAny<AboutPopup>(), true), Times.Once);
    }
    
    [Test]
    public async Task SaveSystemsCommand_WhenExecuted_ShouldCallTheSystemServiceSaveMethod()
    {
        var sut = CreateSut();

        await sut.SaveSystemsCommand.ExecuteAsync(null);

        systemsServiceMock.Verify(m => m.Save(), Times.Once());
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
            fujiFilePickerMock.Object, appSelectorMock.Object);
       
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
            fujiFilePickerMock.Object, appSelectorMock.Object);
        prefsPopupViewModel.Confirmed = true;
        
        prefsPopupMock.SetupGet(p => p.ViewModel).Returns(prefsPopupViewModel);
        
        serviceProviderMock.Setup(s => s.GetService(typeof(IPreferencesPopup))).Returns(prefsPopupMock.Object);
        
        var sut = CreateSut();
        
        await sut.EditPreferencesCommand.ExecuteAsync(null);
        
        prefsPopupMock.Raise(pp => pp.Disappearing += null, EventArgs.Empty);
        preferencesServiceMock.Verify(p => p.SaveAsync(), Times.Once);

        
    }
   
    
    #endregion
    
    private MainViewModel CreateSut()
    {
        var sut = new MainViewModel(configFileServiceMock.Object, popupNavigationMock.Object,
            serviceProviderMock.Object, preferencesServiceMock.Object,
            systemsServiceMock.Object, fujiFilePickerMock.Object, logMock.Object);
        sut.SelectedConfiguration = SelectedConfiguration;
        return sut;
    }
}