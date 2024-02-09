namespace MountFujiTests.ViewModels;

[TestOf(typeof(AboutPopupViewModel))]
public class AboutPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigationMock;
    private Mock<IPersistance> persistanceMock;
    
    [SetUp]
    public void Setup()
    {
        popupNavigationMock = new Mock<IPopupNavigation>();
        persistanceMock = new Mock<IPersistance>();

    }

    [Test]
    public async Task CloseCommand_WhenInvoked_ShouldPopTheWindowOffTheStack()
    {
        var sut = CreateSut();
        await sut.CloseCommand.ExecuteAsync(null);
        popupNavigationMock.Verify(d => d.PopAsync(true), Times.Once);
    }


    private AboutPopupViewModel CreateSut()
    {
        return new AboutPopupViewModel(popupNavigationMock.Object, persistanceMock.Object);
    }
    
}