using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using MountFuji.Models.Keyboard;

namespace MountFuji.Converters;

public class KeyboardShortcutConverter: NaiveConverter, IMultiValueConverter
{
    public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        HatariShortcut res = HatariShortcut.Null;
        if (values.Length == 4 && values[0] is not null && values[1] is not null && values[2] is not null )
        {
            ShortcutModifier modifier = (ShortcutModifier)values[0];
            ShortcutKey key = (ShortcutKey)values[1];
            res = new HatariShortcut(modifier, key, values[2] as string, values[3] as string);
        }
        return res;
    }
    
    public new object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return null;
    }
}