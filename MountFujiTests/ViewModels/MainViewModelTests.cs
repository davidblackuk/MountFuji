using Microsoft.Extensions.Logging;

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
        preferencesServiceMock.Setup(x => x.Preferences).Returns(Preferences);
    }


    [Test]
    public void ClearRom_WhenInvoked_ShouldSetThePreferencesValueToEmptyString()
    {
        SelectedConfiguration.RomImage = Guid.NewGuid().ToString();
        
        var sut = CreateSut();

        sut.ClearRomCommand.Execute(null);
        SelectedConfiguration.RomImage.Should().BeEmpty();
    }


    private MainViewModel CreateSut()
    {
        var sut = new MainViewModel(configFileServiceMock.Object, popupNavigationMock.Object,
            serviceProviderMock.Object, preferencesServiceMock.Object,
            systemsServiceMock.Object, fujiFilePickerMock.Object, logMock.Object);
        sut.SelectedConfiguration = SelectedConfiguration;
        return sut;
    }
}