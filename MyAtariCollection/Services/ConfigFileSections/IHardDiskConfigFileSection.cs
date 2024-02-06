using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public interface IHardDiskConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
    
    void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections);
}