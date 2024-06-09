// Mount Fuji - A front end for the Hatari Emulator
//    Copyright (C) 2024  David Black
// 
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
// 
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
// 
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.

using Microsoft.Extensions.Logging;

namespace MountFuji.Services;

public class RomService : IRomService
{
    private readonly IPreferencesService preferencesService;
    private readonly ILogger<RomService> log;

    private Dictionary<Country, string> contryImageMap = new Dictionary<Country, string>()
    {
        [Country.All] = "all.png", 
        [Country.Czech] = "cz.png", 
        [Country.Dutch] = "nl.png",
        [Country.Finnish] = "fi.png",
        [Country.French] = "fr.png",
        [Country.German] = "de.png",
        [Country.Greek] = "gr.png",
        [Country.Hungarian] = "hu.png",
        [Country.Italian] = "it.png",
        [Country.Norwegian] = "no.png",
        [Country.Polish] = "pl.png",
        [Country.Romanian] = "ro.png",
        [Country.Russian] = "ru.png",
        [Country.Spanish] = "es.png",
        [Country.Swedish] = "se.png",
        [Country.SwissGerman] = "se.png",
        [Country.Turkish] = "tr.png",
        [Country.UK] = "gb.png",
        [Country.US] = "us.png",
    };
    
    
    public RomService(IPreferencesService preferencesService, ILogger<RomService> log)
    {
        this.preferencesService = preferencesService;
        this.log = log;
    }
    
    public IEnumerable<Rom> GetRoms()
    {
        List<Rom> res = new List<Rom>();

        try
        {
            var files = Directory.EnumerateFiles(preferencesService.Preferences.RomFolder);
            foreach (string file in files)
            {
                string extension = Path.HasExtension(file) ? Path.GetExtension(file).ToLowerInvariant() : String.Empty;

                if (extension == ".img" || extension == ".rom")
                {
                    var stream = File.Open(file, FileMode.Open, FileAccess.Read);

                    var rom = new Rom
                    {
                        Country = GetCountry(stream),
                        Name = Path.GetFileName(file),
                        Path = file,
                        ReleaseDate = GetReleaseDate(stream),
                        VersionMajor = RetrieveMajorVersion(stream),
                        VersionMinor = RetrieveMinorVersion(stream),
                    };
                    rom.CountryFlag = GetCountryFlag(rom);
                    
                    
                    res.Add(rom);
                    log.LogInformation(rom.ToString());
                }
                else
                {
                    log.LogInformation("Rom service, skipped non-rom file {Rom}", file);
                }
            }
        }
        catch (Exception e)
        {
            log.LogError(e, "Error retrieving ROMs");
        }

        return res.OrderBy(r => r.VersionMajor).ThenBy(r => r.VersionMinor);
    }

    private string GetCountryFlag(Rom rom) => contryImageMap.ContainsKey(rom.Country) ? contryImageMap[rom.Country] : "unknown.png";

    private DateTime GetReleaseDate(FileStream stream)
    {
                        
        stream.Seek(0x1e, SeekOrigin.Begin);
        byte[] date = new byte[2];
        stream.Read(date, 0, 2);

        // in BDOS date format
        var rawDate = date[0] << 8 | date[1];

        // date format is 0xb 1111111_1111_11111
        //                       year month  day
        // year is added to 1980 to get actual value
        // so we're good for dates till 2107, the the atari internals book says the
        // max value is 119, which makes very little sense in 7 bits.
        var day = rawDate & 0b11111;
        var month = (rawDate >> 5) & 0b1111;
        var year = ((rawDate >> 9) & 0b1111111)+ 1980;
        return new DateTime(year, month, day);
    }

    private static Country GetCountry(FileStream stream)
    {
        stream.Seek(0x1d, SeekOrigin.Begin);
        byte[] country = new byte[1];
        stream.Read(country, 0, 1);
        return (Country)country[0];
    }

    private static int RetrieveMajorVersion(FileStream stream)
    {
        stream.Seek(2, SeekOrigin.Begin);

        byte[] version = new byte[2];
        stream.Read(version, 0, 2);

        return version[0];
    }
    
    private static int RetrieveMinorVersion(FileStream stream)
    {
        stream.Seek(2, SeekOrigin.Begin);

        byte[] version = new byte[2];
        stream.Read(version, 0, 2);

        return version[1];
    }
}