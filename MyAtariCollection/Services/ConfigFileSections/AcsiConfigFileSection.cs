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
//      AddFlag(builder, "nDeviceType0", 0);

        AddDrive(builder, 1, config.IdeOptions.Disk1);
        AddFlag(builder, "nByteSwap1", (int)config.IdeOptions.ByteSwapDrive1); 
//      AddFlag(builder, "nDeviceType1", 0);
    }
}


/*
[HardDisk]
   nGemdosDrive = 0                     [add after acsi/scsi/ide, 0 = false, -1 = true]
   bBootFromHardDisk = FALSE
   bUseHardDiskDirectory = FALSE       [!String.isNullorEmpty(path)]
   szHardDiskDirectory = /
   nGemdosCase = 0
   nWriteProtection = 0
   bFilenameConversion = FALSE
   bGemdosHostTime = FALSE          [Super advanced, ignore for now]
*/