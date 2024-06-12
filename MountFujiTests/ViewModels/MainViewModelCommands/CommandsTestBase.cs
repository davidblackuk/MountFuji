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
    #pragma warning disable CS8618
    protected Mock<IConfigFileService> configFileServiceMock;
    protected Mock<IPopupNavigation> popupNavigationMock;
    protected Mock<IServiceProvider> serviceProviderMock;
    protected Mock<IPreferencesService> preferencesServiceMock;
    protected Mock<ISystemsService> systemsServiceMock;
    protected Mock<ILogger<MainViewModel>> logMock;
    protected Mock<IAppSelectorStrategy> appSelectorMock;
    protected Mock<IAvailableUpdatesService> updateServiceMock;
    protected ApplicationPreferences Preferences { get; set; }
    protected AtariConfiguration SelectedConfiguration { get; set; }

    protected Mock<IRomCommands> romCommandsMock;
    protected Mock<ICartridgeCommands> cartridgeCommandsMock;
    protected Mock<IGemdosCommands> gemdosCommandsMock;
    protected Mock<IFloppyCommands> floppyCommandsMock;
    protected Mock<IAcsiCommands> acsiCommandsMock;
    protected Mock<IScsiCommands> scsiCommandsMock;
    protected Mock<IIdeCommands> ideCommandsMock;
    protected Mock<IToolbarCommands> toolbarCommandsMock;
    
    protected MainViewModel MainViewModel { get; set; }
#pragma warning restore CS8618
    
    


    
    public void SetupMainViewModelMocks()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
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
        acsiCommandsMock = new Mock<IAcsiCommands>();
        scsiCommandsMock = new Mock<IScsiCommands>();
        ideCommandsMock = new Mock<IIdeCommands>();
        toolbarCommandsMock = new Mock<IToolbarCommands>();
        
        MainViewModel = new MainViewModel( 
            preferencesServiceMock.Object,
            systemsServiceMock.Object, 
            updateServiceMock.Object,
            romCommandsMock.Object, cartridgeCommandsMock.Object, gemdosCommandsMock.Object,
            floppyCommandsMock.Object,
            acsiCommandsMock.Object, scsiCommandsMock.Object, ideCommandsMock.Object,
            toolbarCommandsMock.Object);
        MainViewModel.SelectedConfiguration = SelectedConfiguration;
        serviceProviderMock.Setup(s => s.GetService(typeof(MainViewModel))).Returns(MainViewModel);

    }
}