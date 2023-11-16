namespace MyAtariCollection.Services.CommandLineArgumentGenerators;

public class HardDiskCommandLineArguments : CommandLineArguments, IHardDiskCommandLineArguments
{
    public void Generate(AtariConfiguration config, StringBuilder builder)
    {
        AddDrives("acsi", config.AcsiImagePaths, builder);
        AddDrives("scsi", config.ScsiImagePaths, builder);
        AddIde(config, builder);
    }


    private void AddIde(AtariConfiguration config, StringBuilder builder)
    {
        if (!string.IsNullOrWhiteSpace(config.IdeOptions.Disk0))
        {
            AddQuotedFlag(builder, "ide-master", config.IdeOptions.Disk0);
            AddByteSwap(builder, 0, config.IdeOptions.ByteSwapDrive0);
        } 
        if (!string.IsNullOrWhiteSpace(config.IdeOptions.Disk1))
        {
            AddQuotedFlag(builder, "ide-slave", config.IdeOptions.Disk1);
            AddByteSwap(builder, 1, config.IdeOptions.ByteSwapDrive1);
        }
    }

    private void AddByteSwap(StringBuilder builder, int diskId, IdeByteSwap byteSwapOption)
    {
        string value = "off";
        
        if (byteSwapOption == IdeByteSwap.On)
        {
            value = "on";
        } else if (byteSwapOption == IdeByteSwap.Auto)
        {
            value = "auto";
        }
        
        AddIdQuotedIdValueFlag("ide-swap", diskId, value, builder);
    }

    private void AddDrives(string device, AcsiScsiDiskOptions imagePaths, StringBuilder builder)
    {
        AddIdQuotedIdValueFlag(device, 0, imagePaths.Disk0, builder);
        AddIdQuotedIdValueFlag(device, 1, imagePaths.Disk1, builder);
        AddIdQuotedIdValueFlag(device, 2, imagePaths.Disk2, builder);
        AddIdQuotedIdValueFlag(device, 3, imagePaths.Disk3, builder);
        AddIdQuotedIdValueFlag(device, 4, imagePaths.Disk4, builder);
        AddIdQuotedIdValueFlag(device, 5, imagePaths.Disk5, builder);
        AddIdQuotedIdValueFlag(device, 6, imagePaths.Disk6, builder);
        AddIdQuotedIdValueFlag(device, 7, imagePaths.Disk7, builder);
    }


}