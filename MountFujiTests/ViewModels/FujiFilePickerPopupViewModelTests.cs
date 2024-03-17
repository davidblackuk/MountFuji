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

using Microsoft.Extensions.Logging;
using MountFuji.Strategies;
using FileSystemEntry = MountFuji.Models.FileSystemEntry;

namespace MountFujiTests.ViewModels;

public class FujiFilePickerPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<ILogger<FujiFilePickerPopupViewModel>> loggerMock;
    private Mock<IFileSystemService> fileSystemServiceMock;
    private Mock<IDriveRetrievalStrategy> driveRetrievalStragtegyMock;
    
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        loggerMock = new Mock<ILogger<FujiFilePickerPopupViewModel>>();
        fileSystemServiceMock = new Mock<IFileSystemService>();
        driveRetrievalStragtegyMock = new Mock<IDriveRetrievalStrategy>();
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
    public async Task OkCommand_WhenPickerTypeIsFolder_OkButtonShouldAlwaysBeExecutable()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        sut.PickerType = PickerType.Folder;
        sut.OkCommand.CanExecute(null).Should().BeTrue();
    }

    [Test]
    public async Task OkCommand_WhenPickerTypeIsFile_ButtonIsEnabledWhenThereIsASelectedEntryAndTHeEntryTypeIsFile()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        sut.PickerType = PickerType.File;
        sut.SelectedFile = new FileSystemEntry(Directory.GetCurrentDirectory(), EntryType.File);
        sut.OkCommand.CanExecute(null).Should().BeTrue();
    }

 [Test]
    public async Task OkCommand_WhenPickerTypeIsFile_ButtonIsNotEnabledWhenThereIsNoSelectedEntryAndTHeEntryTypeIsFile()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        sut.PickerType = PickerType.File;
        sut.OkCommand.CanExecute(null).Should().BeFalse();
    }


    
    [Test]
    public void SetInitialFolder_WhenInvoked_ShouldInitialize()
    {
        driveRetrievalStragtegyMock.Setup(ds => ds.RetrieveDrives()).Returns([new FileSystemDrive("","")]);
        var sut = CreateSut();
        
        var initialFolder = Directory.GetCurrentDirectory();
        sut.SetInitialFolder(initialFolder);
        sut.CurrentFolder.Should().Be(initialFolder);
    }

    [Test]
    public async Task SelectionChangedCommand_WhenInvoked_ShouldRefreshTheFolder()
    {
        driveRetrievalStragtegyMock.Setup(ds => ds.RetrieveDrives()).Returns([new FileSystemDrive("","")]);
        var sut = CreateSut();
        
        var initialFolder = Directory.GetCurrentDirectory();
        sut.SetInitialFolder(initialFolder);
        sut.SelectedFolder = new MountFuji.Models.FileSystemEntry(initialFolder, EntryType.Folder);
        
        await sut.SelectedFolderChangedCommand.ExecuteAsync(null);
        sut.CurrentFolder.Should().Be(initialFolder);
    }
    
    [Test]
    public async Task SelectionChangedCommand_WhenInvokedWithAFileEntryType_ShouldNotRefreshTheFolder()
    {
        driveRetrievalStragtegyMock.Setup(ds => ds.RetrieveDrives()).Returns([new FileSystemDrive("","")]);
        var sut = CreateSut();

        var initialFolder = Directory.GetCurrentDirectory();
        sut.SetInitialFolder(initialFolder);
        sut.SelectedFile = new MountFuji.Models.FileSystemEntry(initialFolder, EntryType.File);
        
        await sut.SelectedFileChangedCommand.ExecuteAsync(null);
        sut.CurrentFolder.Should().Be(initialFolder);
    }
    

    [Test]
    public async Task SelectionChangedCommand_WhenInvokedWithAParentFolderEntry_ShouldChangeDirectoryUpALevel()
    {
        driveRetrievalStragtegyMock.Setup(ds => ds.RetrieveDrives()).Returns([new FileSystemDrive("","")]);

        var sut = CreateSut();

        var initialFolder = Directory.GetCurrentDirectory();
        sut.SetInitialFolder(initialFolder);
        sut.SelectedFolder = new MountFuji.Models.FileSystemEntry(initialFolder, EntryType.ParentNavigation);
        
        await sut.SelectedFolderChangedCommand.ExecuteAsync(null);
        var expectedFolder = Directory.GetParent(initialFolder);
        sut.CurrentFolder.Should().Be(expectedFolder?.FullName);
    }
    

    private FujiFilePickerPopupViewModel CreateSut()
    {
        return new FujiFilePickerPopupViewModel(driveRetrievalStragtegyMock?.Object, popupNavigationMock.Object, fileSystemServiceMock?.Object, loggerMock.Object );
    }
}