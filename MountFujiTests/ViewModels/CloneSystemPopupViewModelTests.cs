namespace MountFujiTests.ViewModels;

[TestOf(typeof(CloneSystemPopupViewModel))]
public class CloneSystemPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
        
        
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
    }

    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task CancelCommand_WhenInvoked_ShouldMarkThePopupAsNotConfirmed()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeFalse();
    }

    [Test]
    public async Task OkCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task OkCommand_WhenInvoked_ShouldMarkThePopupAsConfirmed()
    {
        var sut = CreateSut();
        await sut.OkCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeTrue();
    }
    
    [Test]
    public void OkCommand_WhenNameIsNotSet_ShouldBeDisabled()
    {
        var sut = CreateSut();
        sut.OkCommand.CanExecute(null).Should().BeFalse();
    }

    [Test]
    public void OkCommand_WhenNameIsSet_ShouldBeEnabled()
    {
        var sut = CreateSut();
        sut.NewName = "Fruit";
        sut.OkCommand.CanExecute(null).Should().BeTrue();
    }

    private CloneSystemPopupViewModel CreateSut()
    {
        return new CloneSystemPopupViewModel(popupNavigationMock.Object);
    }

}