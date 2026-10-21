#ifndef AppVersion
  #error AppVersion must be supplied by the build.
#endif
[Setup]
AppId={{4B12540A-BC41-4A58-B4C6-B71C9D1FC664}
AppName=DeckPilot
AppVersion={#AppVersion}
AppPublisher=lolo34dr
AppPublisherURL=https://github.com/lolo34dr/deckpilot
AppSupportURL=https://github.com/lolo34dr/deckpilot/issues
DefaultDirName={autopf}\DeckPilot
DefaultGroupName=DeckPilot
DisableProgramGroupPage=yes
PrivilegesRequired=admin
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=..\artifacts\installer
OutputBaseFilename=DeckPilot-Setup
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
SetupIconFile=..\src\DeckPilot.Windows\Assets\DeckPilot.ico
LicenseFile=..\LICENSE
UninstallDisplayIcon={app}\DeckPilot.ico
CloseApplications=no
RestartApplications=no
SetupMutex=DeckPilot.Setup
UsePreviousTasks=yes

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Créer un raccourci sur le bureau"; Flags: checkedonce
Name: "startup"; Description: "Lancer DeckPilot avec Windows"; Flags: checkedonce

[Files]
Source: "..\artifacts\standalone\DeckPilot.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\DeckPilot.Windows\Assets\DeckPilot.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "DeckPilot.install"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\DeckPilot"; Filename: "{app}\DeckPilot.exe"; WorkingDir: "{app}"; IconFilename: "{app}\DeckPilot.ico"
Name: "{autodesktop}\DeckPilot"; Filename: "{app}\DeckPilot.exe"; WorkingDir: "{app}"; IconFilename: "{app}\DeckPilot.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\DeckPilot.exe"; Parameters: "--configure-startup on"; Flags: runhidden waituntilterminated runasoriginaluser; Tasks: startup; Check: ConfigureStartup
Filename: "{app}\DeckPilot.exe"; Parameters: "--configure-startup off"; Flags: runhidden waituntilterminated runasoriginaluser; Check: ConfigureNoStartup
Filename: "{app}\DeckPilot.exe"; Description: "Ouvrir DeckPilot"; Flags: postinstall nowait skipifsilent runasoriginaluser
Filename: "{app}\DeckPilot.exe"; Flags: nowait runasoriginaluser; Check: IsUpdate

[Code]
function IsUpdate: Boolean;
begin
  Result := ExpandConstant('{param:UPDATE|0}') <> '0';
end;

function ConfigureStartup: Boolean;
begin
  Result := not IsUpdate;
end;

function ConfigureNoStartup: Boolean;
begin
  Result := (not IsUpdate) and (not WizardIsTaskSelected('startup'));
end;

function StopDeckPilot(Exe: String; OriginalUser: Boolean): Boolean;
var Code, Attempt: Integer;
begin
  Result := True;
  if not CheckForMutexes('Local\DeckPilot.Windows') then exit;
  for Attempt := 1 to 20 do begin
    if OriginalUser then
      ExecAsOriginalUser(Exe, '--exit-for-install', '', SW_HIDE, ewWaitUntilTerminated, Code)
    else
      Exec(Exe, '--exit-for-install', '', SW_HIDE, ewWaitUntilTerminated, Code);
    Sleep(250);
    if not CheckForMutexes('Local\DeckPilot.Windows') then exit;
  end;
  Result := False;
end;

function PrepareToInstall(var NeedsRestart: Boolean): String;
begin
  ExtractTemporaryFile('DeckPilot.exe');
  if not StopDeckPilot(ExpandConstant('{tmp}\DeckPilot.exe'), True) then
    Result := 'Fermez DeckPilot depuis son icône près de l’horloge. Si un flash est en cours, attendez sa fin avant de réessayer.'
  else Result := '';
end;

function InitializeUninstall: Boolean;
begin
  Result := StopDeckPilot(ExpandConstant('{app}\DeckPilot.exe'), False);
  if not Result then
    MsgBox('Fermez DeckPilot avant de désinstaller. Attendez la fin de tout flash en cours.', mbError, MB_OK);
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var Users: TArrayOfString; I: Integer; Key, Command, Expected: String;
begin
  if CurUninstallStep <> usUninstall then exit;
  Expected := '"' + ExpandConstant('{app}\DeckPilot.exe') + '" --background';
  if RegGetSubkeyNames(HKEY_USERS, '', Users) then
    for I := 0 to GetArrayLength(Users) - 1 do begin
      Key := Users[I] + '\Software\Microsoft\Windows\CurrentVersion\Run';
      if RegQueryStringValue(HKEY_USERS, Key, 'DeckPilot', Command) and
         (CompareText(Command, Expected) = 0) then RegDeleteValue(HKEY_USERS, Key, 'DeckPilot');
    end;
end;
