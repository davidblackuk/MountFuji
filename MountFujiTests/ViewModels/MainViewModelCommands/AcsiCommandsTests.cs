using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class AcsiCommandsTests: CommandsTestBase
{
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
    }
    
    [Test]
    public void ClearAcsiDiskImage_WhenInvoked_ShouldSetTheAsciImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 2";
        SelectedConfiguration.AcsiImagePaths.Disk2 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue);
        sut.ClearCommand.Execute(new MainViewModelItemId { ViewModel = MainViewModel, Id = 2});

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

        await sut.BrowseCommand.ExecuteAsync(new MainViewModelItemId { ViewModel = MainViewModel, Id = 2});

        SelectedConfiguration.AcsiImagePaths.Disk2.Should().Be(expectedValue); 
    }
   
    private AcsiCommands CreateSut()
    {
        var sut = new AcsiCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object);
        MainViewModel.AcsiCommands = sut;
        return sut;
    }
}