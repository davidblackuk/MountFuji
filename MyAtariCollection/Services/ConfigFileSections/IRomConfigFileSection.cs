using System.Text;
using MountFuji.Models;

namespace MountFuji.Services.ConfigFileSections;

public interface IRomConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
    
    void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections);
}