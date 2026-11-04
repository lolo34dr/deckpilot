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
Name: "desktopicon"; Description: "Créer un raccourci sur le bureau"; Languages: french; Flags: checkedonce
Name: "desktopicon"; Description: "Create a desktop shortcut"; Languages: english; Flags: checkedonce

[Files]
Source: "..\artifacts\standalone\DeckPilot.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\DeckPilot.Windows\Assets\DeckPilot.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "DeckPilot.install"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\src\DeckPilot.Windows\Install-VBCABLE.ps1"; DestDir: "{app}"; Flags: ignoreversion
Source: "Export-DeckPilotPreferences.ps1"; Flags: dontcopy
Source: "..\LICENSE"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\DeckPilot"; Filename: "{app}\DeckPilot.exe"; WorkingDir: "{app}"; IconFilename: "{app}\DeckPilot.ico"
Name: "{autodesktop}\DeckPilot"; Filename: "{app}\DeckPilot.exe"; WorkingDir: "{app}"; IconFilename: "{app}\DeckPilot.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\DeckPilot.exe"; Parameters: "--configure-startup {code:StartupArg}"; Flags: runhidden waituntilterminated runasoriginaluser; Check: ConfigureStartup
Filename: "{app}\DeckPilot.exe"; Parameters: "--configure-preferences {code:AutoUpdatesArg} {code:ReduceMotionArg}"; Flags: runhidden waituntilterminated runasoriginaluser; Check: ConfigurePreferences
Filename: "{sys}\WindowsPowerShell\v1.0\powershell.exe"; Parameters: "-NoProfile -ExecutionPolicy Bypass -File ""{app}\Install-VBCABLE.ps1"" {code:DriverInstallArgument}"; Flags: runhidden waituntilterminated; StatusMsg: "Recherche, vérification et installation automatique de VB-CABLE…"; Languages: french
Filename: "{sys}\WindowsPowerShell\v1.0\powershell.exe"; Parameters: "-NoProfile -ExecutionPolicy Bypass -File ""{app}\Install-VBCABLE.ps1"" {code:DriverInstallArgument}"; Flags: runhidden waituntilterminated; StatusMsg: "Checking, verifying and automatically installing VB-CABLE…"; Languages: english
Filename: "{app}\DeckPilot.exe"; Parameters: "--configure-microphone-if-needed"; Flags: waituntilterminated runasoriginaluser skipifsilent
Filename: "{app}\DeckPilot.exe"; Description: "Ouvrir DeckPilot"; Flags: postinstall nowait skipifsilent runasoriginaluser; Check: not IsUpdate
Filename: "{app}\DeckPilot.exe"; Flags: nowait runasoriginaluser; Check: IsUpdate

[Code]
var
  PreferencesPage: TWizardPage;
  StartupOption, AutoUpdatesOption, ReduceMotionOption: TNewCheckBox;
  PreferencesAvailable: Boolean;
  InitialStartup, InitialAutoUpdates, InitialReduceMotion: Boolean;
  SavedStartup, SavedAutoUpdates, SavedReduceMotion: Boolean;
  SetupStateInitialized, UpdateInstall: Boolean;

function PreviousInstallFound: Boolean;
var ExistingDirectory: String;
begin
  Result := False;
  ExistingDirectory := '';
  if not RegQueryStringValue(HKEY_LOCAL_MACHINE,
    'Software\Microsoft\Windows\CurrentVersion\Uninstall\{4B12540A-BC41-4A58-B4C6-B71C9D1FC664}_is1',
    'Inno Setup: App Path', ExistingDirectory) then
    RegQueryStringValue(HKEY_LOCAL_MACHINE,
      'Software\Microsoft\Windows\CurrentVersion\Uninstall\{4B12540A-BC41-4A58-B4C6-B71C9D1FC664}_is1',
      'InstallLocation', ExistingDirectory);
  if ExistingDirectory <> '' then
    Result := FileExists(AddBackslash(ExistingDirectory) + 'DeckPilot.install');
end;

function IsUpdate: Boolean;
begin
  if SetupStateInitialized then Result := UpdateInstall
  else Result := (ExpandConstant('{param:UPDATE|0}') <> '0') or PreviousInstallFound;
end;

function ParseOnOff(const Value: String; var Parsed: Boolean): Boolean;
begin
  Result := True;
  if CompareText(Value, 'on') = 0 then Parsed := True
  else if CompareText(Value, 'off') = 0 then Parsed := False
  else Result := False;
end;

function LoadPreferenceArguments: Boolean;
var StartupValue, AutoUpdatesValue, ReduceMotionValue: String;
begin
  StartupValue := ExpandConstant('{param:STARTUP|}');
  AutoUpdatesValue := ExpandConstant('{param:AUTOCHECKUPDATES|}');
  ReduceMotionValue := ExpandConstant('{param:REDUCEMOTION|}');
  Result := ParseOnOff(StartupValue, InitialStartup) and
    ParseOnOff(AutoUpdatesValue, InitialAutoUpdates) and
    ParseOnOff(ReduceMotionValue, InitialReduceMotion);
end;

function LoadExportedPreferences: Boolean;
var OutputPath, ScriptPath, PowerShellPath, Parameters: String; Code: Integer; Values: TArrayOfString;
begin
  Result := False;
  OutputPath := ExpandConstant('{tmp}\DeckPilot-installer-preferences.txt');
  DeleteFile(OutputPath);
  ExtractTemporaryFile('Export-DeckPilotPreferences.ps1');
  ScriptPath := ExpandConstant('{tmp}\Export-DeckPilotPreferences.ps1');
  PowerShellPath := ExpandConstant('{sys}\WindowsPowerShell\v1.0\powershell.exe');
  Parameters := '-NoProfile -ExecutionPolicy Bypass -File "' + ScriptPath + '" -OutputPath "' + OutputPath + '"';
  if not ExecAsOriginalUser(PowerShellPath, Parameters, '', SW_HIDE, ewWaitUntilTerminated, Code) then exit;
  if (Code <> 0) or not LoadStringsFromFile(OutputPath, Values) or (GetArrayLength(Values) < 3) then exit;
  Result := ParseOnOff(Values[0], InitialStartup) and
    ParseOnOff(Values[1], InitialAutoUpdates) and
    ParseOnOff(Values[2], InitialReduceMotion);
  if Result then begin
    SavedStartup := InitialStartup;
    SavedAutoUpdates := InitialAutoUpdates;
    SavedReduceMotion := InitialReduceMotion;
  end;
end;

function InitializeSetup: Boolean;
var HasPersistedPreferences: Boolean;
begin
  UpdateInstall := (ExpandConstant('{param:UPDATE|0}') <> '0') or PreviousInstallFound;
  SetupStateInitialized := True;
  InitialStartup := True;
  InitialAutoUpdates := True;
  InitialReduceMotion := False;
  SavedStartup := InitialStartup;
  SavedAutoUpdates := InitialAutoUpdates;
  SavedReduceMotion := InitialReduceMotion;
  PreferencesAvailable := not UpdateInstall;
  if UpdateInstall then begin
    HasPersistedPreferences := LoadExportedPreferences;
    if LoadPreferenceArguments then begin
      if not HasPersistedPreferences then begin
        SavedStartup := InitialStartup;
        SavedAutoUpdates := InitialAutoUpdates;
        SavedReduceMotion := InitialReduceMotion;
      end;
      PreferencesAvailable := True;
    end else begin
      PreferencesAvailable := HasPersistedPreferences;
      if HasPersistedPreferences then begin
        InitialStartup := SavedStartup;
        InitialAutoUpdates := SavedAutoUpdates;
        InitialReduceMotion := SavedReduceMotion;
      end;
    end;
  end;
  Result := True;
end;

procedure AddPreferenceOption(const Caption: String; const Top: Integer; var Option: TNewCheckBox; const Checked: Boolean);
begin
  Option := TNewCheckBox.Create(WizardForm);
  Option.Parent := PreferencesPage.Surface;
  Option.Left := 0;
  Option.Top := ScaleY(Top);
  Option.Width := PreferencesPage.SurfaceWidth;
  Option.Height := ScaleY(32);
  Option.Caption := Caption;
  Option.Checked := Checked;
  Option.Enabled := PreferencesAvailable;
end;

procedure InitializeWizard;
var Note: TNewStaticText; PageTitle, PageDescription, NoteText: String;
begin
  if ActiveLanguage = 'french' then begin
    PageTitle := 'Préférences de DeckPilot';
    PageDescription := 'Choisissez les réglages de démarrage, des mises à jour et des animations.';
    NoteText := 'Ces préférences seront conservées lors des prochaines mises à jour. Le pilote VB-CABLE sera vérifié et installé automatiquement s’il manque.';
    if IsUpdate and not PreferencesAvailable then NoteText := 'DeckPilot n’a pas pu lire les réglages actuels. Ils seront conservés sans modification.';
  end else begin
    PageTitle := 'DeckPilot preferences';
    PageDescription := 'Choose startup, update and animation settings.';
    NoteText := 'These preferences will be preserved on future updates. The VB-CABLE driver will be checked and installed automatically if missing.';
    if IsUpdate and not PreferencesAvailable then NoteText := 'DeckPilot could not read the current preferences. They will be left unchanged.';
  end;
  PreferencesPage := CreateCustomPage(wpWelcome, PageTitle, PageDescription);

  Note := TNewStaticText.Create(WizardForm);
  Note.Parent := PreferencesPage.Surface;
  Note.Left := 0;
  Note.Top := 0;
  Note.Width := PreferencesPage.SurfaceWidth;
  Note.Height := ScaleY(46);
  Note.AutoSize := False;
  Note.WordWrap := True;
  Note.Caption := NoteText;

  if ActiveLanguage = 'french' then begin
    AddPreferenceOption('Lancer DeckPilot avec Windows', 56, StartupOption, InitialStartup);
    AddPreferenceOption('Rechercher automatiquement les mises à jour', 94, AutoUpdatesOption, InitialAutoUpdates);
    AddPreferenceOption('Réduire les animations', 132, ReduceMotionOption, InitialReduceMotion);
  end else begin
    AddPreferenceOption('Start DeckPilot with Windows', 56, StartupOption, InitialStartup);
    AddPreferenceOption('Check for updates automatically', 94, AutoUpdatesOption, InitialAutoUpdates);
    AddPreferenceOption('Reduce animations', 132, ReduceMotionOption, InitialReduceMotion);
  end;
end;

function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := IsUpdate and ((PageID = wpLicense) or (PageID = wpSelectDir) or
    (PageID = wpSelectComponents) or (PageID = wpSelectProgramGroup) or
    (PageID = wpSelectTasks) or (PageID = wpReady));
end;

function ConfigureStartup: Boolean;
begin
  if not PreferencesAvailable then Result := False
  else if not IsUpdate then Result := True
  else if WizardSilent then Result := InitialStartup <> SavedStartup
  else Result := StartupOption.Checked <> SavedStartup;
end;

function ConfigurePreferences: Boolean;
begin
  if not PreferencesAvailable then Result := False
  else if not IsUpdate then Result := True
  else if WizardSilent then Result := (InitialAutoUpdates <> SavedAutoUpdates) or (InitialReduceMotion <> SavedReduceMotion)
  else Result := (AutoUpdatesOption.Checked <> SavedAutoUpdates) or (ReduceMotionOption.Checked <> SavedReduceMotion);
end;

function StartupArg(Param: String): String;
begin
  if WizardSilent then begin
    if InitialStartup then Result := 'on' else Result := 'off';
  end else if StartupOption.Checked then Result := 'on' else Result := 'off';
end;

function AutoUpdatesArg(Param: String): String;
begin
  if WizardSilent then begin
    if InitialAutoUpdates then Result := 'on' else Result := 'off';
  end else if AutoUpdatesOption.Checked then Result := 'on' else Result := 'off';
end;

function ReduceMotionArg(Param: String): String;
begin
  if WizardSilent then begin
    if InitialReduceMotion then Result := 'on' else Result := 'off';
  end else if ReduceMotionOption.Checked then Result := 'on' else Result := 'off';
end;

function DriverInstallArgument(Param: String): String;
begin
  if ExpandConstant('{param:TESTDRIVER|0}') = '1' then Result := '-SkipSetupForTest' else Result := '';
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
