using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public interface ILogConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
}