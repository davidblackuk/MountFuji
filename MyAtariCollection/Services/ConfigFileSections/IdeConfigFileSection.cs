using System.Text;
using MountFuji.Models;

namespace MountFuji.Services.ConfigFileSections;

public class IdeConfigFileSection : ConfigFileSection, IIdeConfigFileSection
{
    private const string ByteSwapDrive0Key = "nByteSwap0";
    private const string ByteSwapDrive1Key = "nByteSwap1";
    public const string ConfigSectionName = "IDE";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);


        AddDrive(builder, 0, config.IdeOptions.Disk0);
        AddFlag(builder, ByteSwapDrive0Key, (int)config.IdeOptions.ByteSwapDrive0);

        AddDrive(builder, 1, config.IdeOptions.Disk1);
        AddFlag(builder, ByteSwapDrive1Key, (int)config.IdeOptions.ByteSwapDrive1); 

        builder.AppendLine();    
        
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        to.IdeOptions.Disk0 = ParseDrive(sections[ConfigSectionName], 0 );
        to.IdeOptions.Disk1 = ParseDrive(sections[ConfigSectionName], 1 );

        to.IdeOptions.ByteSwapDrive0 = ParseEnumValue<IdeByteSwap>(ByteSwapDrive0Key, sections[ConfigSectionName]);
        to.IdeOptions.ByteSwapDrive1 = ParseEnumValue<IdeByteSwap>(ByteSwapDrive1Key, sections[ConfigSectionName]);
    }
}