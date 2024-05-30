# Mount Fuji Windows Installer

The Mount Fuji Windows Installer is a Microsoft Windows installer for Mount Fuji built using Inno Setup.  It handles installing Mount Fuji (either unpackaged or MSIX-packaged) as well as the custom Hatari executable and `hatari.mfhat` wrapper, and the required .NET 8 runtime components.

The installer script, by default, will build an installer for the unpackaged version of Mount Fuji.  To build an installer for side-loading the MSIX-packaged version, you will need to modify the script by uncommenting the `#define PACKAGED_APP` setting in the `Setup.iss` file and also ensuring you have the digitial certificate used to sign the MSIX package in the correct folder.  See the ***Prerequisites*** section below for more details.

## Prerequisites
To successfully build the installer, the following prerequisites must be present on the computer building the installer:

1. [Inno Setup 6.2.2](https://jrsoftware.org/download.php/is.exe) (or later) and optionally [Inno Script Studio 2.5.1](https://www.kymoto.org/downloads/ISStudio_Latest.exe) (or later, recommended).  Note: If Inno Setup is installed anywhere but its default installation folder `%PROGRAMFILES(X86)%\Inno Setup 6`, the MountFujiWindowsInstaller project's `Post-build event command line` must be modifed to reference the actual file system location of the Inno Setup compiler `iscc.exe` file.

2. [.NET 8 Runtime 8.0.4](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) (or later) installers for [32-bit](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-8.0.4-windows-x86-installer) and [64-bit](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-8.0.4-windows-x64-installer) environments in the project's `Dependencies` folder.  Note: It is important to check the `Setup.iss` file's `#define DotNetRuntimeVersion` setting and modify it to match the version of the installers present in the `Dependencies` folder.

***If Building for an unpackaged app ...***

3. The compiled output of the `MountFujuApp` project in its `bin\{BuildConfiguration}\{WindowsTargetFramework}\win10-x64` folder, where:
	- `{BuildConfiguration}` is the solution build configuration whose output should be used, defined in the `#define BuildConfiguration` setting in the `Setup.iss` file.
	- `{WindowsTargetFramework}` is the target Windows .NET framework assocaited with the ouput, defined in the `#define WindowsTargetFramework` setting in the `Setup.iss` file.

	By default, these are defined as `Release` and `net8.0-windows10.0.19041.0`, respectively, so the output is expected to be in the `MountFujiApp` project's `bin\Release\net8.0-windows10.0.19041.0\win10-x64` folder.

	See the ***Modifying the Installer Script*** section below for details on how to changes these settings by modifing the script.

***Or, if building for a side-loaded MSIX-packaged app ...***

3. The MSIX-packaged output of the `MountFujuApp` project in its `bin\{BuildConfiguration}\{WindowsTargetFramework}\win10-x64\AppPackages\{MSIXRoot}\{MSIXPackageName}.msix` file and the digital certificate file `bin\{BuildConfiguration}\{WindowsTargetFramework}\win10-x64\AppPackages\{MSIXRoot}\{MSIXPackageName}.cer` used to sign the MSIX package, where:
	- `{BuildConfiguration}` is the solution build configuration whose output should be used, defined in the `#define BuildConfiguration` setting in the `Setup.iss` file.
	- `{WindowsTargetFramework}` is the target Windows .NET framework assocaited with the ouput, defined in the `#define WindowsTargetFramework` setting in the 
	- `{MSIXRoot}` is the root folder where the MSIX package is placed by Visual Studio, defined in the `#define MSIXRoot` setting in the `Setup.iss` file.
	- `{MSIXPackageName}` is the name of the MSIX package created by Visual Studio, defined in the `#define MSIXPackageName` setting in the `Setup.iss` file.

	By default, these are defined as `Release`, `net8.0-windows10.0.19041.0`, `MountFujiApp_X.X.X.X_Test`, and `MountFujiApp_X.X.X.X_x64` (`X.X.X.X` being the app version, defined in the `Setup.iss` file's `#define MyAppVersion` setting) respectively, so the output is expected to be in the `MountFujiApp` project's `bin\Release\net8.0-windows10.0.19041.0\win10-x64\MountFujiApp_X.X.X.X_Test\MountFujiApp_X.X.X.X_x64.msix` and `bin\Release\net8.0-windows10.0.19041.0\win10-x64\MountFujiApp_X.X.X.X_Test\MountFujiApp_X.X.X.X_x64.cer` files.

	See the ***Modifying the Installer Script*** section below for details on how to changes these settings by modifing the script.

## Modifying the Installer Script

To modify the installer script, open the `Setup.iss` file in Inno Script Studio (if installed, preferred), Inno Setup, directly in Visual Studio, or a text editor.  See the [Inno Setup Documentation](https://jrsoftware.org/isinfo.php) for more information on the script syntax and commands.

The `MountFujiWindowsInstaller` project doesn't depend on any Visual Studio extenstion for Inno Setup and is only used to organize the installer script and dependencies, and to trigger its post-build event that compiles the `Setup.iss` file and builds the installer.  See the ***Prerequisites*** section above for details on what the `Post-build event command line` needs to know in order to do this.

To manipulate Inno Setup scripts in a more cohesive way, there are several extensions for Visual Studio that offer various levels of integration with Inno Setup, such as [Visual & Installer](https://www.visual-installer.com/) (paid software).

## Building the Installer

To build the installer, perform a `Rebuild` action on the project (a `Build` action won't work unless `dummy.cs` has been modified).  If successful, the installer, named `MountFujiInstaller.exe` will be placed in the project's `Output` folder.  Errors and warnings will be shown in Visual Studio in the `Errors` and `Output` windows.

## Running the Installer

To run the installer, execute the `MountFujiInstaller.exe` file on the target Windows 10 or later computer.  It will install the required .NET 8 runtime, if needed, the Mount Fuji app (if unpackaged) or Mount Fuji MSIX package and digital certificate (if side-loading), custom Hatari executable and hatari.mfhat wrapper file, and register the `.mfhat` file extension and associate it with the custom Hatari executable.  Uninstaller entries are also created in the computer's `Add or remove programs` section.

## A Note On Unpackaged vs. MSIX-Packaged App Configuration

To build the MountFuji app as an unpackaged app (the default), the MountFujiApp project must be configured as follows:
1. Include `<WindowsPackageType Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">None</WindowsPackageType>` in an existing or new `<PropertyGroup>` tag in the `MountFujiApp.csproj` file.
2. Modify the `launchSettings.json` file in the `Properties` folder such that the `"commandName"` property is set to `"Project"`.

To build the MountFuji app as an MSIX package, the MountFujiApp project must be configured as follows:
1. Comment out the `<WindowsPackageType Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'windows'">None</WindowsPackageType>` in the `MountFujiApp.csproj` file.  You can also remove this tag, however it's useful to leave it in as a comment in case you want to switch back to building MountFuji as an unpackaged app.
2. Modify the `launchSettings.json` file in the `Properties` folder such that the `"commandName"` property is set to `"MsixPackage"`.