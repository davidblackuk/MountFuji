
using CommunityToolkit.Mvvm.ComponentModel;

namespace MyAtariCollection.Models;

[ObservableObject]
public partial class AtariConfiguration
{
    [ObservableProperty] 
    private string displayName;

    [ObservableProperty] 
    private AtariSystemType systemType;

    [ObservableProperty]
    private VideoTiming stVideoTiming;

}