using System.Text;
using Microsoft.Extensions.Primitives;

namespace MyAtariCollection.Services;

public class OptionsGenerator
{
    protected void AddFlag(StringBuilder builder, string flag, string value)
    {
        builder.Append(" --");
        builder.Append(flag);
        builder.Append(" ");
        builder.Append(value);
    }
    protected void AddQuotedValue(StringBuilder builder, string flag, string value)
    {
        AddFlag(builder, flag, $" \"{value }\"");
    }

    protected void AddFlag(StringBuilder builder, string flag, bool value)
    {
        AddFlag(builder, flag, value ? "1" : "0");
    }
}