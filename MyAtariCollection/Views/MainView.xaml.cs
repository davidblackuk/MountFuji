using MyAtariCollection.ViewModels;

namespace MyAtariCollection.Views;
public partial class MainView 
{
    private readonly MainViewModel mainViewModel;

    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();
        this.mainViewModel = mainViewModel;
        BindingContext = mainViewModel;
    }
}