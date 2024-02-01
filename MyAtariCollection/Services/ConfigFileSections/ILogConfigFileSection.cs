using System.Text;
using MountFuji.Models;

namespace MountFuji.Services.ConfigFileSections;

public interface ILogConfigFileSection
{
    void ToHatariConfig(StringBuilder builder, AtariConfiguration config);
}