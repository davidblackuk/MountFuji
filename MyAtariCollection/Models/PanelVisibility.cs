namespace MyAtariCollection.Models;

public partial class PanelVisibility: ObservableObject
{
    [ObservableProperty] private bool showSystemPanel = true;
    
    [ObservableProperty] private bool showCpuPanel = true;

    [ObservableProperty] private bool showRomPanel = true;
    
    [ObservableProperty] private bool showAcsiHddPanel = false;
    
    [ObservableProperty] private bool showScsiHddPanel = false;

    [ObservableProperty] private bool showIdeHddPanel = false;

    [ObservableProperty] private bool showFloppyPanel = false;
}