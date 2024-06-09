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
using MountFuji.Services.UpdatesService;
using MountFuji.Views;
using MountFujiTests.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels;

public class MainViewModelTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }


    
    #region ----- APPLICATION TOOL BAR -----
    
    // *about*, settings, new, import, *save*, play

    [Test]
    public async Task About_WhenInvoked_ShouldGetThePopupAndPushIt()
    {

        await CreateSut().AboutCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(p => p.PushAsync(It.IsAny<AboutPopup>(), true), Times.Once);
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

        return MainViewModel;
    }
}