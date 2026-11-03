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
Filename: "{app}\DeckPilot.Factory.exe"; Description: "Ouvrir DeckPilot Atelier"; Flags: postinstall nowait skipifsilent runasoriginaluser
Filename: "{app}\DeckPilot.Factory.exe"; Flags: nowait runasoriginaluser; Check: IsUpdate

[Code]
function IsUpdate: Boolean;
begin
  Result := ExpandConstant('{param:UPDATE|0}') <> '0';
end;

function StopFactory(Exe: String; OriginalUser: Boolean): Boolean;
var Code, Attempt: Integer;
begin
  Result := True;
  if not CheckForMutexes('Local\DeckPilot.Factory') then exit;
  for Attempt := 1 to 20 do begin
    if OriginalUser then
      ExecAsOriginalUser(Exe, '--exit-for-install', '', SW_HIDE, ewWaitUntilTerminated, Code)
    else
      Exec(Exe, '--exit-for-install', '', SW_HIDE, ewWaitUntilTerminated, Code);
    Sleep(250);
    if not CheckForMutexes('Local\DeckPilot.Factory') then exit;
  end;
  Result := False;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
begin
  ExtractTemporaryFile('DeckPilot.Factory.exe');
  if not StopFactory(ExpandConstant('{tmp}\DeckPilot.Factory.exe'), True) then
    Result := 'Fermez DeckPilot Atelier et attendez la fin du flash avant de réessayer.'
  else Result := '';
end;

function InitializeUninstall: Boolean;
begin
  Result := StopFactory(ExpandConstant('{app}\DeckPilot.Factory.exe'), False);
  if not Result then MsgBox('Fermez DeckPilot Atelier et attendez la fin du flash avant de désinstaller.', mbError, MB_OK);
end;
