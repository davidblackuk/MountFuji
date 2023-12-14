using System.Globalization;

namespace MyAtariCollection.Services.ConfigFileSections;

public class ConfigFileSection
{
    protected void AddSection(StringBuilder builder, string name)
    {
        builder.AppendLine($"[{name}]");
    }

    protected void AddFlag(StringBuilder builder, string flag, string value)
    {
        builder.AppendLine($"{flag} = {value}");
    }
    
    protected void AddFlag(StringBuilder builder, string flag, bool value)
    {
        builder.AppendLine($"{flag} = {value.ToString(CultureInfo.InvariantCulture).ToUpper()}");
    }

    protected void AddFlag(StringBuilder builder, string flag, Int32 value)
    {
        builder.AppendLine($"{flag} = {value.ToString(CultureInfo.InvariantCulture).ToUpper()}");
    }
    
    protected void AddDrive(StringBuilder builder, int driveIndex, string path)
    {
        bool inuse = !String.IsNullOrWhiteSpace(path);
        AddFlag(builder, $"bUseDevice{driveIndex}", inuse);
        AddFlag(builder, $"sDeviceFile{driveIndex}", path);
        AddFlag(builder, $"nBlockSize{driveIndex}", 512);
    }
}