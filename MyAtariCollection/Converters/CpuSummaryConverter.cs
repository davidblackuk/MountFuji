using System.Globalization;
using MountFuji.Models;

namespace MountFuji.Converters;

public class CpuSummaryConverter: NaiveConverter, IMultiValueConverter
{
    public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string res = String.Empty;
        if (ValuesAllSet(values))
        {
            CpuType cpuType = (CpuType)values[0];
            CpuClock clock = (CpuClock)values[1];
            FpuType fpuType = (FpuType)values[2];

            res = $"cpu: {cpuType.ToString().Replace("MC", "")}";
            res = $"{res}@{clock.ToString().Replace("Clock", "")}";
            res = $"{res}, fpu: {fpuType.ToString().Replace("MC", "")}";
        }

        return res;
    }

}

public class TemplateSummaryConverter: CpuSummaryConverter {
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string res = null;

        if (ValuesAllSet(values))
        {
            res = (string)base.Convert(values, targetType, parameter, culture);

            int stMemory = (int)values[3];

            string postfix = "MB";
            if (stMemory > 255)
            {
                postfix = "KB";
            }

            res = $"{stMemory} {postfix} {res}";
        }

        return res;
    }
}