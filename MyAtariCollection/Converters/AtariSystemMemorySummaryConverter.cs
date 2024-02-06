using System.Globalization;
using MountFuji.Extensions;

namespace MountFuji.Converters;


/// <summary>
/// Convert a system configuration into a descriptive 1 line summary
/// </summary>
public class AtariSystemMemorySummaryConverter: NaiveConverter, IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string res = "";
        if (ValuesAllSet(values))
        {
            AtariSystemType systemType= (AtariSystemType)values[0];
            int stMemory = (int)values[1];
            int ttMemory = (int)values[2];

            res = $"{systemType.DisplayText()}, ram: {stMemory} ";
            if (stMemory > 255)
            {
                res += "KB";
            }
            else
            {
                res += "MB";
            }
            
            if (ttMemory > 0)
            {
                res += $", tt ram: {ttMemory}MB";
            }
        }
        return res;
    }

   
}