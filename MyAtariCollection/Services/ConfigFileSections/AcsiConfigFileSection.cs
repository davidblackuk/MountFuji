using System.Text;

namespace MountFuji.Services.ConfigFileSections;

public class AcsiConfigFileSection : ConfigFileSection, IAcsiConfigFileSection
{
    public const string ConfigSectionName = "ACSI";

    public void ToHatariConfig(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, ConfigSectionName);


        AddDrive(builder, 0, config.AcsiImagePaths.Disk0);
        AddDrive(builder, 1, config.AcsiImagePaths.Disk1);
        AddDrive(builder, 2, config.AcsiImagePaths.Disk2);
        AddDrive(builder, 3, config.AcsiImagePaths.Disk3);
        AddDrive(builder, 4, config.AcsiImagePaths.Disk4);
        AddDrive(builder, 5, config.AcsiImagePaths.Disk5);
        AddDrive(builder, 6, config.AcsiImagePaths.Disk6);
        AddDrive(builder, 7, config.AcsiImagePaths.Disk7);
        
        builder.AppendLine();
    }

    public void FromHatariConfig(AtariConfiguration to, Dictionary<string, Dictionary<string, string>> sections)
    {
        to.AcsiImagePaths.Disk0 = ParseDrive(sections[ConfigSectionName], 0 );
        to.AcsiImagePaths.Disk1 = ParseDrive(sections[ConfigSectionName], 1 );
        to.AcsiImagePaths.Disk2 = ParseDrive(sections[ConfigSectionName], 2 );
        to.AcsiImagePaths.Disk3 = ParseDrive(sections[ConfigSectionName], 3 );
        to.AcsiImagePaths.Disk4 = ParseDrive(sections[ConfigSectionName], 4 );
        to.AcsiImagePaths.Disk5 = ParseDrive(sections[ConfigSectionName], 5 );
        to.AcsiImagePaths.Disk6 = ParseDrive(sections[ConfigSectionName], 6 );
        to.AcsiImagePaths.Disk7 = ParseDrive(sections[ConfigSectionName], 7 );
    }



    
}




/*


filename is the zip file
zippath is within the zip file
   szDiskAZipPath =
   szDiskBZipPath =
   szDiskImageDirectory = /
   
*/