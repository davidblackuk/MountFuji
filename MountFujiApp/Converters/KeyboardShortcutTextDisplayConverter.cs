using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace MountFuji.Converters;

public class KeyboardShortcutTextDisplayConverter: BaseConverterOneWay<string?, string>
{

    public override string ConvertFrom(string? value, CultureInfo culture)
    {
        return String.IsNullOrEmpty(value) ? "<not set>" : value;
    }

    public override string DefaultConvertReturnValue { get; set; }
}