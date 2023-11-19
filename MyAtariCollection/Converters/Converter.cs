using System.Globalization;

namespace MyAtariCollection.Converters;

public class NaiveConverter
{
    protected bool ValuesAllSet(object[] values)
    {
        for (int i = 0; i< values.Length; i++)
        {
            if (values[i] is null) return false;
        }
        return true;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return null;
    }
}