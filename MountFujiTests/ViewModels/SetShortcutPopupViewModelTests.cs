using MountFuji.Models.Keyboard;

namespace MountFujiTests.ViewModels;

public class SetShortcutPopupViewModelTests
{
    private Mock<IPopupNavigation> popupNavigateionMock;
    private Mock<IGlobalSystemConfigurationService> globalSystemConfigServiceMock;

    [SetUp]
    public void SetUp()
    {
        popupNavigateionMock = new Mock<IPopupNavigation>();
        globalSystemConfigServiceMock = new Mock<IGlobalSystemConfigurationService>();
    }

    [Test]
    public async Task CanceCommand_WhenInvoked_ShouldPopThePopupOffTheStack()
    {
        var sut = CreateSut();
        await sut.CancelCommand.ExecuteAsync(null);
        popupNavigateionMock.Verify(pn => pn.PopAsync(true), Times.Once);
    }
    

    [Test]
    public async Task OkCommand_WhenInvoked_ShouldPopThePopupOffTheStack()
    {
        var sut = CreateSut();
        sut.OriginalShortcut = new(ShortcutModifier.WithModifier, ShortcutKey.BossKey, "", "");
        sut.CurrentShortcut = "";
        await sut.OkCommand.ExecuteAsync(null);
        popupNavigateionMock.Verify(pn => pn.PopAsync(true), Times.Once);
    }
    
    [Test]
    public async Task OkCommand_WhenInvoked_ShouldSetTheShortcutViaTheConfigService()
    {
        var sut = CreateSut();

        var expectedModifier = ShortcutModifier.WithModifier;
        var expectedShortCut = ShortcutKey.Options;
        var expectedDescription = "description";
        var expectedDisplayValue = "displayValue";
        var newShortcut = "F1";
        
        sut.OriginalShortcut = new HatariShortcut(expectedModifier, expectedShortCut, expectedDescription, expectedDisplayValue);
        sut.CurrentShortcut = newShortcut;
        await sut.OkCommand.ExecuteAsync(null);
        globalSystemConfigServiceMock.Verify(g => g.SetShortcutKey(expectedModifier, expectedShortCut, newShortcut));
        
    }

    [Test]
    public async Task SetInitialState_WhenInvoked_ShouldShouldCorrectlyInitializeTheViewModel()
    {
        var expectedShortCut = ShortcutKey.Options;
        var expectedDisplayValue = "displayValue";
        
        var expectedShortcut = new HatariShortcut(ShortcutModifier.WithModifier, expectedShortCut, "", expectedDisplayValue);
        
        var sut = CreateSut();
        
        sut.SetInitialState(expectedShortcut);
        sut.OriginalShortcut.Should().Be(expectedShortcut);
        sut.CurrentShortcut.Should().Be(expectedDisplayValue);
    }

    [Test]
    public async Task SetKey_WhenInvoked_ShouldSetTheCurrentShortcut()
    {
        var sut = CreateSut();
        string expectedKey  = "a key";
        sut.SetKeyCommand.Execute(expectedKey);
        sut.CurrentShortcut.Should().Be(expectedKey);
    }
    
    private SetShortcutPopupViewModel CreateSut()
    {
        return new SetShortcutPopupViewModel(popupNavigateionMock.Object, globalSystemConfigServiceMock.Object);
    }
    
}