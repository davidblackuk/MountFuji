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
using MountFuji;
using MountFuji.Services.UpdatesService;
using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class CommandsTestBase
{
    protected Mock<IConfigFileService> configFileServiceMock;
    protected Mock<IPopupNavigation> popupNavigationMock;
    protected Mock<IServiceProvider> serviceProviderMock;
    protected Mock<IPreferencesService> preferencesServiceMock;
    protected Mock<ISystemsService> systemsServiceMock;
    protected Mock<IFujiFilePickerService> fujiFilePickerMock;
    protected Mock<ILogger<MainViewModel>> logMock;
    protected Mock<IAppSelectorStrategy> appSelectorMock;
    protected Mock<IAvailableUpdatesService> updateServiceMock;
    protected ApplicationPreferences Preferences { get; set; }
    protected AtariConfiguration SelectedConfiguration { get; set; }

    protected Mock<IRomCommands> romCommandsMock;
    protected Mock<ICartridgeCommands> cartridgeCommandsMock;
    protected Mock<IGemdosCommands> gemdosCommandsMock;
    protected Mock<IFloppyCommands> floppyCommandsMock;
    
    protected MainViewModel MainViewModel { get; set; }

    
    public void SetupMainViewModelMocks()
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
        updateServiceMock = new Mock<IAvailableUpdatesService>();
        preferencesServiceMock.Setup(x => x.Preferences).Returns(Preferences);

        romCommandsMock = new Mock<IRomCommands>();
        cartridgeCommandsMock = new Mock<ICartridgeCommands>();
        gemdosCommandsMock = new Mock<IGemdosCommands>();
        floppyCommandsMock = new Mock<IFloppyCommands>();
        
        MainViewModel = new MainViewModel(configFileServiceMock.Object, popupNavigationMock.Object,
            serviceProviderMock.Object, preferencesServiceMock.Object,
            systemsServiceMock.Object, fujiFilePickerMock.Object, logMock.Object,
            updateServiceMock.Object,
            romCommandsMock.Object, cartridgeCommandsMock.Object, gemdosCommandsMock.Object,
            floppyCommandsMock.Object);
        MainViewModel.SelectedConfiguration = SelectedConfiguration;
    }
}