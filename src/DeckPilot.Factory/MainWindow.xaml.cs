using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using DeckPilot.Core;
using Microsoft.Win32;

namespace DeckPilot.Factory;

public partial class MainWindow : Window
{
    private const long LongPressMs = 700;
    private readonly ObservableCollection<FactoryDevice> devices = [];
    private readonly Button[] keys = new Button[12];
    private readonly bool[] shortPassed = new bool[12], longPassed = new bool[12];
    private readonly bool[] keyboardDown = new bool[12], keyboardUp = new bool[12];
    private readonly int[] pinForPhysical = Enumerable.Repeat(-1, 12).ToArray();
    private readonly List<string> events = [];
    private readonly DispatcherTimer scanTimer = new() { Interval = TimeSpan.FromSeconds(10) };
    private readonly FactoryUpdateService updateService = new();
    private readonly CancellationTokenSource lifetime = new();
    private bool scanning, scanRequested, devicesScanned, refreshingDeviceList, flashBusy, testBusy, checkingKeys, diagnosing, held, fail, updateChecking;
    private int physicalIndex, stage, heldPin = -1, previousMask;
    private long pressedAt;
    private CancellationTokenSource? testCancellation;
    private string lastDevice = "";
    private readonly HashSet<string> recoveryLocations = new(StringComparer.OrdinalIgnoreCase);
    private FirmwareManifest? bundledManifest;
    private FactoryDevice? testedDevice;
    private string testedFirmwareVersion = "inconnue";
    private int testGeneration;

    public MainWindow()
    {
        InitializeComponent();
        DeckPilot.Windows.UsbDeviceNotifications.Observe(this, () => _ = RefreshDevicesAsync());
        PreviewKeyDown += (_, e) => RecordKeyboardReport(e, pressed: true);
        PreviewKeyUp += (_, e) => RecordKeyboardReport(e, pressed: false);
        AppVersionLabel.Text = "Atelier " + FactoryUpdateService.CurrentVersion.ToString(3);
        DeviceCombo.ItemsSource = devices;
        BuildKeyGrid();
        try { bundledManifest = FactoryHardware.BundleInfo(); FirmwareVersionLabel.Text = "Firmware inclus : " + (bundledManifest?.Version ?? "absent"); }
        catch (Exception ex) { FirmwareVersionLabel.Text = "Firmware invalide"; FlashStatus.Text = ex.Message; FlashButton.IsEnabled = false; }
        if (bundledManifest == null) { FlashStatus.Text = "Firmware non inclus ou invalide. Réinstalle cette application depuis un installateur officiel."; FlashButton.IsEnabled = false; }
        scanTimer.Tick += async (_, _) => await RefreshDevicesAsync(); scanTimer.Start();
        Loaded += async (_, _) => await RefreshDevicesAsync();
        Loaded += async (_, _) => await CheckUpdatesAsync(manual: false);
        _ = Task.Run(FactoryUpdateService.CleanOldDownloads);
        Closing += (_, e) => { if (flashBusy) { e.Cancel = true; MessageBox.Show(this, "Attends la fin de l’écriture et de la vérification avant de fermer Atelier.", "Opération USB en cours"); return; } IsClosing = true; lifetime.Cancel(); scanTimer.Stop(); testCancellation?.Cancel(); updateService.Dispose(); lifetime.Dispose(); };
    }

    private void BuildKeyGrid()
    {
        for (int i = 0; i < 12; i++)
        {
            var panel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            panel.Children.Add(new TextBlock { Text = (i + 1).ToString("00"), FontSize = 21, FontWeight = FontWeights.SemiBold, HorizontalAlignment = HorizontalAlignment.Center });
            panel.Children.Add(new TextBlock { Text = "Court  ·  Long", FontSize = 11, Foreground = (Brush)FindResource("MutedBrush"), HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 5, 0, 0) });
            var button = new Button { Content = panel, Margin = new Thickness(5), MinHeight = 112, Background = (Brush)FindResource("Panel2Brush"), BorderBrush = (Brush)FindResource("LineBrush"), Tag = i, IsEnabled = false };
            keys[i] = button; KeyGrid.Children.Add(button);
        }
    }

    private async Task RefreshDevicesAsync()
    {
        if (IsClosing || flashBusy || testBusy) return;
        if (scanning) { scanRequested = true; return; }
        scanning = true;
        try
        {
            var found = await Task.Run(FactoryHardware.Discover);
            if (IsClosing || flashBusy || testBusy || devicesScanned && devices.SequenceEqual(found)) return;
            devicesScanned = true;
            var previous = DeviceCombo.SelectedItem as FactoryDevice;
            refreshingDeviceList = true;
            try
            {
                devices.Clear();
                foreach (var device in found) devices.Add(device);
                var recovery = devices.Where(IsRecoveryCandidate).Take(2).ToArray();
                DeviceCombo.SelectedItem = devices.FirstOrDefault(d => d.Id == previous?.Id)
                    ?? devices.FirstOrDefault(d => !string.IsNullOrWhiteSpace(previous?.Location) && d.Location == previous.Location)
                    ?? (recovery.Length == 1 ? recovery[0] : null)
                    ?? (recoveryLocations.Count == 0 ? devices.FirstOrDefault(d => d.Bootloader) ?? (devices.Count == 1 ? devices[0] : null) : null);
            }
            finally { refreshingDeviceList = false; }
            if (devices.Count == 0) { ConnectionDot.Fill = new SolidColorBrush(Color.FromRgb(139, 150, 145)); ConnectionLabel.Text = "Carte non détectée"; }
            else if (devices.Count == 1) { ConnectionDot.Fill = (Brush)FindResource("AccentBrush"); ConnectionLabel.Text = devices[0].Bootloader ? "Bootloader détecté" : "Carte USB détectée"; }
            else { ConnectionDot.Fill = Brushes.Orange; ConnectionLabel.Text = $"{devices.Count} cartes détectées"; }
            UpdateDeviceActionButtons();
        }
        catch (Exception ex) { FlashStatus.Text = "Détection USB indisponible : " + ex.Message; }
        finally
        {
            scanning = false;
            if (scanRequested)
            {
                scanRequested = false;
                _ = RefreshDevicesAsync();
            }
        }
    }

    private void DeviceCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (refreshingDeviceList || FlashButton == null || EraseBoardButton == null) return;
        var device = DeviceCombo.SelectedItem as FactoryDevice;
        UpdateDeviceActionButtons();
        if (device != null) FlashStatus.Text = device.Bootloader ? "Bootloader Caterina prêt. Le firmware peut être installé." : "Carte Leonardo détectée. L’application redémarrera la carte en mode bootloader avant le flash.";
    }

    private async void CheckUpdates_Click(object sender, RoutedEventArgs e) => await CheckUpdatesAsync(manual: true);

    private async Task CheckUpdatesAsync(bool manual)
    {
        if (updateChecking || IsClosing) return;
        if (flashBusy || testBusy || diagnosing)
        {
            if (manual) MessageBox.Show(this, "Termine le flash ou le test en cours avant de mettre à jour Atelier.", "Opération USB en cours", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        updateChecking = true; UpdateButton.IsEnabled = false; UpdateButton.Content = "Recherche…";
        try
        {
            var manifest = bundledManifest ?? throw new IOException("Firmware inclus absent.");
            var release = await updateService.CheckAsync(Version.Parse(manifest.Version), lifetime.Token);
            if (IsClosing || flashBusy || testBusy || diagnosing) return;
            if (release == null)
            {
                UpdateButton.ToolTip = "DeckPilot Atelier et le firmware inclus sont à jour.";
                if (manual) MessageBox.Show(this, "DeckPilot Atelier et son firmware inclus sont à jour.", "Mise à jour", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var prompt = new UpdatePromptWindow(updateService, release, FactoryUpdateService.CurrentVersion, Version.Parse(manifest.Version)) { Owner = this };
            prompt.ShowDialog();
        }
        catch (Exception ex)
        {
            UpdateButton.ToolTip = "Impossible de vérifier les mises à jour. Vérifie ta connexion puis réessaie.";
            if (manual) MessageBox.Show(this, "La vérification des mises à jour a échoué.\n\n" + ex.Message, "Mise à jour indisponible", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        finally { updateChecking = false; if (!IsClosing) { UpdateButton.IsEnabled = true; UpdateButton.Content = "Vérifier les mises à jour"; } }
    }

    private async void DiagnosticsButton_Click(object sender, RoutedEventArgs e)
    {
        if (flashBusy || testBusy || diagnosing) { DiagnosticsResult.Text = "Termine l’opération USB en cours avant le diagnostic."; return; }
        if (DeviceCombo.SelectedItem is not FactoryDevice device) { DiagnosticsResult.Text = "Diagnostic impossible : sélectionne d’abord une carte."; return; }
        diagnosing = true;
        DiagnosticsButton.IsEnabled = false;
        UpdateDeviceActionButtons();
        DiagnosticsResult.Text = "Vérification de la carte et du firmware…";
        try { DiagnosticsResult.Text = await DiagnoseAsync(device); }
        catch (Exception ex) { DiagnosticsResult.Text = "Diagnostic incomplet : " + ex.Message; }
        finally { diagnosing = false; DiagnosticsButton.IsEnabled = true; UpdateDeviceActionButtons(); }
    }

    private Task<string> DiagnoseAsync(FactoryDevice device) => Task.Run(() => Diagnose(device));

    private static string Diagnose(FactoryDevice device)
    {
        if (string.IsNullOrWhiteSpace(device.Location)) throw new IOException("Emplacement USB physique indisponible. Le flash est bloqué par sécurité.");
        if (!FactoryHardware.Discover().Any(d => d.Id == device.Id && d.Port == device.Port && d.Location == device.Location))
            throw new IOException("La carte a changé de port ou a été débranchée. Relance la détection.");
        using var package = FactoryHardware.Prepare();
        string bundled = package.Manifest.Version;
        string result = $"Diagnostic OK · {device.Name} ({device.Port}) · carte {package.Manifest.Board} · firmware inclus {bundled} · image HEX vérifiée ({new FileInfo(Path.Combine(package.Folder, "DeckPilot.hex")).Length:N0} octets). ";
        if (device.Bootloader) return result + "Bootloader Caterina prêt ; version actuellement installée non lisible en mode bootloader.";
        using var transport = new FactoryTransport(device.Port);
        var identity = new BoardProtocolClient(transport).IdentifyOptional(CancellationToken.None);
        if (identity == null) return result + "Aucun firmware DeckPilot identifié. Le bootloader Caterina et la signature ATmega32U4 seront vérifiés avant l’écriture.";
        var installed = Version.Parse(identity.Version); var available = Version.Parse(bundled);
        if (installed > available) return result + $"ATTENTION : firmware installé {installed} plus récent que l’image incluse. Flash bloqué.";
        if (installed == available) return result + $"Comparaison : firmware installé {installed}, identique à l’image incluse.";
        return result + $"Comparaison : mise à niveau {installed} → {available} disponible.";
    }

    private async void FlashButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeviceCombo.SelectedItem is not FactoryDevice device || flashBusy || testBusy || diagnosing) return;
        await FlashOneAsync(device);
    }

    private async void RecoverButton_Click(object sender, RoutedEventArgs e)
    {
        if (recoveryLocations.Count == 0 || flashBusy || testBusy || diagnosing) return;
        var selected = DeviceCombo.SelectedItem as FactoryDevice;
        var candidates = devices.Where(IsRecoveryCandidate).ToArray();
        var device = selected != null && IsRecoveryCandidate(selected) ? selected : candidates.Length == 1 ? candidates[0] : null;
        if (device == null)
        {
            RecoveryInstructions.Text = "Aucun port de récupération n’est visible pour l’instant. Laisse le macropad branché, relie brièvement ses contacts RST et GND deux fois rapidement, puis attends quelques secondes : Atelier recherche le port bootloader automatiquement. Quand il apparaît dans la liste, sélectionne-le et clique à nouveau sur Récupérer.";
            RecoveryInstructions.Visibility = Visibility.Visible;
            FlashStatus.Text = "En attente du bootloader Caterina. La carte ne peut pas être reflashed tant que son port n’apparaît pas.";
            return;
        }
        var answer = MessageBox.Show(this, $"Réinstaller et vérifier le firmware DeckPilot sur {device.Name} ({device.Port}) ?\n\nLaisse la carte branchée au même emplacement USB pendant toute la récupération.", "Récupération du firmware", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
        if (answer == MessageBoxResult.Yes) await FlashOneAsync(device);
    }

    private bool IsRecoveryCandidate(FactoryDevice device) => recoveryLocations.Contains(device.Location);

    private async Task FlashOneAsync(FactoryDevice device)
    {
        if (string.IsNullOrWhiteSpace(device.Location)) { FlashStatus.Text = "Windows ne permet pas de vérifier l’emplacement USB physique. Aucun flash ne sera lancé."; return; }
        flashBusy = true; FlashButton.IsEnabled = false; RecoverButton.IsEnabled = false; DiagnosticsButton.IsEnabled = false; BatchFlashButton.IsEnabled = false; EraseBoardButton.IsEnabled = false; FlashProgress.Visibility = Visibility.Visible; FlashProgress.IsIndeterminate = true;
        var progress = new Progress<string>(s => FlashStatus.Text = s); bool writeAttempted = false;
        try
        {
            DiagnosticsResult.Text = await DiagnoseAsync(device);
            if (DiagnosticsResult.Text.Contains("Flash bloqué", StringComparison.Ordinal)) { FlashStatus.Text = "Flash refusé par le diagnostic : la carte contient un firmware plus récent."; return; }
            writeAttempted = true;
            recoveryLocations.Add(device.Location);
            await Task.Run(() => FactoryHardware.FlashAsync(device, progress, CancellationToken.None));
            recoveryLocations.Remove(device.Location);
            RecoverButton.Content = "Récupérer / relancer le flash…";
            if (recoveryLocations.Count == 0) RecoveryInstructions.Visibility = Visibility.Collapsed;
            FlashStatus.Text = "Tout est prêt. Passe à l’étape 2 pour contrôler les 12 touches.";
            ShowTestPage();
        }
        catch (Exception ex)
        {
            if (writeAttempted)
            {
                RecoverButton.Content = "Réinstaller le firmware après échec…";
                RecoveryInstructions.Visibility = Visibility.Visible;
                FlashStatus.Text = ex.Message + " La récupération reste disponible.";
            }
            else FlashStatus.Text = ex.Message;
        }
        finally { flashBusy = false; FlashProgress.IsIndeterminate = false; FlashProgress.Visibility = Visibility.Collapsed; UpdateDeviceActionButtons(); DiagnosticsButton.IsEnabled = true; BatchFlashButton.IsEnabled = true; await RefreshDevicesAsync(); }
    }

    private async void BatchFlashButton_Click(object sender, RoutedEventArgs e)
    {
        if (flashBusy || testBusy || diagnosing) return;
        var batch = devices.Where(d => !string.IsNullOrWhiteSpace(d.Location)).GroupBy(d => d.Location, StringComparer.OrdinalIgnoreCase).Select(g => g.First()).OrderBy(d => d.Port, StringComparer.OrdinalIgnoreCase).ToArray();
        if (batch.Length == 0) { FlashStatus.Text = "Aucune carte avec un emplacement USB vérifiable. Branche une ou plusieurs cartes puis réessaie."; return; }
        string list = string.Join(Environment.NewLine, batch.Select(d => $"• {d.Name} · {d.Port}"));
        if (MessageBox.Show(this, $"Atelier en série : {batch.Length} carte(s) détectée(s). Chaque carte sera diagnostiquée puis flashée séparément.\n\n{list}\n\nNe débranche aucune carte avant la fin. Continuer ?", "Mode atelier en série", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) != MessageBoxResult.Yes) return;
        flashBusy = true; UpdateDeviceActionButtons(); BatchFlashButton.IsEnabled = false; DiagnosticsButton.IsEnabled = false; EraseBoardButton.IsEnabled = false; FlashProgress.Visibility = Visibility.Visible; FlashProgress.IsIndeterminate = false; FlashProgress.Maximum = batch.Length;
        var outcomes = new List<string>(); int index = 0;
        try
        {
            foreach (var device in batch)
            {
                index++; FlashProgress.Value = index - 1;
                FlashStatus.Text = $"Carte {index}/{batch.Length} · diagnostic {device.Port}…";
                try
                {
                    string diagnosis = await DiagnoseAsync(device); DiagnosticsResult.Text = diagnosis;
                    if (diagnosis.Contains("Flash bloqué", StringComparison.Ordinal)) throw new IOException("Firmware installé plus récent ; carte ignorée.");
                    var progress = new Progress<string>(message => FlashStatus.Text = $"Carte {index}/{batch.Length} · {message}");
                    recoveryLocations.Add(device.Location);
                    await Task.Run(() => FactoryHardware.FlashAsync(device, progress, CancellationToken.None));
                    recoveryLocations.Remove(device.Location);
                    outcomes.Add($"OK · {device.Name} · {device.Port} · firmware {bundledManifest?.Version}");
                }
                catch (Exception ex) { outcomes.Add($"ÉCHEC · {device.Name} · {device.Port} · {ex.Message}"); }
                FlashProgress.Value = index;
            }
            var report = new StringBuilder(); report.AppendLine("DeckPilot Atelier — Rapport de série"); report.AppendLine($"Date : {DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz}"); report.AppendLine($"Firmware inclus : {bundledManifest?.Version ?? "inconnu"}"); report.AppendLine(); foreach (var outcome in outcomes) report.AppendLine(outcome);
            var dialog = new SaveFileDialog { Title = "Enregistrer le rapport de série", Filter = "Rapport texte (*.txt)|*.txt", FileName = $"DeckPilot-serie-{DateTime.Now:yyyyMMdd-HHmm}.txt", AddExtension = true, DefaultExt = ".txt" };
            if (dialog.ShowDialog() == true)
            {
                try { File.WriteAllText(dialog.FileName, report.ToString(), new UTF8Encoding(true)); }
                catch (Exception ex) { FlashStatus.Text = "La série est terminée, mais le rapport n’a pas pu être enregistré : " + ex.Message; }
            }
            int success = outcomes.Count(x => x.StartsWith("OK ·", StringComparison.Ordinal));
            FlashStatus.Text = $"Série terminée : {success}/{batch.Length} firmware(s) vérifié(s). Relance le contrôle des touches pour la disposition et les appuis.";
        }
        finally { flashBusy = false; BatchFlashButton.IsEnabled = true; FlashProgress.Visibility = Visibility.Collapsed; await RefreshDevicesAsync(); UpdateDeviceActionButtons(); DiagnosticsButton.IsEnabled = true; }
    }

    private async void EraseBoardButton_Click(object sender, RoutedEventArgs e)
    {
        if (DeviceCombo.SelectedItem is not FactoryDevice device || flashBusy || testBusy || diagnosing) return;
        if (string.IsNullOrWhiteSpace(device.Location)) { FlashStatus.Text = "Windows ne permet pas de confirmer l’emplacement USB physique. Aucune écriture ne sera lancée."; return; }
        var confirmation = new ResetConfirmationWindow { Owner = this };
        if (confirmation.ShowDialog() != true) return;

        flashBusy = true; recoveryLocations.Add(device.Location); RecoverButton.Content = "Réinstaller le firmware après effacement…"; UpdateDeviceActionButtons(); FlashProgress.Visibility = Visibility.Visible; FlashProgress.IsIndeterminate = true;
        var progress = new Progress<string>(s => FlashStatus.Text = s);
        try
        {
            await Task.Run(() => FactoryHardware.ResetAsync(device, progress, CancellationToken.None));
            RecoveryInstructions.Text = "Le programme a été effacé comme demandé. La carte disparaît alors normalement des périphériques USB. Pour la récupérer, laisse-la branchée, relie brièvement RST à GND deux fois rapidement, attends que le port bootloader apparaisse dans Atelier, puis clique sur « Réinstaller le firmware après effacement ».";
            RecoveryInstructions.Visibility = Visibility.Visible;
            FlashStatus.Text = "Effacement terminé. Le bootloader Caterina est conservé ; utilise la procédure affichée pour réinstaller le firmware.";
        }
        catch (Exception ex)
        {
            RecoveryInstructions.Visibility = Visibility.Visible;
            FlashStatus.Text = "L’effacement ou sa vérification a échoué : " + ex.Message + " La récupération reste disponible.";
        }
        finally
        {
            flashBusy = false; FlashProgress.IsIndeterminate = false; FlashProgress.Visibility = Visibility.Collapsed;
            await RefreshDevicesAsync(); UpdateDeviceActionButtons();
        }
    }

    private void UpdateDeviceActionButtons()
    {
        var selected = DeviceCombo.SelectedItem as FactoryDevice;
        bool available = bundledManifest != null && !flashBusy && !testBusy && !diagnosing;
        var interval = TimeSpan.FromSeconds(recoveryLocations.Count > 0 ? 2 : 10);
        if (scanTimer.Interval != interval) scanTimer.Interval = interval;
        if (FlashButton != null) FlashButton.IsEnabled = selected != null && available;
        if (EraseBoardButton != null) EraseBoardButton.IsEnabled = selected != null && available;
        if (RecoverButton != null) RecoverButton.IsEnabled = recoveryLocations.Count > 0 && available;
    }

    private void FlashTab_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowFlashPage();
    private void TestTab_Click(object sender, System.Windows.Input.MouseButtonEventArgs e) => ShowTestPage();
    private void ShowFlashPage()
    {
        StopTest(); FlashPage.Visibility = Visibility.Visible; TestPage.Visibility = Visibility.Collapsed;
        FlashTab.Background = (Brush)FindResource("Panel2Brush"); TestTab.Background = Brushes.Transparent;
        ((TextBlock)((Grid)TestTab.Child).Children[0]).Foreground = (Brush)FindResource("MutedBrush");
        ((TextBlock)((Grid)FlashTab.Child).Children[0]).Foreground = Brushes.White;
    }
    private void ShowTestPage()
    {
        StopTest(); FlashPage.Visibility = Visibility.Collapsed; TestPage.Visibility = Visibility.Visible;
        TestTab.Background = (Brush)FindResource("Panel2Brush"); FlashTab.Background = Brushes.Transparent;
        ((TextBlock)((Grid)FlashTab.Child).Children[0]).Foreground = (Brush)FindResource("MutedBrush");
        ((TextBlock)((Grid)TestTab.Child).Children[0]).Foreground = Brushes.White;
    }

    private async void ConnectTest_Click(object sender, RoutedEventArgs e)
    {
        if (testBusy) { StopTest(); ConnectTestButton.Content = "Démarrer le test USB"; TestStatus.Text = "Test interrompu."; return; }
        await StartTestAsync();
    }

    private async Task StartTestAsync()
    {
        if (flashBusy || diagnosing) return;
        var candidates = devices.Where(d => !d.Bootloader).ToArray();
        var device = (DeviceCombo.SelectedItem as FactoryDevice is { Bootloader: false } selected ? selected : null)
            ?? candidates.FirstOrDefault(d => d.Id == lastDevice) ?? (candidates.Length == 1 ? candidates[0] : null);
        if (device == null) { TestStatus.Text = candidates.Length == 0 ? "Carte avec firmware DeckPilot introuvable. Installe d’abord le firmware à l’étape 1." : "Plusieurs cartes détectées. Débranche les autres cartes pour choisir sans ambiguïté."; return; }
        ResetTestState(); testedDevice = device;
        int generation = ++testGeneration;
        testBusy = true; lastDevice = device.Id; ConnectTestButton.Content = "Arrêter le test"; TestStatus.Text = "Connexion au firmware…";
        UpdateDeviceActionButtons();
        testCancellation = new CancellationTokenSource(); var token = testCancellation.Token;
        try
        {
            using var transport = new FactoryTransport(device.Port);
            var client = new BoardProtocolClient(transport);
            BoardInfo? info = null;
            for (int attempt = 0; attempt < 8 && info == null; attempt++)
            {
                token.ThrowIfCancellationRequested();
                try { info = await Task.Run(() => client.Connect(token), token); }
                catch (IOException) when (attempt < 7) { await Task.Delay(400, token); }
            }
            if (info == null) throw new IOException("Le macropad ne répond pas. Vérifie qu’il n’est pas ouvert dans DeckPilot ou un autre outil série.");
            testedFirmwareVersion = info.Firmware.Version;
            if (bundledManifest != null && Version.Parse(info.Firmware.Version) < Version.Parse(bundledManifest.Version))
            {
                FlashStatus.Text = $"Le firmware de la carte ({info.Firmware.Version}) est ancien. La version {bundledManifest.Version} est incluse dans Atelier et prête à flasher.";
                ShowFlashPage(); return;
            }
            if (info.Protocol != 2 || (info.Capabilities & 2) == 0) throw new IOException("Cette version du firmware ne transmet pas les événements des touches. Installe le firmware inclus dans l’outil Atelier.");
            TestStatus.Text = $"Firmware {info.Firmware.Version} connecté. Appuie brièvement sur la touche physique 01.";
            TestInstructions.Text = "Garde Atelier au premier plan. Fais un appui court puis un appui long sur chaque touche.";
            TestInstructions.TextWrapping = TextWrapping.Wrap;
            ConnectTestButton.Focus();
            checkingKeys = true;
            foreach (var key in keys) key.IsEnabled = true;
            await Task.Run(() => client.Run(telemetry =>
            {
                long receivedAt = Environment.TickCount64;
                Dispatcher.BeginInvoke(() => { if (generation == testGeneration) ProcessMask(telemetry.Mask, receivedAt); });
            }, token), token);
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Dispatcher.Invoke(() => { TestStatus.Text = "Connexion échouée : " + ex.Message; Activity.Text = "Aucune mesure en cours."; }); }
        finally
        {
            testBusy = checkingKeys = false;
            testCancellation?.Dispose(); testCancellation = null;
            ConnectTestButton.Content = "Démarrer le test USB";
            foreach (var key in keys) key.IsEnabled = false;
            UpdateDeviceActionButtons();
        }
    }

    private void StopTest()
    {
        testGeneration++;
        checkingKeys = false;
        testCancellation?.Cancel(); testCancellation?.Dispose(); testCancellation = null;
        foreach (var key in keys) key.IsEnabled = false;
    }

    private bool IsClosing { get; set; }
    public void RequestInstallerExit()
    {
        if (flashBusy || testBusy)
        {
            FlashStatus.Text = "Une opération USB est en cours. Termine le flash ou le test avant la mise à jour.";
            TestStatus.Text = "Une mise à jour attend la fin du test en cours.";
            return;
        }
        IsClosing = true; StopTest(); Close();
    }



    private void RecordKeyboardReport(KeyEventArgs e, bool pressed)
    {
        if (!checkingKeys || e.IsRepeat) return;
        int key = KeyInterop.VirtualKeyFromKey(e.Key);
        if (!Calibration.IsDeckKey(key)) return;
        int index = key - Calibration.FirstKey;
        if (pressed) keyboardDown[index] = true;
        else if (keyboardDown[index]) keyboardUp[index] = true;
        UpdateControlResult();
    }

    private void UpdateControlResult()
    {
        if (physicalIndex < 12) return;
        int received = keyboardUp.Count(value => value);
        ExportButton.IsEnabled = received == 12;
        if (received < 12)
        {
            ResultTitle.Text = "Réception clavier à vérifier";
            TestStatus.Text = $"Windows a reçu {received}/12 touches. Garde Atelier au premier plan et réessaie : " +
                string.Join(", ", Enumerable.Range(0, 12).Where(index => !keyboardUp[index]).Select(index => $"F{13 + index}"));
            return;
        }
        Activity.Text = "Les 12 touches répondent en appui court et long. Windows a reçu leurs appuis et relâchements.";
        ResultTitle.Text = "Contrôle réussi";
        ResultTitle.Foreground = (Brush)FindResource("AccentBrush");
        TestStatus.Text = "Tu peux enregistrer le rapport.";
    }

    private void ProcessMask(int mask, long receivedAt)
    {
        if (!testBusy) return;
        if (fail) { if (mask == 0) { fail = false; previousMask = 0; } return; }
        if (mask == previousMask) return;
        if (held)
        {
            if (mask == 0) { long duration = receivedAt - pressedAt; held = false; previousMask = 0; CompletePress(duration); return; }
            SetFailure("Plusieurs touches détectées en même temps. Relâche tout et vérifie le câblage."); return;
        }
        previousMask = mask;
        if (mask == 0) return;
        if (physicalIndex >= 12) return;
        if (System.Numerics.BitOperations.PopCount((uint)mask) != 1) { SetFailure("Plusieurs entrées se sont activées ensemble. Relâche les touches puis vérifie le câblage."); return; }
        int pin = System.Numerics.BitOperations.TrailingZeroCount((uint)mask);
        int mappedPhysical = Array.IndexOf(pinForPhysical, pin);
        if (mappedPhysical >= 0 && mappedPhysical != physicalIndex) { SetFailure($"Le signal F{13 + pin} a déjà été affecté à la touche physique {mappedPhysical + 1:00}. Vérifie qu’il n’y a pas de pont entre deux boutons."); return; }
        if (stage == 1 && pinForPhysical[physicalIndex] != pin) { SetFailure($"La touche physique {physicalIndex + 1:00} a envoyé F{13 + pin} au lieu de F{13 + pinForPhysical[physicalIndex]}. Relâche et recommence."); return; }
        held = true; heldPin = pin; pressedAt = receivedAt;
        Activity.Text = $"Touche physique {physicalIndex + 1:00} détectée (F{13 + pin}). {(stage == 0 ? "Relâche avant 700 ms pour valider l’appui court." : "Garde la touche enfoncée au moins 700 ms pour valider l’appui long.")}";
    }

    private void CompletePress(long duration)
    {
        bool longPress = duration >= LongPressMs;
        if (stage == 0 && longPress) { SetFailure($"Appui trop long ({duration} ms). Pour le test court, relâche en moins de 700 ms."); return; }
        if (stage == 1 && !longPress) { SetFailure($"Appui trop court ({duration} ms). Maintiens la touche au moins 700 ms."); return; }
        if (stage == 0)
        {
            pinForPhysical[physicalIndex] = heldPin; shortPassed[physicalIndex] = true; stage = 1;
            events.Add($"{DateTimeOffset.Now:O} · Touche {physicalIndex + 1:00} · F{13 + heldPin} · court · {duration} ms · OK");
            UpdateGrid(); Activity.Text = $"Appui court réussi sur {physicalIndex + 1:00}. Maintenant, garde cette même touche enfoncée au moins 700 ms.";
        }
        else
        {
            longPassed[physicalIndex] = true;
            events.Add($"{DateTimeOffset.Now:O} · Touche {physicalIndex + 1:00} · F{13 + heldPin} · long · {duration} ms · OK");
            int done = physicalIndex; physicalIndex++; stage = 0; UpdateGrid();
            if (physicalIndex == 12)
            {
                UpdateControlResult();
            }
            else Activity.Text = $"Appui long réussi sur {done + 1:00}. Passe à la touche physique {physicalIndex + 1:00} et commence par un appui court.";
        }
    }

    private void SetFailure(string message)
    {
        fail = previousMask != 0; held = false; previousMask = fail ? -1 : 0; ResultTitle.Text = "À vérifier"; ResultTitle.Foreground = Brushes.Orange; Activity.Text = message;
        TestStatus.Text = "Erreur de mesure détectée. Relâche les touches ; la séquence reprendra au même bouton.";
    }

    private void UpdateGrid()
    {
        int shortCount = shortPassed.Count(v => v), longCount = longPassed.Count(v => v);
        ShortCount.Text = $"{shortCount} / 12"; LongCount.Text = $"{longCount} / 12";
        for (int i = 0; i < 12; i++)
        {
            var root = (StackPanel)keys[i].Content; var number = (TextBlock)root.Children[0]; var status = (TextBlock)root.Children[1];
            number.Text = pinForPhysical[i] < 0 ? $"{i + 1:00}" : $"{i + 1:00}   ·   F{13 + pinForPhysical[i]}";
            status.Text = $"{(shortPassed[i] ? "✓ Court" : "○ Court")}     {(longPassed[i] ? "✓ Long" : "○ Long")}";
            keys[i].BorderBrush = i == physicalIndex && physicalIndex < 12 ? (Brush)FindResource("AccentBrush") : shortPassed[i] && longPassed[i] ? (Brush)FindResource("AccentBrush") : (Brush)FindResource("LineBrush");
        }
        ExportButton.IsEnabled = shortCount == 12 && longCount == 12 && keyboardUp.All(value => value);
        if (physicalIndex < 12) ResultTitle.Text = $"Touche {physicalIndex + 1:00} sur 12 · {(stage == 0 ? "appui court" : "appui long")}";
    }

    private void ResetTest_Click(object sender, RoutedEventArgs e) { StopTest(); ResetTestState(); }

    private void ResetTestState()
    {
        testedDevice = null; testedFirmwareVersion = "inconnue";
        Array.Clear(keyboardDown); Array.Clear(keyboardUp);
        Array.Clear(shortPassed); Array.Clear(longPassed); Array.Fill(pinForPhysical, -1); events.Clear(); physicalIndex = stage = 0; fail = held = false; previousMask = 0;
        UpdateGrid(); ResultTitle.Text = "En attente"; ResultTitle.Foreground = (Brush)FindResource("MutedBrush"); Activity.Text = "En attente d’une touche…"; TestStatus.Text = "Appuie sur Démarrer le test USB pour commencer."; ExportButton.IsEnabled = false;
    }

    private void ExportButton_Click(object sender, RoutedEventArgs e)
    {
        if (!ExportButton.IsEnabled) return;
        var dialog = new SaveFileDialog { Title = "Enregistrer le rapport de contrôle", Filter = "Rapport texte (*.txt)|*.txt", FileName = $"DeckPilot-controle-{DateTime.Now:yyyyMMdd-HHmm}.txt", AddExtension = true, DefaultExt = ".txt" };
        if (dialog.ShowDialog() != true) return;
        var report = new StringBuilder();
        report.AppendLine("DeckPilot Atelier — Rapport de contrôle");
        report.AppendLine($"Date : {DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss zzz}");
        report.AppendLine($"Carte : {testedDevice?.Name ?? "non sélectionnée"} · {testedDevice?.Port ?? "port inconnu"}");
        report.AppendLine($"Identifiant USB : {testedDevice?.Id}");
        report.AppendLine($"Emplacement USB : {testedDevice?.Location}");
        report.AppendLine($"Firmware testé : {testedFirmwareVersion}");
        report.AppendLine($"Firmware inclus : {bundledManifest?.Version ?? "inconnu"}");
        report.AppendLine("Disposition physique 01→12 :");
        for (int i = 0; i < pinForPhysical.Length; i++)
            report.AppendLine($"  Touche {i + 1:00} → F{13 + pinForPhysical[i]} · court OK · long OK · reçu par Windows");
        report.AppendLine("Résultat : RÉUSSI");
        report.AppendLine("Appuis courts et longs vérifiés : 12 / 12 chacun");
        report.AppendLine("Appuis et relâchements clavier reçus par Windows : 12 / 12");
        report.AppendLine();
        report.AppendLine("Détail des mesures :");
        foreach (string item in events) report.AppendLine(item);
        try { File.WriteAllText(dialog.FileName, report.ToString(), new UTF8Encoding(true)); TestStatus.Text = "Rapport enregistré : " + dialog.FileName; } catch (Exception ex) { TestStatus.Text = "Impossible d’enregistrer le rapport : " + ex.Message; }
    }
}
