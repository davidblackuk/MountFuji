using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public class LogConfigFileSection: ConfigFileSection, ILogConfigFileSection
{
    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Log");
        
        AddFlag(builder, "sLogFileName", "stderr");
        AddFlag(builder, "sTraceFileName", "stderr");
        AddFlag(builder, "nTextLogLevel", "3");
        AddFlag(builder, "nAlertDlgLogLevel", "1");
        AddFlag(builder, "bConfirmQuit", true);
        AddFlag(builder, "bNatFeats", false);
        AddFlag(builder, "bConsoleWindow", false);
        
        builder.AppendLine();
    }
}