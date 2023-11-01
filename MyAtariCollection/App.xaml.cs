using TinyMvvm;

namespace MyAtariCollection;

public partial class App
{
    public App()
    {
        InitializeComponent();
        if (App.Current is App app)
        {
            //app.UserAppTheme = AppTheme.Dark;
        }
        MainPage = new AppShell();
    }
}