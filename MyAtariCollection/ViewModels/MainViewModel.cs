
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyAtariCollection.Models;

namespace MyAtariCollection.ViewModels;

public partial class MainViewModel: TinyViewModel
{
    private readonly ISystemsService systemService;
    private readonly Random random = new();
    public MainViewModel(ISystemsService systemService)
    {
        this.systemService = systemService;
    }

    public override Task OnFirstAppear()
    {
        base.OnFirstAppear();
        return Task.CompletedTask;
    }
    

    [ObservableProperty]
    private ObservableCollection<AtariConfiguration> systems = new();

    [ObservableProperty] 
    private AtariConfiguration selectedConfiguration;
    
    [RelayCommand]
    private void CreateNewSystem()
    {
        // for now we just clone a random system
        var all = systemService.All().ToArray();
        var system = all[random.Next(all.Length)];
        Systems.Add(system);
        SelectedConfiguration = system;
    }

    [RelayCommand]
    private void SystemSelected(object a)
    {
        
    }
    
    
    
}