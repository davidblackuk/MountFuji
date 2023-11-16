using System;
namespace MyAtariCollection.Services.OptionsGenerators
{
	public interface IFloppyOptionsGenerator
	{
		void Generate(AtariConfiguration config, StringBuilder builder);
	}

	public class FloppyOptionsGenerator: OptionsGenerator, IFloppyOptionsGenerator
	{
		public FloppyOptionsGenerator()
		{
		}

		public void Generate(AtariConfiguration config, StringBuilder builder)
		{
			AddFlag(builder, "drive-a", config.FloppyOptions.DriveAEnabled);
			AddFlag(builder, "drive-b", config.FloppyOptions.DriveBEnabled);
			
			AddFlag(builder, "drive-a-heads", config.FloppyOptions.DriveADoubleSided ? 2 : 1);
			AddFlag(builder, "drive-b-heads", config.FloppyOptions.DriveBDoubleSided ? 2 : 1);
			
			AddQuotedFlag(builder, "disk-a", config.FloppyOptions.DriveAPath);
			AddQuotedFlag(builder, "disk-b", config.FloppyOptions.DriveBPath);
			
			AddFlag(builder, "fastfdc", config.FloppyOptions.FastFloppyAccess);

			AddFlag(builder, "protect-floppy", config.FloppyOptions.WriteProtection.ToString().ToLowerInvariant());			
		}
	}
}

