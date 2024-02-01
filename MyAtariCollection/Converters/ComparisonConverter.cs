namespace MountFuji.Converters;



/// <summary>
/// Comparison converter used to convert Enums to bools for
/// data binding several RadioButtons to a single property for example.
///
/// Re-read the source article if you need to deal with nullable bools or flags
/// 
/// Source: https://stackoverflow.com/questions/397556/how-to-bind-radiobuttons-to-an-enum
/// 
/// </summary>
public class ComparisonConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value?.Equals(parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : Binding.DoNothing;
    }
}