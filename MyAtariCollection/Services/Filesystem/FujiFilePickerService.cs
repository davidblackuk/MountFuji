using System.Diagnostics.CodeAnalysis;

namespace MyAtariCollection.Services.Filesystem;

public class FujiFilePickerService : IFujiFilePickerService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IPopupNavigation popupNavigation;

    public FujiFilePickerService(IServiceProvider serviceProvider, IPopupNavigation popupNavigation)
    {
        this.serviceProvider = serviceProvider;
        this.popupNavigation = popupNavigation;
    }
    
    public async Task<string> Pick([NotNull] Action<string> complete, string initialFolder = null)
    {
        string res = null;

        if (String.IsNullOrWhiteSpace(initialFolder))
        {
            initialFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        }
        
        var popup = serviceProvider.GetService<FujiFilePickerPopup>();
        popup.ViewModel.SetInitialFolder(initialFolder);
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (complete is not null & popup.ViewModel.Confirmed)
            {
                complete(popup.ViewModel.SelectedFile);
            }
        }; 
        
        return res;
    }
}