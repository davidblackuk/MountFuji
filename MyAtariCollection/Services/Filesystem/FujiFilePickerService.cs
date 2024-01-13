using System.Diagnostics.CodeAnalysis;

namespace MyAtariCollection.Services.Filesystem;

public class FujiFilePickerService : IFujiFilePickerService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IPopupNavigation popupNavigation;

    private string lastFolderAccessed;
    
    public FujiFilePickerService(IServiceProvider serviceProvider, IPopupNavigation popupNavigation)
    {
        this.serviceProvider = serviceProvider;
        this.popupNavigation = popupNavigation;
    }
    public async Task<string> PickFolder(string title, Action<string> complete, string initialFolder = null)
    {
        return await Initialize(PickerType.Folder, title, complete, initialFolder);
    }
    
    public async Task<string> PickFile([NotNull]string title, [NotNull] Action<string> complete, string initialFolder = null)
    {
        return await Initialize(PickerType.File, title, complete, initialFolder);
    }

    private async Task<string> Initialize(PickerType pickerType, string title, Action<string> complete, string initialFolder)
    {
        string res = null;

        if (String.IsNullOrWhiteSpace(initialFolder))
        {
            initialFolder = String.IsNullOrWhiteSpace(lastFolderAccessed) ? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) : lastFolderAccessed;
        }
        
        var popup = serviceProvider.GetService<FujiFilePickerPopup>();
        popup.ViewModel.Title = title;
        popup.ViewModel.PickerType = pickerType;
        popup.ViewModel.SetInitialFolder(initialFolder);
        await popupNavigation.PushAsync(popup);

        popup.Disappearing += (sender, args) =>
        {
            if (complete is not null & popup.ViewModel.Confirmed && pickerType == PickerType.File)
            {
                lastFolderAccessed = popup.ViewModel.CurrentFolder;
                complete(popup.ViewModel.SelectedEntry.Path);
            }
            else  if (complete is not null & popup.ViewModel.Confirmed && pickerType == PickerType.Folder)
            {
                lastFolderAccessed = popup.ViewModel.CurrentFolder;
                complete(popup.ViewModel.CurrentFolder);
            }
        }; 
        
        return res;
    }
}