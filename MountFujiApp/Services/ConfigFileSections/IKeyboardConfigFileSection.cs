using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public interface IKeyboardConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, GlobalSystemConfiguration config);

    public void FromHatariConfig(KeyboardOptions to, Dictionary<string, Dictionary<string, string>> sections);
}