using System.Globalization;
using MountFuji.ViewModels;
using MountFuji.ViewModels.MainViewModelCommands;

namespace MountFuji.Converters;

public class MainViewModelDiskIdConverter: NaiveConverter, IMultiValueConverter
{
    public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        MainViewModelDiskId res = null;
        if (ValuesAllSet(values))
        {
            res = new MainViewModelDiskId()
            {
                ViewModel = (MainViewModel)values[0],
                Id = (int) values[1],
            };
        }
        return res;
    }
    
    public new object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return null;
    }
}