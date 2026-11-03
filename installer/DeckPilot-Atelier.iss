#ifndef AppVersion
  #error AppVersion must be supplied by the build.
#endif
[Setup]
AppId={{31AA8D19-EE5F-4D18-BAE7-A0E1EF1F76C3}
AppName=DeckPilot Atelier
AppVersion={#AppVersion}
AppPublisher=lolo34dr
AppPublisherURL=https://github.com/lolo34dr/deckpilot
AppSupportURL=https://github.com/lolo34dr/deckpilot/issues
DefaultDirName={autopf}\DeckPilot Atelier
DefaultGroupName=DeckPilot Atelier
DisableProgramGroupPage=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=..\artifacts\installer
OutputBaseFilename=DeckPilot-Atelier-Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
SetupIconFile=..\src\DeckPilot.Windows\Assets\DeckPilot.ico
LicenseFile=..\LICENSE
UninstallDisplayIcon={app}\DeckPilot.Factory.ico
CloseApplications=no
UsePreviousTasks=yes

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Créer un raccourci sur le bureau"; Flags: checkedonce

[Files]
Source: "..\artifacts\factory-standalone\DeckPilot.Factory.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\DeckPilot.Windows\Assets\DeckPilot.ico"; DestDir: "{app}"; DestName: "DeckPilot.Factory.ico"; Flags: ignoreversion
Source: "..\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\DeckPilot Atelier"; Filename: "{app}\DeckPilot.Factory.exe"; WorkingDir: "{app}"; IconFilename: "{app}\DeckPilot.Factory.ico"
Name: "{autodesktop}\DeckPilot Atelier"; Filename: "{app}\DeckPilot.Factory.exe"; WorkingDir: "{app}"; IconFilename: "{app}\DeckPilot.Factory.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\DeckPilot.Factory.exe"; Description: "Ouvrir DeckPilot Atelier"; Flags: postinstall nowait skipifsilent
