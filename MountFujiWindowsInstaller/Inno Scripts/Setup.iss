;
; Copyright (C) 2024  David Black
;
; This program is free software: you can redistribute it and/or modify
; it under the terms of the GNU General Public License as published by
; the Free Software Foundation, either version 3 of the License, or
; (at your option) any later version.
;
; This program is distributed in the hope that it will be useful,
; but WITHOUT ANY WARRANTY; without even the implied warranty of
; MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
; GNU General Public License for more details.
;
; You should have received a copy of the GNU General Public License
; along with this program.  If not, see <https://www.gnu.org/licenses/>.
;

; NOTE: Uncomment this line to build a setup for MountFuji using the MSIX package rather than the unpackaged applicaiton files
;#define PACKAGED_APP

; Setup metadata
#define MyAppName "Mount Fuji"
#define MyAppVersion "1.0.4.2"
#define MyAppPublisher "davidblackuk"
#define MyAppURL "https://github.com/davidblackuk/MountFuji/"
#define MyAppAuthor "David Black"
#define MyAppCopyright "Copyright (C) 2024 " + MyAppAuthor

; Runtime dependency and app details
#define DotNetRuntimeVersion "8.0.4"
#define CustomHatariVersionFolder "Hatari2.4.1-Windows"
#define BuildConfigutation "Release"
#define WindowsTargetFramework "net8.0-windows10.0.19041.0"
#ifdef PACKAGED_APP
  #define MSIXRoot "MountFujiApp_" + MyAppVersion + "_Test"
  #define MSIXPackageName "MountFujiApp_" + MyAppVersion + "_x64"
#endif

; Support routines for data file management
#include "DataFiles.ish"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{0728FE60-0948-435F-8B04-1159F083480D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultGroupName={#MyAppName}
LicenseFile=..\..\license.txt
OutputDir=..\Output
OutputBaseFilename=MountFujiInstaller
SetupIconFile=..\Assets\MountFuji.ico
Compression=lzma
SolidCompression=yes
Uninstallable=yes
ChangesAssociations=True
MinVersion=0,10.0.17763
AppCopyright={#MyAppCopyright}
DefaultDirName={commonpf}\Mount Fuji
ArchitecturesInstallIn64BitMode=x64
ArchitecturesAllowed=x86 x64
UninstallDisplayIcon={app}\MountFuji.ico
WizardImageFile=..\Assets\MountFuji-100.bmp,..\Assets\MountFuji-125.bmp,..\Assets\MountFuji-150.bmp,..\Assets\MountFuji-175.bmp,..\Assets\MountFuji-200.bmp,..\Assets\MountFuji-225.bmp,..\Assets\MountFuji-250.bmp
WizardSmallImageFile=..\Assets\MountFuji-Small-100.bmp,..\Assets\MountFuji-Small-125.bmp,..\Assets\MountFuji-Small-150.bmp,..\Assets\MountFuji-Small-175.bmp,..\Assets\MountFuji-Small-200.bmp,..\Assets\MountFuji-Small-225.bmp,..\Assets\MountFuji-Small-250.bmp
WizardImageStretch=no
WizardStyle=modern
VersionInfoVersion={#MyAppVersion}
VersionInfoCompany={#MyAppAuthor}
VersionInfoCopyright={#MyAppCopyright}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
#ifdef PACKAGED_APP
  Source: "..\..\MountFujiApp\bin\{#BuildConfigutation}\{#WindowsTargetFramework}\win10-x64\AppPackages\{#MSIXRoot}\{#MSIXPackageName}.cer"; DestDir: "{tmp}"; Flags: deleteafterinstall
  Source: "..\..\MountFujiApp\bin\{#BuildConfigutation}\{#WindowsTargetFramework}\win10-x64\AppPackages\{#MSIXRoot}\{#MSIXPackageName}.msix"; DestDir: "{tmp}"; Flags: nocompression deleteafterinstall
#else
  Source: "..\..\MountFujiApp\bin\{#BuildConfigutation}\{#WindowsTargetFramework}\win10-x64\MountFuji.exe"; DestDir: "{app}\App"; Flags: restartreplace
  Source: "..\..\MountFujiApp\bin\{#BuildConfigutation}\{#WindowsTargetFramework}\win10-x64\*"; DestDir: "{app}\App"; Flags: createallsubdirs recursesubdirs restartreplace
#endif
Source: "..\..\{#CustomHatariVersionFolder}\win32\hatari.exe"; DestDir: "{app}\Hatari"; Flags: 32bit restartreplace; Check: Not IsWin64
Source: "..\..\{#CustomHatariVersionFolder}\win64\hatari.exe"; DestDir: "{app}\Hatari"; Flags: 64bit restartreplace; Check: IsWin64
Source: "..\..\{#CustomHatariVersionFolder}\win32\SDL2.dll"; DestDir: "{app}\Hatari"; Flags: 32bit restartreplace; Check: Not IsWin64
Source: "..\..\{#CustomHatariVersionFolder}\win64\SDL2.dll"; DestDir: "{app}\Hatari"; Flags: 64bit restartreplace; Check: IsWin64
Source: "..\..\{#CustomHatariVersionFolder}\hatari.mfhat"; DestDir: "{app}\Hatari"; Flags: restartreplace;
Source: "..\Dependencies\dotnet-runtime-{#DotNetRuntimeVersion}-win-x64.exe"; DestDir: "{tmp}"; Flags: 64bit nocompression deleteafterinstall; Check: IsWin64
Source: "..\Dependencies\dotnet-runtime-{#DotNetRuntimeVersion}-win-x86.exe"; DestDir: "{tmp}"; Flags: 32bit nocompression deleteafterinstall; Check: Not IsWin64
Source: "..\Assets\MountFuji.ico"; DestDir: "{app}";

[Icons]
#ifndef PACKAGED_APP
  Name: "{group}\Mount Fuji"; Filename: "{app}\App\MountFuji.exe"; WorkingDir: "{app}\App"; IconFilename: "{app}\App\MountFuji.exe"
#endif

[Run]
Filename: "{tmp}\dotnet-runtime-{#DotNetRuntimeVersion}-win-x64.exe"; Parameters: "/install /quiet /norestart"; WorkingDir: "{tmp}"; Flags: runascurrentuser waituntilterminated 64bit; Description: "Installs .NET 8 64-bit runtime"; StatusMsg: "Installing .NET 8 64-bit Runtime..."; Check: IsWin64
Filename: "{tmp}\dotnet-runtime-{#DotNetRuntimeVersion}-win-x86.exe"; Parameters: "/install /quiet /norestart"; WorkingDir: "{tmp}"; Flags: runascurrentuser waituntilterminated 32bit; Description: "Installs .NET 8 32-bit runtime"; StatusMsg: "Installing .NET 8 32-bit Runtime..."; Check: Not IsWin64
Filename: "cmd.exe"; Parameters: "/c assoc .mfhat=hatarifile"; WorkingDir: "{tmp}"; Flags: runascurrentuser waituntilterminated runhidden; Description: "Creates .mfhat extension association"; StatusMsg: "Creating .mfhat association..."
Filename: "cmd.exe"; Parameters: "/c ftype hatarifile=""{app}\Hatari\hatari.exe"" ""%1"""; WorkingDir: "{tmp}"; Flags: runascurrentuser waituntilterminated runhidden; Description: "Associates .mfhat file type with Hatari executable"; StatusMsg: "Associating .mfhat files with Hatari executable..."
#ifdef PACKAGED_APP
  Filename: "powershell.exe"; Parameters: "-ExecutionPolicy Bypass -Command Import-Certificate -FilePath '{tmp}\{#MSIXPackageName}.cer' -CertStoreLocation 'Cert:\LocalMachine\Root'"; WorkingDir: "{tmp}"; Flags: runascurrentuser waituntilterminated runhidden; Description: "Installs the WinUI App Certificate associated with the Mount Fuji application"; StatusMsg: "Installing self-signed application certificate..."
  Filename: "powershell.exe"; Parameters: "-ExecutionPolicy Bypass -Command Add-AppxPackage '{tmp}\{#MSIXPackageName}.msix'"; WorkingDir: "{tmp}"; Flags: runascurrentuser waituntilterminated runhidden
#endif

[UninstallRun]
Filename: "cmd.exe"; Parameters: "/c ftype hatarifile="; WorkingDir: "{app}\Hatari"; Flags: waituntilterminated runhidden; RunOnceId: "MountFuji"
Filename: "cmd.exe"; Parameters: "/c assoc .mfhat="; WorkingDir: "{app}\Hatari"; Flags: waituntilterminated runhidden; RunOnceId: "MountFuji"

[Code]
procedure CurStepChanged(CurStep: TSetupStep);
begin
  // After all files have been installed and commands have been run...
  if CurStep = ssPostInstall then
  begin
    // Look for and gather any existing data files from previous versions.  If successful...
    if GatherPreviousDataFiles then
    begin
      // Update the preferences data file with the location of the new hatari.mfhat file.
      if not UpdateHatariApplicationProperty(ExpandConstant('{app}\Hatari\hatari.mfhat')) then
      begin
        MsgBox('Failed to set HatariApplication property in preferences file', mbError, MB_OK);
      end;
    end;
  end;
end;