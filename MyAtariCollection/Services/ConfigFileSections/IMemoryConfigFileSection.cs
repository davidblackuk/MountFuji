using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public interface IMemoryConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
    void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections);
}