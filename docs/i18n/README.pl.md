> Jest to automatyczne tłumaczenie kompletnego README. Angielski oryginał jest punktem odniesienia. Etykiety interfejsu aplikacji i komendy są dokładnie zachowane.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="Baner DeckPilot" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="Logo DeckPilot" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Towarzysz Windows dla Twojego 12- Klucz do makra DIY 'ego.<br>
  Konfiguracja kluczy, programów startowych, nośników sterowania i profili przełączania.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">Pobierz najnowszą wersję</a>
   · 
  <a href="#getting-started">Zaczynając</a>
   · 
  <a href="#development">Rozwój</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&color=78b7a0" alt="Najnowsze wydanie"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&logo=windows" alt="Pobierz dla Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe"><img src="https://img.shields.io/badge/Download-DeckPilot_Atelier-16856A?style=flat-square&logo=windows" alt="Pobierz Atelier DeckPilot"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="Licencja MIT"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Stan budowy systemu Windows"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&color=70a7e8" alt="GitHub wydaj pliki do pobrania"></a>
</p>

<p align="center">
  <a href="../../README.md">🇬🇧 English</a> ·
  <a href="README.fr.md">🇫🇷 Français</a> ·
  <a href="README.de.md">🇩🇪 Deutsch</a> ·
  <a href="README.es.md">🇪🇸 Español</a> ·
  <a href="README.it.md">🇮🇹 Italiano</a> ·
  <a href="README.pt-BR.md">🇧🇷 Português brasileiro</a> ·
  <a href="README.nl.md">🇳🇱 Nederlands</a> ·
  <a href="README.pl.md">🇵🇱 Polski</a> ·
  <a href="README.ru.md">🇷🇺 Русский</a> ·
  <a href="README.ja.md">🇯🇵 日本語</a> ·
  <a href="README.ko.md">🇰🇷 한국어</a> ·
  <a href="README.zh-CN.md">🇨🇳 简体中文</a> ·
  <a href="README.ar.md">🇸🇦 العربية</a>
</p>

---

DeckPilot konfiguruje Arduino Leonardo- kompatybilne makropadki, które już wysyłają **F13- F24** jako wejścia klawiatury USB, w **3 wiersze 4 klawisze**. Fizyczna kolejność klawiszy może być arbitralna: nakierowana kalibracja mapuje każdy przycisk do jego pozycji.

**Funkcje klawiatury F13- F24 nie wymagają dodatkowych zmian sterownika ani oprogramowania firmowego. Soundboard mieszanie wymaga podpisanego VB-CABLE sterownik audio, zainstalowany przez Setup w przypadku braku.** Interfejs aplikacji jest obecnie w języku francuskim. Wybierz język powyżej dla pełnego przetłumaczonego README, w tym odniesienia technicznego i historii wersji. Tłumaczenia są generowane automatycznie; ten angielski oryginał jest referencją.

<a id="features"></a>
## Cechy

- **Kalibracja z przewodnikiem:** identyfikacja wszystkich 12 przyciski fizyczne bez ponownego okablowania makro-pad.
- **Instalowany wybielacz aplikacji:** wyszukaj skróty menu Start i katalog aplikacji Windows, w tym wpisy Microsoft Store. Ręczny wybór plików pozostaje dostępny.
- **Niestandardowe działania:** uruchamianie aplikacji, otwieranie folderów lub stron internetowych, wysyłanie skrótów i sterowanie mediami.
- **Wolne klawisze:** zachować F13- F24 dostępne dla skrótów skonfigurowanych bezpośrednio w grze lub innej aplikacji.
- **Profile lokalne:** tworzy, duplikuje, zmienia nazwę, usuwa i wybiera profile. Kalibracja jest dzielona między profile.
- **Profile wykonane z papieru:** zacząć od szablonów Music, Productivity lub Creation, a następnie dostosować je. Przełącz na konkretny, następny lub poprzedni profil z klucza fizycznego.
- **Szybsza edycja:** kopiowanie / wycinanie / wklejanie / resetowanie kompletnych kluczy, nagrywanie skrótów klawiszowych, wybieranie kolorów akcentów i cofanie zmian.
- **Narzędzia Windows:** otwarty dźwięk, wyświetlacz, ustawienia Bluetooth i sieci, Task Manager, lub zablokować sesję.
- **Kompaktowy interfejs ciemny:** ikony wektorowe, ikony aplikacji tam, gdzie są dostępne, i subtelne animacje, które respektują preferencje animacji Windows.
- **Operacja w tle:** zachować DeckPilot w obszarze powiadomienia, z uruchomieniem w systemie Windows sign- in domyślnie włączone (może być wyłączone).
- **Automatyczne aktualizacje:** zatwierdzenie nowego wydania, a następnie niech DeckPilot Pobieranie, weryfikowanie, zastępowanie i ponowne uruchamianie.

<a id="getting-started"></a>
## Zaczynając

<a id="requirements"></a>
### Wymagania

- Okna 10 lub Windows 11, **x64**.
- Arduino Leonardo- kompatybilny makropad wysyłanie 12 różne klawisze z **F13 do F24**.
- Połączenie danych USB.

Publikowane pliki do pobrania zawierają runtime .NET. **Nie trzeba instalować .NET do użycia DeckPilot.**

<a id="install-and-launch"></a>
### Zainstaluj i uruchom

1. Pobieranie **[DeckPilot-Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** i uruchom instalatora. Windows prosi o zgodę administratora, aby zainstalować do **Program Files\DeckPilot**.
2. Instalator pozwala wybrać, czy DeckPilot zaczyna się od systemu Windows, automatycznie sprawdza aktualizacje aplikacji i redukuje animacje. Domyślnie uruchamianie i aktualizowanie kontroli; zmniejszone animacje są wyłączone. VB-CABLE kierowca wymagany przez Soundboard i instaluje go automatycznie, jeśli brakuje. Do sprawdzenia kierowcy wymagane jest połączenie internetowe.
3. Instalator dodaje DeckPilot do Windows **Instalowane aplikacje** i tworzy skrót w menu Start. Skrót pulpitu jest domyślnie wybierany.
4. Połącz makropad i otwórz DeckPilot ze skrótu.
5. Postępuj zgodnie z kalibracją od lewej do prawej strony **4 kolumny**, następnie w następnym wierszu, dla **trzy wiersze**. Każdy przycisk musi wysłać inny klawisz F13- F24.

Podczas modernizacji od pierwszego 4- układ wiersza, DeckPilot zachowuje profile / działania i żąda jednej nowej kalibracji dla skorygowanych pozycji. Instalacja nad istniejącym przenośnym ustawieniem zachowuje również ustawienia w `%APPDATA%\DeckPilot`; rzucić starą kopię, jeśli konfiguracja poprosi.

Przenośny [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) oraz [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) pozostają dostępne dla kompatybilności. Do użytku przenośnego należy przechowywać wykonywalny w osobnym folderze do zapisu.

Odinstaluj z systemu Windows **Instalowane aplikacje**. Deinstalacja usuwa program i jego skróty, zachowując profile osobiste, aby później można je ponownie zainstalować.

Pojedynczy plik wykonywalny pobiera swoje powiązane zależności do pamięci podręcznej .NET użytkownika przy pierwszym uruchomieniu, co może potrwać trochę dłużej. Opublikowane pliki wykonywalne są obecnie niepodpisane, więc Windows może wyświetlić ostrzeżenie SmartScreen.

<a id="configure-a-key"></a>
### Konfiguracja klucza

1. Wybierz klucz w siatce.
2. Wybierz akcję na prawym pasku bocznym.
3. Konfiguracja ustawień poniżej siatki i zapis. Przełączanie klawiszy lub profili również zapisuje ustawienia bieżącego klucza.

| Działanie | Zachowanie |
| --- | --- |
| Wolny klawisz | Przesyłki F13- F24 przez normalnie. Przypisz go bezpośrednio w grze lub aplikacji. |
| Stosowanie | Otwiera zainstalowaną aplikację lub ręcznie wybrany program lub skrót. |
| Katalog | Otwiera folder w systemie Windows Explorer. |
| Strona internetowa | Otwiera adres HTTP lub HTTPS w domyślnej przeglądarce. |
| Skrót klawiszowy | Wysyła kombinację kluczy do aktywnego okna. |
| Media | Kontroluje odtwarzanie, utwory, głośność lub wyciszenie. |
| Spotify | Steruje odtwarzaniem Spotify i dostosowuje tylko głośność sesji Windows Spotify. |
| OBS / Strealaboratory | Rozmawia bezpośrednio z aplikacją WebSocket API dla scen, wyjście na żywo, nagrywanie, bufor powtórki, wyciszenie dźwięku i widoczność źródła. |
| Zastosowanie - DeckPilot | Pokazuje istniejący DeckPilot okno, w tym kiedy jest uruchomiony w obszarze powiadomienia. |

W przypadku akcji aplikacji otwórz program i wyszukaj go po nazwie. Ręczny selektor plików pozostaje dostępny, z opcjonalnymi argumentami programu w zaawansowanych opcjach. Wprowadź argumenty zgodnie z składnią aplikacji docelowej.

Trzymanie przycisku uruchamia skonfigurowaną akcję tylko raz do czasu wydania. W trybie free- key, normalne zdarzenia powtarzające klawiaturę pozostają dostępne.

<a id="manage-profiles"></a>
### Zarządzanie profilami

Otwórz sekcję zarządzania profilem na prawym pasku bocznym, aby utworzyć, skopiować, zmienić nazwę lub usunąć profil. Użyj selektora powyżej siatki klawiszy, aby wybrać aktywny profil.

Kalibracja ma zastosowanie do wszystkich profili i może być powtórzona z paska bocznego.

<a id="background-operation"></a>
### Operacja w tle

Zamknięcie schowka okna DeckPilot jednocześnie zachowując aktywne działania. Double- kliknij ikonę obszaru zawiadamiania w pobliżu zegara, aby ją ponownie otworzyć lub kliknij prawym przyciskiem myszy ikonę i wybierz polecenie zakończenia, aby całkowicie ją zatrzymać.

Uruchomienie systemu Windows jest domyślnie włączone dla bieżącego użytkownika. Możesz go wyłączyć za pomocą **Lancer avec Windows**; późniejsze aktualizacje aplikacji zachowują ten wybór. Instalacja i kolejne uruchamia odświeżanie włączonego wpisu do bieżącej ścieżki wykonywalnej.

<a id="installed-applications"></a>
## Instalowane aplikacje

Picker odczytuje skróty z menu bieżącego użytkownika i systemowego, a następnie uzupełnia je o `Get-StartApps` wyniki z katalogu aplikacji Windows.

Szukaj ignoruje przypadek i akcent. Duplikaty nazwy są zgrupowane, przy czym pozycje w menu Start mają pierwszeństwo. Przenośne aplikacje i programy bez zarejestrowanych skrótów mogą być nadal wybierane ręcznie. Jeśli katalog Windows jest niedostępny, odczytywane skróty w menu Start pozostają użyteczne.

Windows uruchamia wybrane skróty. `shell:AppsFolder`. Opcjonalne argumenty odnoszą się do programów opartych na plikach i skrótów; są one ukryte dla wpisów AppsFolder.

<a id="automatic-updates"></a>
## Automatyczne aktualizacje

Dostępne od **0.2.0**, automatyczne kontrole aktualizacji uruchomić przy starcie po kalibracji, następnie co sześć godzin. Mogą one być wyłączone w pasku bocznym. Kontrola manualna pozostaje dostępna.

Kiedy zostanie znaleziona nowsza stabilna wersja GitHub, DeckPilot Pyta, czy uaktualnić. **Zainstalowane kopie** pobierz i SHA-256 weryfikuj `DeckPilot-Setup.exe`, następnie uruchomić instalatora z aktualnym startup, automatic- update i reduced- motion preferencje presected. Możesz je zmienić podczas aktualizacji. Windows prosi o zgodę administratora na aktualizację plików programu. Instalator zamyka DeckPilot wdzięcznie, gdy jest gotowy, zachowuje profile i konfigurację, sprawdza i instaluje podpisane VB-CABLE sterownik, jeśli brakuje i ponownie uruchamia aplikację. Anulowanie instalatora pozostawia otwartą aplikację. Setup odmawia przerwania otwartej sesji firmware.

**Kopie przenośne** zachowuje istniejący mechanizm aktualizacji. Po potwierdzeniu:

1. Pobiera wykonywalny z obsługą raportowania postępu i anulowania.
2. Weryfikuje jego SHA-256 hash again `SHA256SUMS.txt` z tego samego wydania GitHub.
3. Oszczędza ustawienia i przygotowuje pomocnika aktualizacji.
4. Wychodzi, zastępuje wykonywalny i ponownie uruchamia.
5. Utrzymuje poprzedni wykonywalny dopóki nowy proces nie potwierdzi udanej inicjalizacji; jeśli startup zawiedzie, przywraca i ponownie uruchamia poprzednią wersję.

Wybór do aktualizacji późniejsze pocztówki automatyczne prompts dla 24 godziny. Ręczne kontrole omijają to odroczenie.

Pliki do pobrania używają HTTPS, z przekierowaniem ograniczonym do oczekiwanych hostów GitHub. Pliki tymczasowe są przechowywane w `%LOCALAPPDATA%\DeckPilot\Updates`; pliki do pobrania starsze niż siedem dni są czyszczone. Wpis startowy systemu Windows nadal używa tej samej ścieżki wykonywalnej po aktualizacji.

Kopie przenośne wymagają zapisu osobistego folderu. Zainstalowane kopie korzystają z instalatora dla chronionych katalogów; DeckPilot sama aplikacja nadal działa bez praw administratora.

**Aktualizacja z 0.1.x?** Zakończ starą wersję z obszaru powiadomień i ręcznie zastąp wykonywalny najnowszym raz. Te starsze wersje nie zawierają updater. Późniejsze aktualizacje mogą być zainstalowane z wewnątrz DeckPilot.

<a id="repository-rename-compatibility"></a>
### Zgodność zmiany nazwy repozytorium

Wersja **1.0.1** przygotowuje ruch z `lolo34dr/streamdeck-diy` do `lolo34dr/deckpilot`Repozytorium jest teraz `lolo34dr/deckpilot`. Aktualizacja kontroli użyj nowej nazwy i zachować zgodność z oryginalną nazwą. Udostępnianie pobrań jest zatwierdzane na podstawie tych dwóch dokładnych ścieżek repozytorium.

Stosowanie **1.0.1 lub później** z repozytorium przemianowanym. Wersje 0.2.0 oraz 1.0.0 ścisła walidacja oryginalnych ścieżek aktywów i może wymagać ręcznej aktualizacji. Pobierz bieżący wykonywalny ze strony wydania, jeśli starsza wersja nie może się uaktualnić.

<a id="settings-and-backups"></a>
## Ustawienia i kopie zapasowe

Profile i kalibracja są przechowywane poza folderem aplikacji:

```text
%APPDATA%\DeckPilot\settings.json
```

Zakończ DeckPilot przed skopiowaniem tego pliku do kopii zapasowej lub przywracania konfiguracji. Jeśli plik ustawień nie może być odczytany, DeckPilot zachowuje kopię z `.backup-<date>` przyrostek przed użyciem świeżej konfiguracji.

Nie jest wymagane konto. Aktualizacja sprawdza kontakt GitHub i może być wyłączony w interfejsie.

<a id="development"></a>
## Rozwój

Zainstaluj **.NET 8 SDK**, wersja 8.0.415 lub nowsza kompatybilna wersja dozwolona przez `global.json`. Visual Studio 2022 z zadaniem rozwoju .NET desktop jest opcjonalny.

Uruchom te polecenia z głównego repozytorium:

```powershell
dotnet restore DeckPilot.sln
dotnet build DeckPilot.sln -c Release --no-restore
dotnet run --project tests/DeckPilot.Checks -c Release
dotnet run --project src/DeckPilot.Windows -c Release
```

Podstawowa biblioteka i kontrola konsoli działają również na Linuksie. Interfejs WPF i API wejściowe systemu Windows wymagają systemu Windows.

<a id="validation"></a>
### Walidacja

Konsola sprawdza wyjście z niezerowym statusem przy pierwszym nieudanym stwierdzeniu. Obejmuje:

- Mapowanie kluczy, powtarzanie klawiszy held-key, wydawanie, przełączanie profilu i kopiowanie akcji.
- Walidacja ustawień, trwałość i migracja ze starszych konfiguracji.
- Wyszukiwarka aplikacji i obsługa celów.
- Wydanie wyboru, sumy kontrolne, okrojone lub uszkodzone pliki do pobrania, przekierowanie i anulowanie.
- Wykonawcze zastępstwo, zapasowe zabezpieczenie i zwroty.

Sprawdzanie blokady plików odbywa się tylko w systemie Windows.

GitHub Akcje również działa `tests/windows-update-smoke.ps1` w stosunku do pliku wykonywalnego standalone. Otwiera on helper aktualizacji, wycofuje oryginalny proces, weryfikuje potwierdzenie wymiany i uruchamiania, sprawdza zachowanie ustawień, a następnie otwiera zainstalowany picker aplikacji poprzez Automatyzację UI i zapisuje wybór. Wykonuje również zmiany nazwy plików wykonywalnych i ścieżek zawierających spacje. Skrypt ten jest zarezerwowany dla jednorazowego konta testowego GitHub Actions. `tests/windows-installer-smoke.ps1`: prawdziwa instalacja plików programu, skróty w menu pulpitu / Start, domyślne startup, upgrade z otwartą aplikacją, dezabled- startup conservation i odinstaluj bez usuwania profili.

<a id="publish-a-standalone-windows-folder"></a>
### Opublikuj samodzielny folder Windows

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Dystrybucja kompletnego archiwum. System Windows build workflow produkuje również ten pakiet.

<a id="publish-a-github-release"></a>
### Opublikuj wydanie GitHub

Aktualizuj wersję aplikacji i `.github/release-notes.md`, a następnie naciśnij dopasowanie `v*` tag, takie jak `v1.0.0`. The release workflow checks that the tag means the project version and runs walidation before publishing:

| Pobieranie | Spis treści |
| --- | --- |
| `DeckPilot.exe` | Self- contained, single- file Windows x64 application. |
| `DeckPilot-win-x64.zip` | Kompletny samodzielny folder aplikacji. |
| `SHA256SUMS.txt` | Sha-256 hashs for both downloads. |

Dla systemu Windows ARM64, publikować oddzielnie z `-r win-arm64` i potwierdzić na maszynie docelowej.

<a id="hardware-checks-on-windows"></a>
## Sprawdzanie sprzętu w systemie Windows

Z podłączonym Leonardo:

- Kalibracja wszystkich 12 pozycje w arbitralnej kolejności F13- F24. Potwierdzenie duplikatu kluczy jest odrzucane i anulowanie zachowuje poprzednie mapowanie.
- Przypisz darmowy klucz w grze i sprawdź, czy gra otrzymuje go normalnie.
- Uruchom skonfigurowany program, a następnie przytrzymaj przycisk: powinien uruchomić jeden raz do wydania. Dołącz ścieżki zawierające spacje i opcjonalne argumenty.
- Przetestuj folder, stronę internetową, skrót w zwykłej aplikacji i sterowanie mediami.
- Wyślij F13- F24 za pomocą skrótu i potwierdzić, że nie ma pętli DeckPilot działania.
- Tworzenie dwóch profili z różnych działań, przełączanie między nimi, zakończenie i wznowienie. Profile i kalibracja powinny trwać.
- Zamknij okno, uruchom akcję, a następnie ponownie otwórz i zakończ pracę przez ikonę obszaru zgłoszenia. DeckPilot Wyjścia, wszystkie klucze powinny przejść normalnie.
- Włącz uruchamianie i testowanie systemu Windows po zalogowaniu się ponownie; wyłączyć go po niechcianym uruchomieniu.

<a id="current-limitations"></a>
## Bieżące ograniczenia

- Klucz odsłuchowy jest globalny dla sesji Windows. Fizyczne klawisze F13- F24 z innej klawiatury postępują zgodnie z tą samą konfiguracją. Zdarzenia klawiszowe wstrzyknięte przez Software są ignorowane, aby zapobiec pętlom akcji.
- Niektóre gry, systemy antyoszustw lub programy za pomocą bezpośredniego wejścia urządzenia mogą nadal otrzymywać przechwycone klucze. Tryb Free- key pozostaje dostępny. Sygnał w ekranie, bezpieczny pulpit i inne sesje są poza kontrolą aplikacji.
- Windows może blokować skróty wysyłane do programów uruchomionych z uprawnieniami administratora. DeckPilot działa bez praw administratora i nie omija tego ograniczenia.
- Wydanie Ctrl, Alt, Shift i Windows przed uruchomieniem działania skrótu. Kombinacje takie jak Alt + Tab pozostają przedmiotem zachowania systemu Windows.
- Detekcja odłączenia, automatyczne profile pergaminowe i podpisany instalator to przyszłe ulepszenia. Profile są obecnie wybierane ręcznie.

<a id="project-structure"></a>
## Struktura projektu

| Ścieżka | Cel |
| --- | --- |
| `src/DeckPilot.Core` | Ustawienia, stan klucza, wyszukiwanie, walidacja wydania GitHub, zweryfikowane pobieranie i transakcje wymiany. |
| `src/DeckPilot.Windows` | Interfejs WPF, katalog aplikacji, ikony, działania w systemie Windows, integracja obszaru zgłaszania i aktualizacja helper. |
| `tests/DeckPilot.Checks` | Kontrole wykonywane bez zewnętrznych ram testowych. |
| `tests/windows-update-smoke.ps1` | Native Windows update and application- picker smoke test used by CI. |
| `deckpilotlogo.png` | Logo aplikacji. |
| `deckpilotbanner.png` | Baner projektu. |

<a id="license-and-credit"></a>
## Licencja i kredyt

DeckPilot jest zwolniony na podstawie **[Licencja MIT](../../LICENSE)**. Możesz używać, kopiować, modyfikować i redystrybuować go, w tym komercyjnie, tak długo, jak długo zachowasz prawa autorskie i ogłoszenia licencyjne wymagane przez licencję. Związany denoiser RNNoise audio jest oddzielnym komponentem trzeciej strony na licencji BSD- style; patrz [`third-party/RNNoise/COPYING`](../../third-party/RNNoise/COPYING) oraz [`third-party/RNNoise/NOTICE.md`](../../third-party/RNNoise/NOTICE.md).

If you build on DeckPilotDoceniany byłby niewielki, widoczny kredyt oraz link do tego repozytorium. Ten dodatkowy kredyt jest nieobowiązkowy; utrzymanie ogłoszeń MIT jest nadal wymagane.

Proponowany kredyt:

> Na podstawie [DeckPilot](https://github.com/lolo34dr/deckpilot) przez [lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Macropad firmware aktualizacje

Dla przygotowania urządzenia i kontroli jakości warsztatu, zainstalować oddzielny [DeckPilot Atelier](../../docs/FACTORY.md) narzędzie. Jego instalator jest opublikowany jako **[DeckPilot-Atelier- Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Atelier-Setup.exe)** obok aplikacji głównej. Może wyświetlić dołączone oprogramowanie firmowe na desce Leonardo / Caterina, a następnie sprawdzić krótkie i długie prasy na wszystkich 12 klawisze fizyczne i eksportować raport.

DeckPilot 1.1.0 zawiera **Firmware du macropad…** okno dialogowe i oprogramowanie firmowe 1.0.0 dla Pro Micro ATmega32U4 **5 V / 16 MHz**, przy użyciu konfiguracji Leonardo / Caterina potwierdzonej przez właściciela dziennika wysyłania. Złączone narzędzie do migania systemu Windows nie wymaga Arduino IDE. Wybór urządzenia i potwierdzenie są obowiązkowe; aktualizacje aplikacji nigdy po cichu nie świecą planszą. Istniejące mappingi, profile i kalibracja F13- F24 pozostają kompatybilne.

**Właściciel potwierdził udaną migawkę w aplikacji, firmware 1.0.0 identyfikacja i wszystkie 12 przyciski na tablicy odniesienia.** Przechowywać oryginalny szkic i dostęp do RST / GND do odzysku. Mignięcie firmware nie może być toczone automatycznie. Zobacz [dokumentacja oprogramowania firmowego](FIRMWARE.pl.md) dla kompatybilności, walidacji, odzyskiwania, budowania kroków i licencji stron trzecich. Przycisk Per- tryby USB i natywnych kontroli mediów nie są częścią tej wersji oprogramowania firmowego.

<a id="building-the-windows-installer"></a>
### Budowa instalatora systemu Windows

Po produkcji `artifacts/standalone/DeckPilot.exe` z wbudowanym pakietem oprogramowania firmowego, zainstalować Inno Setup 6 i uruchomić `./installer/build.ps1` z głównego repozytorium. Wyjściem jest `artifacts/installer/DeckPilot-Setup.exe`. CI buduje i testuje tego instalatora przed dołączeniem go do wydania. Instalator używa stabilnego ID aplikacji, więc przyszłe wersje uaktualniają tę samą instalację.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Kontrola podłączenia deski rozdzielczej i oprogramowania firmowego (1.3.0)

W przypadku **Votre macropad** karta pokazuje stan połączenia, wykryte oprogramowanie firmowe i wszelkie dostępne pakiety aktualizacji. **Connexion et diagnostic** Eksponuje wybór planszy po podłączeniu kilku Leonardos, stan łącza USB na żywo, fizyczne liczenie przycisków, preferencje powiadomień firmware- i naśladowalne podsumowanie diagnostyczne. Diagnostyka nie kopiuje akcji profilu lub seryjnych identyfikatorów USB. Monitor ponownie łączy się po odłączeniu i oddaje automatycznie port szeregowy, podczas gdy menedżer oprogramowania firmowego jest otwarty. Działania klawiatury F13- F24 nadal działają z firmware 1.0.0 i bez ogniwa diagnostycznego.

Sprawdzanie oprogramowania firmowego w startupie jest domyślnie włączone. Po tym, jak sprawdzanie aktualizacji i kalibracja aplikacji są wyłączone, starsze zidentyfikowane oprogramowanie firmowe może skłaniać do otwarcia instalacji; opóźnianie przypomnienia o opóźnieniach 24 godziny. Nieznane tablice i wielu niewybranych kandydatów nie są automatycznie wyświetlane. Wydawnictwa firmware są połączone z aktualizacjami aplikacji z GitHub, a nie pobierane z niezależnego kanału firmware.

Oprogramowanie firmowe **1.1.1** dodaje identyfikację sesji, skorelowane odpowiedzi na bicie serca, ograniczone komunikaty i fizyczne zdarzenia stanu key-. Zdarzenia te animują mapowane przyciski i aktualizują diagnostykę; nigdy nie powołują się na działania po raz drugi. Jego pierwsza walidacja płyt fizycznych jest nadal wymagana, chociaż wcześniejsze oprogramowanie firmowe 1.0.0 i flash procedury zostały potwierdzone przez właściciela. Zobacz [dokumentacja dotycząca protokołu i zgodności](FIRMWARE.pl.md).

<a id="keypad-interface-140"></a>
## Interfejs klawiatury (1.4.0)

Im większy 3 × 4 klawiatura śledzi fizyczną obudowę i czarne nasadki klawiszy. Każdy klucz pokazuje swoją główną ikonę, etykietę, mapowanie F13- F24 i małą ikonę typu action- type; wskazówki o narzędziach zachowują pełną etykietę i pozycję. Naciśnij sprzężenie zwrotne, uruchamianie działań, udaną wysyłkę i błędy mają różne pozory, w tym symbole, a nie tylko kolor. Animacje podążają za preferencjami animacji systemu Windows.

Wybierz klucz do edycji. **Options avancées · fichier et arguments** utrzymuje ręczne ścieżki aplikacji i argumenty startowe zawalone domyślnie, podczas gdy podstawowe foldery i cele strony internetowej pozostają rozszerzone. **Tester l’action** zapisuje i wysyła wybrane działanie na bieżącym komputerze PC; wskaźnik sukcesu potwierdza wysyłkę, a nie ukończenie wewnątrz innej aplikacji. Skróty klawiszowe mają na celu aktywne okno. Wolne klawisze F13- F24 nie mają działania testowego.

Kiedy makropad jest odłączony, **Hors ligne** i towarzysząca podpowiedź wyjaśnia, że profile mogą być nadal edytowane i testowane. Konfiguracja jest przechowywana na komputerze PC i pozostaje dostępna w trakcie rozłączania i ponownego łączenia. Te usprawnienia interfejsu działają z istniejącym oprogramowaniem firmowym i nie wymagają dodatkowego błysku.

Menu obszaru zgłoszeniowego włącza teraz bezpośrednio profile. **Ctrl + S** Zapisuje bieżącą konfigurację przycisku. Nieprawidłowe pliki konfiguracyjne są kopiowane zanim zostanie użyta świeża konfiguracja. Instalacja aplikacji, ikony menu desktop / Start, preferencje startowe i istniejący automatyczny updater są zatrzymywane.

Do ręcznego przesyłania danych IDE Arduino, **zrezygnować DeckPilot z menu tacki** aby uwolnić port szeregowy. Zamknięcie głównego okna utrzymuje go w tle.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, wyszukiwania akcji i drag- and- drop (1.5.0)

**Spotify** jest oddzielony od **Multimédia**. Jego odtwarzanie / pauza, następne i poprzednie polecenia adresują sesję mediów Windows w aplikacji Spotify, nawet gdy przeglądarka gra na YouTube. 5% kroki, pozostawiając główny wolumin i innych graczy bez zmian. Spotify musi być otwarty i mieć dostępną sesję odtwarzania; uruchomić raz. Spotify w przeglądarce nie jest obsługiwane. Brakujące Spotify sesje produkują błąd, bez odwrotu do globalnych kluczy medialnych lub innego gracza. Brak Spotify autoryzacji konta, premium subskrypcji lub zapisanego token jest potrzebny przez DeckPilotOkna 10 wersja 2004 lub później, lub Windows 11, jest wymagane przez tę budowę.

Stosowanie **Rechercher une action ou une application** w przypadku zapytań takich jak: `volume`, `skip`, `Spotify` lub nazwę zainstalowanej aplikacji. Wskaźnik instalacji aplikacji ładuje się przy pierwszym wyszukiwaniu i jest buforowany przez pięć minut; ręczne wybieranie plików pozostaje dostępne. Kliknij wynik, aby skonfigurować wybrany klucz lub przeciągnąć go na inny klucz.

<a id="obs-studio-and-streamlabs-desktop-control"></a>
## OBS Studio i Streamplates Sterowanie pulpitem

Wybierz **OBS / Strealaboratory** dla klawisza, wybierz program docelowy i polecenie, a następnie zapisz. DeckPilot Łączy się z lokalnym interfejsem WebSocket / RPC aplikacji; nie wysyła skrótów klawiszowych. Działania OBS obejmują przełączanie sceny, start / stop strumienia, nagrywanie startu / stop / pauza / resume, mikrofon lub desktop- audio nieme, widoczność źródła, odtwarzanie bufor, Studio Mode i kamera wirtualna. Działania strumieniowe obejmują przełączanie sceny, strumień i nagrywanie start / stop, source- audio nieme, widoczność źródła i bufora powtórki.

Kontrole te wykorzystują istniejące zdarzenia przycisków macropad i nie wymagają zmiany oprogramowania firmware ani zmiany ustawień.

Stosowanie **Tester la connexion et récupérer les scènes** aby pobrać dostępne sceny z wybranej aplikacji. Dla działań audio lub źródłowych, wprowadź dokładną nazwę źródła / wejścia użytą wewnątrz tej aplikacji. **Narzędzia → Ustawienia serwera WebSocket** (OBS 28 i później włączyć go). Streampalls Desktop wymaga **Ustawienia → Zdalne sterowanie** być włączone; skopiować jego token z **Pokaż szczegóły**. DeckPilot przechowuje hasło / token za pomocą ochrony danych poziomu użytkownika systemu Windows. Aplikacje muszą być uruchomione po uruchomieniu działania. Zobacz oficjalny [Przewodnik kontroli oddalenia OBS](https://obsproject.com/kb/remote-control-guide) oraz [Streamalls Desktop API reference](https://github.com/streamlabs/desktop/blob/master/docs/README.md).

Przeciągnij akcję z katalogu lub wyniki wyszukiwania na klucz, aby ją przypisać. Przeciągnij jeden skonfigurowany klucz na inny **wymieniają się swoimi działaniami**; fizyczna kalibracja pozostaje niezmieniona. Możesz również upuścić jedną `.exe`, `.lnk`, `.appref-ms`, folder lub HTTP (S) URL na klucz. Upuszczanie tylko konfiguruje klucz i nigdy nie wykonuje działania. Ukończ brakujące ustawienia w edytorze.

Uruchomienie systemu Windows utrzymuje teraz wyraźną preferencję dla użytkownika, naprawia nieświeży lub brakujący wpis Run i informuje o znanych flagach startowych systemu Windows. Normalne uruchamia z poszanowaniem wyłączenia systemu Windows; wyraźnie umożliwia **Lancer avec Windows** oczyszcza znane DeckPilot dezaktywować flagę dla bieżącego użytkownika. Wyrzutnie tła pozostają ukryte nawet przed kalibracją, a duplikaty wychodzące po cichu. Po aktualizacji, otwórz **Préférences de l’application**, sprawdzić status startowy i ponownie aktywować opcję jeśli Windows zablokował go. Prawdziwe sign- out / sign- in na komputerze użytkownika pozostaje ostateczna kontrola dla polityki Windows lub ograniczeń specyficznych dla maszyn.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Ostre renderowanie klawiatury, odzyskiwanie profilu i sterowanie dźwiękiem (1.6.1)

Klawiatura staje się teraz w rodzimych rozmiarach WPF zamiast skalowania tekstu przez Viewbox. Efekty cieni rodzicielskiej / kluczowej oraz animacje skali tekstu zostały usunięte; mniejsze okna przewijają się raczej niż kurczą klawiaturę. Aplikacja deklaruje świadomość DPI monitora i wykorzystuje większe ikony powłoki systemu Windows do aplikacji na pulpicie.

**Annuler / Rétablir** przywraca edycje profili, w tym swapy drag- and- drop, ustawienia akcji, import i tworzenie / usuwanie profilu. Historia utrzymuje 50 edytuje podczas sesji aplikacji. Ctrl + Z / Ctrl + Y działają na profilach poza polami tekstowymi; pola tekstowe zachowują swoje własne cofnięcie / redo. Kalibracja, preferencje uruchamiania i wykonywane działania nie są cofane.

Pod **Gérer les profils**, eksportować obecny profil lub wszystkie profile jako `.deckpilot.json`, importować profile jako nowe kopie, lub przywrócić kopię zapasową wszystkich profili. Import nigdy nie wykonuje działań i nigdy nie zastępuje kalibracji komputera PC. Profile zawierają ścieżki działania, argumenty i identyfikatory audio punktów końcowych, które mogą wymagać dostosowania na innym komputerze PC. Nieznane formaty, przerośnięte pliki i nieprawidłowo ukształtowane konfiguracje gestów są odrzucane.

Przed zapisaniem zmienionych profili, DeckPilot utrzymuje poprzednie profile w `%APPDATA%\DeckPilot\Backups` (20 Wersja, content- deduplicated). Tylko preference- rates nie tworzy kopii zapasowych profilu. Błąd kopii zapasowej zapobiega nadpisywaniu aktualnych ustawień. Przywracanie zastępuje profile po potwierdzeniu i może zostać cofnięte; kalibracja pozostaje niezmieniona.

**Microphone Windows** Włącza wyciszenie wszystkich obecnie aktywnych punktów końcowych przechwytywania systemu Windows. Utrzymujący się pasek stanu i karta Audio Windows pokazują aktywny / zmiksowany / mieszany / niedostępny stan, odświeża się co dwie sekundy i po komendach aplikacji. Nieudane wielopunktowe próby aktualizacji w celu przywrócenia poprzednich stanów niemych. Wskaźniki te opisują stan końcowy systemu Windows, a nie przełącznik sprzętowy lub własny przycisk niemowy aplikacji.

**Changer de sortie audio** Zastępuje dwa wybrane, połączone urządzenia wyjściowe, dla konsoli Windows, multimediów i komunikacji domyślne. Brak celu daje błąd bez wyboru innego urządzenia. Aplikacje z ich własnym wyjściem stacjonarnym mogą nie być zgodne z domyślnym Windows. Importowane identyfikatory punktów końcowych są wyświetlane jako niedostępne do czasu dostosowania do obecnego komputera PC. Nie powiodła się próba zmiany trasy przywracania poprzednich domyślnych.

Konfigurowanie **appui court / appui long**, najpierw wybierz skonfigurowane działanie, włącz opcję two-action, wybierz gest do edycji i ustawić próg od 200 do 3000 ms. Bez tego trybu działania zachowują natychmiastowe zachowanie klawisza. Z nim krótkie akcje przebiegają po zwolnieniu przed progiem; długie akcje przebiegają raz na progu (lub po zwolnieniu, jeśli zegar jeszcze nie działa). Długa akcja nigdy nie działa również na krótki. Wolne klawisze F13- F24 nie mogą używać trybu dual- action. Oczekiwane gesty są anulowane na zmiany profilu / konfiguracji, kalibracji, operacji firmware, zmian sesji i zawieszenia. Nie jest potrzebne oprogramowanie firmware flash; istniejące raporty klawiatury key- down / key- up dostarczają gestów.

Kontrole podstawowe obejmują granice powtarzania / uwalniania, jednoczesne przytrzymywanie, anulowanie, historię profilu / wymiana / kopie zapasowe i wrzuty audio przy użyciu fałszywych punktów końcowych. Sprawdzanie UI systemu Windows wykonuje rzeczywiste przeciążenie undo / redo, konfigurując długie działanie i próg, native keypad wymiary po zmianie rozmiaru i wskaźnik mikrofonu. Rzeczywiste sterowniki mikrofonu, headset / głośnik przełącznik i fizyczne uchwyty nadal wymagają walidacji na komputerze właściciela.

<a id="search-and-ci-reliability-fixes-164"></a>
## Wyszukiwarka i niezawodność CI (1.6.4)

Równoważne wyniki wyszukiwania zachowują swoje istniejące przyciski WPF. Katalog instalacji aplikacji i debuunce timer odracza wymianę wyników, podczas gdy przycisk wyszukiwania jest przechwytywany lub przeciąganie jest w toku. To zapobiega usuwaniu przycisku źródłowego podczas interakcji.

Kontrola dymu w systemie Windows nadal wykonuje natywne przeciągnięcia myszy i sprawdza zapisane zadania, swapy, cofnij / redo i oba gesty prasowe. Klient testowy używa fizycznych współrzędnych kursora, aby dopasować się do granic Automatyzacji UI niezależnie od DPI, koncentruje się przed pomiarem i skanuje wszystkie wartości procentowe przewijania. Sprawdzenie regresji sprawdza również, czy równoważne wyszukiwanie zachowuje ten sam dostępny przycisk. Aseria startowe Background- startup czekają na ładowarkę WPF i wymagają pięciu kolejnych obserwacji hidden- okiennych, podczas gdy aplikacja pozostaje przy życiu.

Budowanie i zwalnianie przepływów roboczych za pomocą Node.js 24 działania. Kompilator Arduino pozostaje w CLI 1.2.2; CI pobiera swoje archiwum Windows z przypiętym Sha-256 suma kontrolna. Oprogramowanie firmowe i jego wersje łańcucha narzędzi nie ulegają zmianie.

Ustawienia odczytywane w testach Windows wyraźnie pozwalają odczytywać / zapisywać / usuwać udostępnianie i parse UTF-8 Po zamknięciu czytnika. To zapobiega temu, że test sam w sobie odmawia wymiany plików atomowych aplikacji. Test historyczny utrzymuje stare zdjęcie otwarte przez trzy cykle cofania / ponownego wykonania i sprawdza, czy pozostaje nienaruszony podczas zmiany ustawień na żywo.

W systemie Windows istniejące pliki konfiguracyjne są oddane z `File.Replace` zamiast trybu nadpisarskiego `File.Move`To obsługuje czytniki, które pozwalają usunąć przy zachowaniu kompletnego starego migawki. Podstawowy apartament testuje trzy zamiany z czytnikiem utrzymywane otwarte; Kontrola rdzeń systemu Windows uruchomić przed firmware kompilacji i publikacji tak uporczywe błędy są wykrywane wcześnie.

Czytnik migawek testowych może czekać aż 500 ms dla Windows udostępniania / naruszenia blokady podczas `ReplaceFile` commits. Poczekanie dotyczy tylko otwarcia migawki, nigdy nie wykonywania lub ponownego wykonania działania aplikacji. Inne błędy, źle ukształtowany JSON i trwałe zamki pozostają niepowodzeniem.

<a id="runtime-and-editing-optimizations-170"></a>
## Optymalizacja czasu pracy i edycji (1.7.0)

Zapisywanie identycznych ustawień sprawdza bieżący plik, ale pomija pracę zamiany i tworzenia kopii zapasowych. Zewnętrzne edycje / usuwanie i nieudany zapis są nadal wykrywane. `ButtonAction` ma równość wartości (w tym długie działanie); edytor brudnych czeków uniknąć serializacji JSON. Historia utrzymuje oddzielone migawki modelu, waliduje nowe stany, i unika JSON parsing podczas cofnięcia / redo. 50-edit limit i 20 Pozostają kopie zapasowe profilu.

Search and picker udostępniają niezmienny katalog instalacji-aplikacji na pięć minut (15 seconds for a partial result). Współbieżni konsumenci dzielą jeden skan; zamknięcie jednego picker nie anuluje innego konsumenta. Odśwież pomija bufora, a każdy picker ma swój własny filtr / widok. Skanowanie i katalogowanie parsowanie uruchomić z wątek UI; picker filtrowanie jest oblężone przez 120 ms. Wyszukiwarka akcji ponownie sprawdza tę pamięć podręczną po jej odrzuceniu i po powrocie z pickera, więc odświeżony katalog jest tam dostępny.

Aktualizacje UI planszy teraz śledzić powiadomienia o połączeniach zamiast budzenia każdego 100 MS. Zamówione powiadomienia o odłączeniu nadal anulują oczekujące uchwyty. Haki Keyboard kolejki wizualne pracy, podczas supresji i gestu czasu pozostają synchroniczne. Ukryte / zminimalizowane okna zatrzymać animacje sprzężenia zwrotnego i używać dziesięciosekundowy interwał sondowania audio zamiast dwóch sekund; ponowne otwarcie lub wydanie komendy audio żąda świeży stan. W oczekiwaniu audio odświeża udostępniać pętlę i zawierać prośby przychodzące podczas istniejącego czytania. Niezmienione migawki audio i zamrożone szczotki uniknąć wielokrotnych przydziałów UI. Process wrapers zwracany przez uruchomienie aplikacji są usuwane bez zamykania uruchomionego programu.

Zmiany te zmniejszają określoną pracę; nie tworzą niezależnego od maszyn procesora / RAM procent. Native sprzętowy przełączanie audio i fizyczne zachowanie klucza nadal wymagają walidacji na komputerze z systemem Windows PC z makro pad.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Narzędzia do pracy i odświeżony interfejs (1.8.0)

Interfejs węgla drzewnego stawia bibliotekę działania na pierwszym miejscu, oddziela kontrolę profilu od Crisp native- size 3 × 4 klawiatura i grupy ustawień akcji w karcie. Kluczowe akcenty wyróżniają własne kategorie; wybór, ostrość klawiatury i informacje zwrotne akcji pozostają osobne. **Réduire les animations** w preferencjach uzupełnia ustawienia animacji Windows. Mniejsze okna zachowują dostęp do przewijania zamiast kurczenia się tekstu klucza.

- **Szablony profilu:** poniżej **Gérer les profils**, Dodaj muzykę i dźwięk, pulpit i wydajność, lub Tworzenie. Każdy szablon tworzy osobny edytowalny profil i nigdy nie wykonuje swoich działań. Istniejące profile i kalibracja fizyczna są zachowane. Skróty klawiszowe używają aktywnej aplikacji, której wiązania mogą się różnić.
- **Klucze profilu:** przypisywanie **Changer de profil**, następnie wybierz nazwany profil lub cykl do przodu / do tyłu. Referencje między profilami są remapowane podczas importowania zestawu lub powielania profilu samoodsyłającego. Brak miejsca docelowego zgłasza błąd i może być zastąpiony w edytorze.
- **Polecenia Windows:** do stosowania **Commandes Windows** aby otworzyć wspólne ustawienia lub Task Manager, lub wyraźnie zablokować komputer PC. Polecenia pochodzą ze stałej listy, bez skryptu do wprowadzenia. Akcja blokady wymaga podpisanie z powrotem do systemu Windows.
- **Komplete- klucz schowka:** ctrl + C / X / V i Delete pracy, podczas gdy klawisz pokładowy ma ostrość klawiatury; zwykłe pola tekstowe utrzymują swoje skróty edycji. Klawisz schowka pozostaje wewnątrz DeckPilot i jest oczyszczony przy wyjściu. Te edytuje wsparcie cofnij / ponownie.
- **Rejestrator skrótów:** wybrać akcję klawiatury i kliknąć **Enregistrer un raccourci**. Naciśnij wspieraną kombinację, a następnie zapisać go. Anulowanie ucieczki; F13- F24 jest zarezerwowany dla makro pad podczas nagrywania.
- **Szybki dostęp:** Ctrl + K lub przycisk Biblioteka koncentruje wyszukiwanie akcji. **Activité récente** utrzymuje ostatni 30 wysłanie / nieudana akcja w pamięci dla tej sesji, bez zapisywania argumentów programu lub ścieżki docelowe. Można ją w każdej chwili wyczyścić. Udane wysłanie nie gwarantuje zakończenia w aplikacji docelowej.

Pozostaje oprogramowanie firmowe **1.1.1**. Akceptowanie automatycznej oferty oprogramowania firmowego teraz poprawnie otwiera menedżera oprogramowania firmowego; poprzednia ochrona modalna uniemożliwiła przekazanie go. Istniejąca identyfikacja oprogramowania firmowego, bicie serca, ponowne podłączenie i kontrola flash pozostają na miejscu. Nowe funkcje przestrzeni roboczej nie potrzebują błysku firmware.

<a id="microphone-noise-suppression-220"></a>
## tłumienie szumów mikrofonów (2.2.0)

**Soundboard · micro virtuel** teraz zawiera lokalne tłumienie hałasu RNNoise dla wybranego mikrofonu fizycznego. Włącz **Filtrer le bruit du micro** i dostosować **Intensité**. DeckPilot filtruje głos przed zmieszaniem w klipach płyt dźwiękowych, więc klipy pozostają niezmienione i nie zostaną usunięte przez filtr hałasu. Filtr działa dla każdego programu, który wykorzystuje **CABLE Output** jako jego mikrofon, w tym gry i głośnomówiące aplikacje; programy ustawione do korzystania z mikrofonu fizycznego bezpośrednio nie przechodzą przez DeckPilot.

Audio jest przetwarzane lokalnie w DeckPilot. Nie jest rejestrowane lub wysyłane przez sieć. RNNoise pracuje w 10 ms, 48 klatka mono kHz; ten rozmiar ramki nie jest gwarancją latencji na koniec, która zależy również od systemu Windows, urządzeń i VB-CABLE. Ta aktualizacja nie wymaga oprogramowania firmware flash. Native RNNoise komponent jest objęty własną licencją BSD- stylu, zaznaczone powyżej.

<a id="soundboard-return-and-on-screen-sound-notifications-219"></a>
## Soundboard powiadomienia zwrotne i dźwiękowe na ekranie ekranu (2.1.9)

W **Soundboard · micro virtuel**, enable **Entendre aussi les sons du soundboard** aby usłyszeć klipy przez słuchawki lub głośniki, jak również wirtualny mikrofon. Wybierz wyjście fizyczne i ustawić odrębną głośność zwrotną z 0 do 100%. Ten zwrot jest domyślnie wyłączony i zapisany na tym komputerze. **Tylko klipy z tablic dźwiękowych**; twój fizyczny mikrofon nigdy nie jest dodawany do wyjścia głośnika. Wirtualny mikrofon nadal wysyła twój głos i klipy do VB-CABLE.

Zmiana głośności nie przerywa wirtualnego mikrofonu. Jeśli wybrany głośnik odłącza się, powrót zatrzymuje się podczas trwania wirtualnego mikrofonu. Odśwież go, odśwież listę urządzeń lub wybierz inne wyjście. Słuchawki zapobiegają akustycznemu odbieraniu klipów przez fizyczny mikrofon.

Głośnik zwraca teraz korzysta z ogłoszonego przez wybrane urządzenie formatu audio Windows, w tym z rozszerzalnych formatów używanych przez niektóre zestawy słuchawek USB, i powraca z odtwarzacza z napędem event- driver, jeśli kierowca go odrzuci. Uruchomienie klipu soundboard wyświetla również swoją nazwę w tym samym konfigurowalnym, kliknącym nakładce jako zmiany profilu. Preferencje i pozycja nakładki odnoszą się do obu ogłoszeń.

DeckPilot Atelier pozostaje w 1.0.12 i firmware pozostaje 1.1.1. Nie jest potrzebne oprogramowanie firmware flash. Rzeczywiste routing głośników nadal zależy od urządzenia audio systemu Windows i powinien być sprawdzany na komputerze właściciela.

<a id="soundboard-speaker-return-218"></a>
## Soundboard powrót głośnika (2.1.8)

W **Soundboard · micro virtuel**, enable **Entendre aussi les sons du soundboard** aby usłyszeć klipy przez słuchawki lub głośniki, jak również wirtualny mikrofon. Wybierz wyjście fizyczne i ustawić odrębną głośność zwrotną z 0 do 100%. Ten zwrot jest domyślnie wyłączony i zapisany na tym komputerze. **Tylko klipy z tablic dźwiękowych**; twój fizyczny mikrofon nigdy nie jest dodawany do wyjścia głośnika. Wirtualny mikrofon nadal wysyła twój głos i klipy do VB-CABLE.

Zmiana głośności nie przerywa wirtualnego mikrofonu. Jeśli wybrany głośnik odłącza się, powrót zatrzymuje się podczas trwania wirtualnego mikrofonu. Odśwież go, odśwież listę urządzeń lub wybierz inne wyjście. Słuchawki zapobiegają akustycznemu odbieraniu klipów przez fizyczny mikrofon.

<a id="open-deckpilot-and-profile-notifications-217"></a>
## Otwórz DeckPilot oraz powiadomienia o profilu (2.1.7)

Pod **Ouvrir une application**, wybierz built- in **DeckPilot** wejście lub kliknięcie **Affecter "Ouvrir DeckPilot »**Ta akcja przynosi istniejący DeckPilot okno do przodu; jego cel pozostaje ważny po aktualizacji aplikacji lub ponownej instalacji. Jest on również dostępny w wyszukiwaniu akcji.

Zmiana profilu pokazuje krótki, klik-poprzez powiadomienie z aktywną nazwą profilu. **Préférences de l’application**, można go wyłączyć, wybrać dowolny kąt, lub ustawić niestandardowe położenie poziome i pionowe. **Enregistrer et prévisualiser** wyświetla wynik natychmiast. Zgłoszenie następuje po ekranie zawierającym aplikację pierwszoplanową, a także pojawia się podczas DeckPilot jest ukryta. Może pojawić się w grach bez granic / z parapetem; ekskluzywne gry pełnoekranowe mogą obejmować normalne nakłady Windows.

<a id="deckpilot-210-fixed-signed-virtual-microphone-routing"></a>
## DeckPilot 2.1.0: stałe, podpisane wirtualne routing mikrofonu

DeckPilot teraz domyślnie używa głębokiego czarnego interfejsu. **Préférences de l’application**, zmienić kolory tła, panelu i akcentu za pomocą `#RRGGBB` wartości; Twój motyw jest zapisany na tym komputerze.

W przypadku **Soundboard · micro virtuel** panel pozwala na wybór mikrofonu fizycznego i klipów MP3 / WAV. DeckPilot automatycznie przenosi mix do VB- Audio VB-CABLE punkt końcowy odtwarzania; w Discord lub gra, wybrać **`CABLE Output`** jako mikrofon. Podczas instalacji i aktualizacji, DeckPilot kontrole VB-CABLE i instaluje go, gdy brakuje. Pobiera oficjalny pakiet przez HTTPS i weryfikuje wydawcę Authenticode przed uruchomieniem cichego instalatora sterownika. Wymaga to połączenia internetowego i zezwolenia administratora. DeckPilot nie gromadzi ani nie redystrybuuje plików VB- Audio. Jeśli później nie ma sterownika, można uruchomić ten sam instalator sprawdzony sygnałem z Soundboard panel. Silnik audio wykorzystuje NAudio (MIT; patrz: [Ogłoszenie stron trzecich](../../third-party/NAudio-LICENSE.txt)). VB-CABLE jest oddzielnie licencjonowanym, podpisanym sterownikiem Windows, a nie komponentem MIT.

<a id="deckpilot-atelier-190"></a>
## DeckPilot Atelier (1.9.0)

Atelier sprawdza teraz wybraną tablicę i oprogramowanie firmowe przed miganiem, porównuje zainstalowane i połączone wersje i oferuje zweryfikowane ponowne próby po przerwaniu pisania. **Mode atelier en série...** diagnozuje i błyszczy wszystkie wykryte tablice sekwencyjnie i może eksportować raport wsadowy. 12-test kluczowy sprawdza fizyczny 01→12 układ, rejestruje mapowanie F13- F24 i zawiera oprogramowanie firmowe i klucz per- wyniki w raporcie jakości.


Oddzielny **DeckPilot Atelier** Instalator systemu Windows przygotowuje nowe makropadki DIY. Wykrywa deski Leonardo / Caterina, waliduje i błyskuje sprzętem firmowym w pakiecie bez nadpisywania bootloadera, a następnie prowadzi fizyczny 3×4 kontrola wszystkich 12 przyciski. Każdy przycisk musi przejść krótką prasę i uchwyt co najmniej 700 MS. Narzędzie rejestruje, który sygnał F13- F24 należy do każdej pozycji fizycznej, flagi wiele lub niespójne sygnały, i eksportuje raport oznaczony czasem. Pobierz `DeckPilot-Atelier-Setup.exe` ze strony wydania. Bootloader Caterina musi być już obecny; narzędzie nie instaluje brakującego bootloadera.

DeckPilot Atelier sprawdza GitHub dla aktualizacji aplikacji i bundled- firmware na startup i na żądanie. Aktualizacje wymagają potwierdzenia i używać wydania SHA-256 manifest. Firmware nigdy nie jest wyświetlany automatycznie przez aktualizację; po aktualizacjach Atelier, wybierz działanie firmware, gdy jesteś gotowy do zaprogramowania podłączonej płyty.

Atelier 1.0.7 dodaje potwierdzone **Program Effacer le DeckPilot** działania. To oczyszcza 28 Obszar aplikacji KiB z zweryfikowaną instrukcją bezpiecznego zatrzymywania, więc płyta przestaje działać jako klawiatura, zachowując Caterina do odzyskiwania i przyszłego migania. Pozostawia bootloader, EEPROM i Windows profile nietknięte.

Wersja 1.9.1 naprawia uruchomienie Atelier po instalacji poprzez uruchomienie go w sesji signed- in Windows. Jego updater może zainstalować nowszy pakiet Atelier lub pakiet oprogramowania firmowego, a następnie ponownie uruchomić aplikację.

Wersja 1.9.2 Poprawia etap pakowania Atelier Windows i informuje o dokładnej lokalizacji awarii w CI.

Wersja 1.9.3 koryguje sposób, w jaki pakiet Windows odczytuje dołączoną wersję oprogramowania firmowego, tak więc aktualizacja Atelier śledzi oprogramowanie firmowe wysłane do instalatora.

Sprawdzenie wydania Atelier zgłasza diagnostykę uruchamiania, jeśli system Windows nie otworzy zainstalowanego okna.

DeckPilot 1.9.6 naprawia manifest DPI systemu Atelier, dzięki czemu zainstalowana aplikacja uruchamia się poprawnie w systemie Windows 10 oraz 11.

DeckPilot 1.9.7 w czasie jego pracy usuwa Atelier. Test wydania systemu Windows weryfikuje również uruchomienie, wprowadzenie aktualizacji i odblokowanie.

DeckPilot 1.9.8 dodaje jednoznaczny, potwierdzony reset Atelier, który zastępuje program użytkownika planszy zweryfikowanym pustym szkicem i zachowuje bootloader Caterina.

Walidacja obejmuje dotychczasowe ustawienia, walidację akcentu, nawigację profilu i remapowanie odniesienia, przenośne szablony, natywny szablon Windows / cofnij / redo i przełączanie profilu, nagrywanie skrótów, kopiowanie / wklej / reset, utrzymywanie ustawień, instalację / aktualizację, zmianę układu graficznego i rzeczywiste przechwytywanie UI. Hardware- specyficzne zachowanie nadal wymaga makro-pad i urządzeń audio na komputerze Windows.

<a id="profile-sound-files-and-workshop-safety"></a>
### Profilowe pliki dźwiękowe i bezpieczeństwo warsztatów

Eksport profili obejmuje wymienione klipy MP3 / WAV (do 32 MiB audio na pakiet). Import kopii wbudowane audio do DeckPilotjest zarządzany folder Sounds i identyfikatory remap bez wymiany istniejących klipów. Automatyczne kopie zapasowe profilu używają tego samego formatu; niedostępny dźwięk jest rejestrowany jako brakuje, a nie po cichu zastąpiony. Zachowaj pliki źródłowe, jeśli przekroczysz limit eksportu. Pliki profilu Legacy pozostają obsługiwane. Usuwanie klipu, który jest nadal przypisany do akcji krótkiej lub długiej jest zablokowane.

Atelier akceptuje deski, które jeszcze nie odpowiadają na DeckPilot protokół, podczas sprawdzania lokalizacji USB, Caterina bootloader i ATmega32U4 podpis przed programowaniem. Każda sesja testowa zaczyna się od pustych wyników, a jej raport pozostaje związany z testowaną planszą. Zamknięcie aplikacji podczas flash lub reset jest zablokowane do momentu zakończenia pisania i weryfikacji.

<a id="live-microphone-latency"></a>
### Opóźnienie mikrofonu na żywo

DeckPilot 2.1.3 procesory na żywo audio w małych blokach, napędzanych wyrzutniami. 20 ms event-driven mikrofon przechwytywania i 40 ms bufory odtwarzania. Kolejka mikrofonu jest ograniczona do 100 ms i odrzuca starsze kompletne ramki, jeśli playback stalls; to pozwala uniknąć budowy sekund opóźnionej mowy. Są to ustawienia bufora oprogramowania, a nie mierzone end-to@-@ end gwarancji latency. Windows, mikrofon, VB-CABLE a aplikacja odbierająca przyczynia się również do opóźnienia. Ustawienia izolacji głosowej nie ulegają zmianie i mogą tłumić klipy płyt dźwiękowych, ponieważ otrzymują mieszany sygnał.

<a id="automatic-virtual-microphone-startup"></a>
### Automatyczne uruchamianie wirtualnego mikrofonu

DeckPilot 2.1.4 automatycznie aktywuje wirtualny mikrofon, gdy wybrany mikrofon fizyczny i VB-CABLE są dostępne. Działa to również w tle po systemie Windows signing- in kiedy **Zacznij od Windows** jest włączona. Brakujące urządzenia są ponownie testowane co pięć sekund. Zaprzestanie wirtualnego mikrofonu wyraźnie zapisuje automatyczne uruchomienie jako wyłączone; ponownie włączyć go w Soundboard panel. Klipsy są odtwarzane tylko na życzenie.
