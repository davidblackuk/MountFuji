using System.Text;

namespace MyAtariCollection.Services;

public interface ISystemOptionsGenerator
{
    void Generate(AtariConfiguration config, StringBuilder builder);
}