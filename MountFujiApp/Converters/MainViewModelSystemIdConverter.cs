using System.Globalization;
using MountFuji.ViewModels;
using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFuji.Converters;

public class MainViewModelSystemIdConverter: NaiveConverter, IMultiValueConverter
{
    public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var x = values[0];
        var y = values[1];
        
        MainViewModelSystemId res = null;
        if (ValuesAllSet(values))
        {
            res = new MainViewModelSystemId()
            {
                ViewModel = (MainViewModel)values[0],
                Id = values[1].ToString(),
            };
        }
        return res;
    }
    
    public new object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return null;
    }
}