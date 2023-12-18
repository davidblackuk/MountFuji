namespace MyAtariCollection.Services.ConfigFileSections;

public interface ISoundConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class SoundConfigFileSection: ConfigFileSection, ISoundConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Sound");
        
        AddFlag(builder, "bEnableSound", config.SoundOptions.Enabled);
        AddFlag(builder, "bEnableSoundSync", config.SoundOptions.Synchronized);
        
        AddFlag(builder, "nPlaybackFreq", (int)config.SoundOptions.PlaybackQuality);
        AddFlag(builder, "YmVolumeMixing", (int)config.SoundOptions.VoiceMixer);
        
    }
}

/*
    Future:
    szYMCaptureFileName = /hatari.wav
    nSdlAudioBufferSize = 0
    bEnableMicrophone = TRUE
*/