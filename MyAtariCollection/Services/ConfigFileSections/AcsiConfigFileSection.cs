namespace MyAtariCollection.Services.ConfigFileSections;

public interface IAcsiConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class AcsiConfigFileSection : ConfigFileSection, IAcsiConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "ACSI");


        AddDrive(builder, 0, config.AcsiImagePaths.Disk0);
        AddDrive(builder, 1, config.AcsiImagePaths.Disk1);
        AddDrive(builder, 2, config.AcsiImagePaths.Disk2);
        AddDrive(builder, 3, config.AcsiImagePaths.Disk3);
        AddDrive(builder, 4, config.AcsiImagePaths.Disk4);
        AddDrive(builder, 5, config.AcsiImagePaths.Disk5);
        AddDrive(builder, 6, config.AcsiImagePaths.Disk6);
        AddDrive(builder, 7, config.AcsiImagePaths.Disk7);
    }
}

public interface IScsiConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class ScsiConfigFileSection : ConfigFileSection, IScsiConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "SCSI");


        AddDrive(builder, 0, config.ScsiImagePaths.Disk0);
        AddDrive(builder, 1, config.ScsiImagePaths.Disk1);
        AddDrive(builder, 2, config.ScsiImagePaths.Disk2);
        AddDrive(builder, 3, config.ScsiImagePaths.Disk3);
        AddDrive(builder, 4, config.ScsiImagePaths.Disk4);
        AddDrive(builder, 5, config.ScsiImagePaths.Disk5);
        AddDrive(builder, 6, config.ScsiImagePaths.Disk6);
        AddDrive(builder, 7, config.ScsiImagePaths.Disk7);
    }
}

public interface IIdeConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class IdeConfigFileSection : ConfigFileSection, IIdeConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "IDE");


        AddDrive(builder, 0, config.IdeOptions.Disk0);
        AddFlag(builder, "nByteSwap0", (int)config.IdeOptions.ByteSwapDrive0);

        AddDrive(builder, 1, config.IdeOptions.Disk1);
        AddFlag(builder, "nByteSwap1", (int)config.IdeOptions.ByteSwapDrive1); 
    }
}


public interface IHardDiskConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class HardDiskConfigFileSection : ConfigFileSection, IHardDiskConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "HardDisk");

        AddFlag(builder, "nGemdosDrive", config.GdosDriveOptions.AddGemdosAfterPhysicalDrives ? -1 : 0);
        AddFlag(builder, "bBootFromHardDisk", config.GdosDriveOptions.BootFromHardDisk);
        AddFlag(builder, "bUseHardDiskDirectory", !string.IsNullOrEmpty(config.GdosDriveOptions.GemdosFolder));
        AddFlag(builder, "szHardDiskDirectory", config.GdosDriveOptions.GemdosFolder);
 //     AddFlag(builder, "nGemdosCase", );
        AddFlag(builder, "nWriteProtection", (int) config.GdosDriveOptions.WriteProtection);
        AddFlag(builder, "bFilenameConversion", config.GdosDriveOptions.AtariHostFilenameConversion );

    }
}

public interface IFloppyConfigFileSection
{
    void Generate(StringBuilder builder, AtariConfiguration config);
}

public class FloppyConfigFileSection : ConfigFileSection, IFloppyConfigFileSection
{
    public void Generate(StringBuilder builder, AtariConfiguration config)
    {
        AddSection(builder, "Floppy");
        
        
        AddFlag(builder, "bAutoInsertDiskB", config.FloppyOptions.AutoInsertB);
        AddFlag(builder, "FastFloppy", config.FloppyOptions.FastFloppyAccess);

        AddFlag(builder, "EnableDriveA", config.FloppyOptions.DriveAEnabled);
        AddFlag(builder, "szDiskAFileName", config.FloppyOptions.DriveAPath);
        AddFlag(builder, "DriveA_NumberOfHeads", config.FloppyOptions.DriveADoubleSided ? 2 : 1);
        
        
        
        AddFlag(builder, "EnableDriveB", config.FloppyOptions.DriveBEnabled);
        AddFlag(builder, "szDiskBFileName", config.FloppyOptions.DriveBPath);
        AddFlag(builder, "DriveB_NumberOfHeads", config.FloppyOptions.DriveBDoubleSided ? 2 : 1);
        
        AddFlag(builder, "nWriteProtection", (int) config.FloppyOptions.WriteProtection);
        
    }
}

/*


filename is the zip file
zippath is within the zip file
   szDiskAZipPath =
   szDiskBZipPath =
   szDiskImageDirectory = /
   
*/