using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class ScsiCommandsTests: CommandsTestBase
{
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
    }
    
    [Test]
    public void ClearScsiDiskImage_WhenInvoked_ShouldSetTheSsciImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 2";
        SelectedConfiguration.ScsiImagePaths.Disk2 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().Be(expectedValue);
        sut.ClearCommand.Execute(new MainViewModelItemId { ViewModel = MainViewModel, Id = 2});

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

        await sut.BrowseCommand.ExecuteAsync(new MainViewModelItemId { ViewModel = MainViewModel, Id = 2});

        SelectedConfiguration.ScsiImagePaths.Disk2.Should().Be(expectedValue); 
    }
    
    private ScsiCommands CreateSut()
    {
        var sut = new ScsiCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object);
        MainViewModel.ScsiCommands = sut;
        return sut;
    }
}