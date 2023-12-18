namespace MyAtariCollection.Models;

public partial class SoundOptions: ObservableObject
{
    [ObservableProperty] private bool enabled = true;
    [ObservableProperty] private bool synchronized = false;
    [ObservableProperty] private PlaybackQuality playbackQuality = PlaybackQuality.Hz44100;
    [ObservableProperty] private YmVoiceMix voiceMixer = YmVoiceMix.StTable;
    
}