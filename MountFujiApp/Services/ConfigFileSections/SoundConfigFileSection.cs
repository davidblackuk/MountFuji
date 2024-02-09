/*
   Mount Fuji - A front end for the Hatari Emulator
   Copyright (C) 2024  David Black

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public class SoundConfigFileSection: ConfigFileSection, ISoundConfigFileSection
{
    public const string ConfigSectionName = "Sound";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);
        
        AddFlag(builder, (string)"bEnableSound", (bool)config.SoundOptions.Enabled);
        AddFlag(builder, (string)"bEnableSoundSync", (bool)config.SoundOptions.Synchronized);
        
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