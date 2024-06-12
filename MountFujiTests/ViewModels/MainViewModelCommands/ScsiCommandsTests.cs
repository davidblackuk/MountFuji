using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class ScsiCommandsTests: CommandsTestBase
{
    private Mock<IFujiFilePickerService> fujiFilePickerMock;
    
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
        fujiFilePickerMock = new Mock<IFujiFilePickerService>();
    }

    
    [Test]
    public void ClearScsiDiskImage_WhenInvoked_ShouldSetTheSsciImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 2";
        SelectedConfiguration.ScsiImagePaths.Disk2 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().Be(expectedValue);
        sut.ClearCommand.Execute(2);

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

        await sut.BrowseCommand.ExecuteAsync(2);

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().Be(expectedValue); 
    }
    
    private ScsiCommands CreateSut()
    {
        var sut = new ScsiCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object, serviceProviderMock.Object);
        MainViewModel.ScsiCommands = sut;
        return sut;
    }
}