using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFujiTests.ViewModels.MainViewModelCommands;

public class IdeCommandsTests: CommandsTestBase
{
    [SetUp]
    public void Setup()
    {
        base.SetupMainViewModelMocks();
    }
    
    [Test]
    public void ClearIdeDiskImage_WhenInvoked_ShouldSetTheIdeImagePathsValueInTheSelectedConfigurationToEmptyString()
    {
        var expectedValue = "disk 1";
        SelectedConfiguration.IdeOptions.Disk1 = expectedValue;
        var sut = CreateSut();

        SelectedConfiguration.IdeOptions.Disk1.Should().Be(expectedValue);
        sut.ClearCommand.Execute(new MainViewModelItemId { ViewModel = MainViewModel, Id = 1});

        SelectedConfiguration.IdeOptions.Disk1.Should().BeEmpty();
    }
    
    [Test]
    public async Task BrowseIdeDiskImage_WhenInvoked_ShouldSetTheIdeImagePathValueInTheSelectedConfigFromTheFilePicker()
    {
        var expectedValue = "my disk filename"; 
        var sut = CreateSut();

        fujiFilePickerMock.Setup(x => x.PickFile(It.IsAny<string>(),
                It.IsAny<Action<string>>(), null))
            .Callback((string title, Action<string> action, string x) => action(expectedValue));

        await sut.BrowseCommand.ExecuteAsync(new MainViewModelItemId { ViewModel = MainViewModel, Id = 1});

        SelectedConfiguration.IdeOptions.Disk1.Should().Be(expectedValue); 
    }

   
    private IdeCommands CreateSut()
    {
        var sut = new IdeCommands(fujiFilePickerMock.Object, preferencesServiceMock.Object);
        MainViewModel.IdeCommands = sut;
        return sut;
    }
}