



using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MyAtariCollection.Models;

namespace MyAtariCollection.ViewModels;

public partial class MainViewModel: TinyViewModel
{
    private readonly ISystemsService systemService;
    
    public MainViewModel(ISystemsService systemService)
    {
        this.systemService = systemService;
    }

    public override Task OnFirstAppear()
    {
        base.OnFirstAppear();
        AllSystems = new ObservableCollection<AtariConfiguration>(systemService.All());
        return Task.CompletedTask;
    }

    [ObservableProperty]
    private ObservableCollection<AtariConfiguration> allSystems = new();
    
    

    
}