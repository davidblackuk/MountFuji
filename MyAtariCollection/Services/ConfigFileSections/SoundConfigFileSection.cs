namespace MyAtariCollection.Services.ConfigFileSections;

public class SoundConfigFileSection: ConfigFileSection, ISoundConfigFileSection
{
    public const string ConfigSectionName = "Sound";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder, "bEnableSound", config.SoundOptions.Enabled);
        AddFlag(builder, "bEnableSoundSync", config.SoundOptions.Synchronized);
        
        AddFlag(builder, "nPlaybackFreq", (int)config.SoundOptions.PlaybackQuality);
        AddFlag(builder, "YmVolumeMixing", (int)config.SoundOptions.VoiceMixer);

        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        var section = sections[ConfigSectionName];
        to.SoundOptions.Enabled = ParseBool("bEnableSound", section);
        to.SoundOptions.Synchronized = ParseBool("bEnableSoundSync", section);

        to.SoundOptions.PlaybackQuality = ParseEnumValue<PlaybackQuality>("nPlaybackFreq",section);
        to.SoundOptions.VoiceMixer = ParseEnumValue<YmVoiceMix>("YmVolumeMixing",section);
    }
}

/*
    Future:
    szYMCaptureFileName = /hatari.wav
    nSdlAudioBufferSize = 0
    bEnableMicrophone = TRUE
*/