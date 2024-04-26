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

using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MountFuji.Services;

public interface IPersistence
{
    /// <summary>
    /// The root folder for storing save files from fuji
    /// </summary>
    string MountFujiFolder { get; }
    
    /// <summary>
    /// Full path to the preferences file
    /// </summary>
    string MountFujiPreferencesFile { get; }
    
    /// <summary>
    /// Full path to the systems file
    /// </summary>
    string MountFujiSystemsFile { get; }

    /// <summary>
    /// Full path to the global config file (keyboard settings etc
    /// </summary>
    string MountFujiGlobalSystemConfigurationFile { get; }
    
    /// <summary>
    /// Serialize an instance of type T to a json file
    /// </summary>
    /// <param name="filename">full path to the file that we are serializing to</param>
    /// <param name="toSerialize">The object to serialize</param>
    Task SerializeAsync<T>(string filename, T toSerialize);

    /// <summary>
    /// Deserialize an instance of type T from a specified file. This will return default of T if
    /// the file does not exist
    /// </summary>
    /// <param name="filename">full path to the file to deserialize from</param>
    /// <typeparam name="T">A type to deserialize, must have a default constructor</typeparam>
    /// <returns>An instance of type T</returns>
    Task<T> DeSerializeAsync<T>(string filename) where T: new();

    /// <summary>
    /// Deserialize an instance of type T from a specified file. This will return default of T if
    /// the file does not exist
    /// </summary>
    /// <param name="filename">full path to the file to deserialize from</param>
    /// <typeparam name="T">A type to deserialize, must have a default constructor</typeparam>
    /// <returns>An instance of type T</returns>
    T DeSerialize<T>(string filename) where T: new();
}

public class Persistence : IPersistence
{
    private readonly ILogger<Persistence> log;
    private const string AppPreferencesFilename = "preferences.json";
    private const string AppSystemsFilename = "systems.json";
    private const string GlobalSystemConfigurationFile = "globalSystemConfig.json";
    private const string AppDataFolder= "fuji";

    public string MountFujiFolder => Path.Combine(FileSystem.AppDataDirectory, AppDataFolder);

    public string MountFujiPreferencesFile => Path.Combine(MountFujiFolder, AppPreferencesFilename);
    public string MountFujiSystemsFile => Path.Combine(MountFujiFolder, AppSystemsFilename);

    public string MountFujiGlobalSystemConfigurationFile  => Path.Combine(MountFujiFolder, GlobalSystemConfigurationFile);

    public Persistence(ILogger<Persistence> log)
    {
        this.log = log;
    }
    
    private void EnsureFolderExists()
    {
        try
        {
            if (!Path.Exists(MountFujiFolder))
            {
                log.LogInformation("Creating App folder: {MountFujiFolder}", MountFujiFolder);
                Directory.CreateDirectory(MountFujiFolder);
            }
        }
        catch (Exception e)
        {
            log.LogError(e,"Error creating Fuji folder {MountFujiFolder}", MountFujiFolder);
        }
    }

    public async Task SerializeAsync<T>(string filename, T toSerialize)
    {
        try
        {
            EnsureFolderExists();
            using FileStream createStream = File.Create(filename);
            await JsonSerializer.SerializeAsync<T>(createStream, toSerialize, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }
        catch (Exception e)
        {
            log.LogInformation(e, "Error serializing type: {Type}", typeof(T).FullName);
        }
    }
    
    public T DeSerialize<T>(string filename)
    where T: new()
    {
        T deserialized = default;
        try
        {
            if (File.Exists(filename))
            {
                string data = File.ReadAllText(filename);
                deserialized = JsonSerializer.Deserialize<T>(data);
                if (deserialized is null)
                    log.LogInformation("Error deserializing type: {Type}, result is null", typeof(T).FullName);

            }
            else
            {
                log.LogInformation("File: {Filename}, does not exist, going with defaults", filename);
            }
        }
        catch (Exception e)
        {
            log.LogInformation(e, "Error deserializing type: {Type}", typeof(T).FullName);
        }
        return deserialized != null ? deserialized : new T();
    } 
    
    public async Task<T> DeSerializeAsync<T>(string filename)
        where T: new()
    {
        T deserialized = default;
        try
        {
            if (File.Exists(filename))
            {
                var data = File.Open(filename, FileMode.Open);
                deserialized = await JsonSerializer.DeserializeAsync<T>(data);
                if (deserialized is null)
                    log.LogInformation("Error deserializing type: {Type}, result is null", typeof(T).FullName);

            }
            else
            {
                log.LogInformation("File: {Filename}, does not exist, going with defaults", filename);
            }
        }
        catch (Exception e)
        {
            log.LogInformation(e, "Error deserializing type: {Type}", typeof(T).FullName);
        }
        return deserialized != null ? deserialized : new T();
    } 
}