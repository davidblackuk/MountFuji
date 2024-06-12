using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class AcsiCommandsTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }
    
    [Test]
    public void ClearAcsiDiskImage_WhenInvoked_ShouldSetTheAsciImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 2";
        SelectedConfiguration.AcsiImagePaths.Disk2 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue);
        sut.ClearCommand.Execute(2);

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

        await sut.BrowseCommand.ExecuteAsync(2);

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue); 
    }
   
    private AcsiCommands CreateSut()
    {
        var sut = new AcsiCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object, serviceProviderMock.Object);
        MainViewModel.AcsiCommands = sut;
        return sut;
    }
}