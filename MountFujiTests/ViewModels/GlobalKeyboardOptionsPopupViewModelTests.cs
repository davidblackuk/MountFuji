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
using MountFuji.Models.Keyboard;
using MountFuji.Views;

namespace MountFujiTests.ViewModels;

public class GlobalKeyboardOptionsPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IGlobalSystemConfigurationService> globalConfigServiceMock;
    private Mock<IPreferencesService> preferencesServiceMock;
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    private Mock<IServiceProvider> serviceProviderMock;
    private Mock<ILogger<GlobalKeyboardOptionsPopupViewModel>> logMock;

    [SetUp]
    public void SetUp()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        globalConfigServiceMock = new Mock<IGlobalSystemConfigurationService>();
        preferencesServiceMock = new Mock<IPreferencesService>();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
        serviceProviderMock = new Mock<IServiceProvider>();
        logMock = new Mock<ILogger<GlobalKeyboardOptionsPopupViewModel>>();

        preferencesServiceMock.SetupGet(s => s.Preferences).Returns(new ApplicationPreferences()
        {
            HatariConfigFile = "hatariConfig"
        });
    }

    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldRestorePreviousState()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        
        globalConfigServiceMock.Verify(v => v.LoadAsync(), Times.Once);
    }

    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldPopThePopupOfTheStack()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        
        popupNavigationMock.Verify(p => p.PopAsync(true), Times.Once);
    }
    
    [Test]
    public async Task OkCommand_WhenInvoked_ShouldSaveState()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        
        globalConfigServiceMock.Verify(v => v.SaveAsync(), Times.Once);
    }

    [Test]
    public async Task OkCommand_WhenInvoked_ShouldPopThePopupOfTheStack()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        
        popupNavigationMock.Verify(p => p.PopAsync(true), Times.Once);
    }

    [Test]
    public void SetKey_WhenInvoked_ShouldPushThePopUp()
    {
        var setShortCutPopupMock = new Mock<ISetShortcutPopupView>();
        setShortCutPopupMock.Setup(s => s.AsPopUp()).Returns(new PopupPage());
        
        serviceProviderMock.Setup(sp => sp.GetService(typeof(ISetShortcutPopupView))).Returns(setShortCutPopupMock.Object);

        var sut = CreateSut();
        setShortCutPopupMock.SetupGet(s => s.ViewModel).Returns(new SetShortcutPopupViewModel(popupNavigationMock.Object, globalConfigServiceMock.Object));

        
        sut.SetKeyCommand.ExecuteAsync(new HatariShortcut(ShortcutModifier.WithModifier, ShortcutKey.Options, "",""));
        popupNavigationMock.Verify(p => p.PushAsync(It.IsAny<PopupPage>(), true), Times.Once);
    }
    
    public GlobalKeyboardOptionsPopupViewModel CreateSut()
    {
        return new GlobalKeyboardOptionsPopupViewModel(
            popupNavigationMock.Object, globalConfigServiceMock.Object,
            preferencesServiceMock.Object, fujiFilePickerMock.Object,
            serviceProviderMock.Object, logMock.Object);
    }
}