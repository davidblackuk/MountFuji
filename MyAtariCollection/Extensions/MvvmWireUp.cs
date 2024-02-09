using Mopups.Services;
using MountFuji.ViewModels;
using MountFuji.Views;

namespace MountFuji.Extensions;

public static class MvvmWireUp {

    /// <summary>
    /// Adds the Views and ViewModels from MVVM to the app
    /// </summary>
    /// <param name="services"></param>
    public static void AddViewViewModels(this IServiceCollection services)
    {
        services.AddSingleton<IPopupNavigation>(MopupService.Instance);
        
        services.AddTransient<MainView>();
        services.AddTransient<MainViewModel>();

        services.AddTransient<NewSystemPopup>();
        services.AddTransient<NewSystemViewModelViewModel>();
        
        services.AddTransient<IPreferencesPopup, PreferencesPopup>();
        services.AddTransient<PreferencesPopupViewModel>();
        
        services.AddTransient<FujiFilePickerPopup>();
        services.AddTransient<FujiFilePickerPopupViewModel>();
        
        services.AddTransient<CloneSystemPopup>();
        services.AddTransient<CloneSystemPopupViewModel>();
        
        services.AddTransient<DeleteSystemPopup>();
        services.AddTransient<DeleteSystemPopupViewModel>();

        services.AddTransient<ImportSystemPopup>();
        services.AddTransient<ImportSystemPopupViewModel>();
        
        services.AddTransient<AboutPopup>();
        services.AddTransient<AboutPopupViewModel>();
    }
}