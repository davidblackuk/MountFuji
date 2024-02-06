namespace MountFujiTests.ViewModels;

[TestOf(typeof(DeleteSystemPopupViewModel))]
public class DeleteSystemPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
        
        
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
    }

    [Test]
    public async Task NoCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.NoCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task NoCommand_WhenInvoked_ShouldMarkThePopupAsNotConfirmed()
    {
        var sut = CreateSut();
        await sut.NoCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeFalse();
    }

    [Test]
    public async Task YesCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.YesCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }

    [Test]
    public async Task YesCommand_WhenInvoked_ShouldMarkThePopupAsConfirmed()
    {
        var sut = CreateSut();
        await sut.YesCommand.ExecuteAsync(null);
        sut.Confirmed.Should().BeTrue();
    }
    
    private DeleteSystemPopupViewModel CreateSut()
    {
        return new DeleteSystemPopupViewModel(popupNavigationMock.Object);
    }
    
}

