namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public class CommandLineArguments
{
    protected void AddFlag(StringBuilder builder, string flag, string value)
    {
        builder.Append(" --");
        builder.Append(flag);
        builder.Append(" ");
        builder.Append(value);
    }
    protected void AddQuotedFlag(StringBuilder builder, string flag, string value)
    {
        AddFlag(builder, flag, $" \"{value }\"");
    }

    protected void AddFlag(StringBuilder builder, string flag, bool value)
    {
        AddFlag(builder, flag, value ? "1" : "0");
    }
    
    protected void AddFlag(StringBuilder builder, string flag, int value)
    {
        AddFlag(builder, flag, value.ToString());
    }
    
    protected void AddIdQuotedIdValueFlag(string flag, int id, string diskImage, StringBuilder builder)
    {
        if (!String.IsNullOrWhiteSpace(diskImage))
        {
            AddFlag(builder, flag, $"{id}=\"{diskImage}\"");
        }
    }
}