using System.Globalization;

namespace MountFuji.Converters;

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