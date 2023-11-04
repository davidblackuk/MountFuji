using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Storage;


namespace MyAtariCollection.ViewModels;

public partial class MainViewModel : TinyViewModel
{
    private readonly ISystemsService systemService;
    private readonly ICommandLineOptionsService optionsService;
    private readonly IFilePicker filePicker;
    private readonly Random random = new();

    public MainViewModel(ISystemsService systemService, ICommandLineOptionsService optionsService, IFilePicker filePicker)
    {
        this.systemService = systemService;
        this.optionsService = optionsService;
        this.filePicker = filePicker;
    }

    public override Task OnFirstAppear()
    {
        base.OnFirstAppear();
        return Task.CompletedTask;
    }


    [ObservableProperty] private ObservableCollection<AtariConfiguration> systems = new();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasSelectedConfig))]
    private AtariConfiguration selectedConfiguration;


    public bool HasSelectedConfig => SelectedConfiguration != null;


    private int nextSystem = 0;

    #region ---- RELAY COMMANDS ----
    
        
    [RelayCommand()]
    private async void BrowseRoms()
    {
        var file = await filePicker.PickAsync();
        if (file != null)
        {
            SelectedConfiguration.RomImage = file.FullPath;
        }
    }

    [RelayCommand]
    private void ClearRom()
    {
        SelectedConfiguration.RomImage = String.Empty;
    }
    
    [RelayCommand]
    private void CreateNewSystem()
    {
        // for now we just clone a random system
        var all = systemService.All().ToArray();
        var system = all[nextSystem];

        nextSystem += 1;
        if (nextSystem >= all.Length) nextSystem = 0;

        Systems.Add(system);
        SelectedConfiguration = system;
    }

    [RelayCommand()]
    private void Run()
    {
        Console.WriteLine(optionsService.Generate(SelectedConfiguration));
    }

    #endregion
}