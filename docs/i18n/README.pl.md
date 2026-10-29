> Jest to automatyczne tłumaczenie kompletnego README. Angielski oryginał jest punktem odniesienia. Etykiety interfejsu aplikacji i komendy są dokładnie zachowane.

<p align="center">
  <img src="../../deckpilotbanner.png#gh-light-mode-only" alt="Baner DeckPilot" width="760">
  <img src="../../deckpilotlogo.png#gh-dark-mode-only" alt="Logo DeckPilot" width="112">
</p>

<h1 align="center">DeckPilot</h1>

<p align="center">
  Towarzysz Windows dla Twojego 12- Klucz do makropadu DIY.<br>
  Konfiguracja kluczy, programów startowych, nośników sterowania i profili przełączników.
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest">Pobierz najnowszą wersję</a>
  &nbsp;·&nbsp;
  <a href="#getting-started">Rozpoczynanie</a>
  &nbsp;·&nbsp;
  <a href="#development">Rozwój</a>
</p>

<p align="center">
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest"><img src="https://img.shields.io/github/v/release/lolo34dr/deckpilot?style=flat-square&amp;color=78b7a0" alt="Najnowsze wydanie"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe"><img src="https://img.shields.io/badge/Download-Windows_x64-0078D4?style=flat-square&amp;logo=windows" alt="Pobierz dla Windows x64"></a>
  <a href="https://github.com/lolo34dr/deckpilot/blob/main/LICENSE"><img src="https://img.shields.io/github/license/lolo34dr/deckpilot?style=flat-square" alt="Licencja MIT"></a>
  <a href="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml"><img src="https://github.com/lolo34dr/deckpilot/actions/workflows/windows.yml/badge.svg?branch=main" alt="Stan budowy systemu Windows"></a>
  <a href="https://github.com/lolo34dr/deckpilot/releases"><img src="https://img.shields.io/github/downloads/lolo34dr/deckpilot/total?style=flat-square&amp;color=70a7e8" alt="GitHub umożliwia pobieranie plików"></a>
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

**Nie są wymagane żadne dodatkowe zmiany sterownika ani oprogramowania firmowego.** Interfejs aplikacji jest obecnie w języku francuskim. Wybierz język powyżej dla pełnego przetłumaczonego README, w tym odniesienia technicznego i historii wersji. Tłumaczenia są generowane automatycznie; ten angielski oryginał jest referencją.

<a id="features"></a>
## Cechy

- **Kalibracja z przewodnikiem:** identyfikacja wszystkich 12 Przyciski fizyczne bez przełączania makro-pad.
- **Instalowany wybielacz aplikacji:** wyszukaj skróty w menu Start i katalog aplikacji Windows, w tym wpisy Microsoft Store. Ręczny wybór plików pozostaje dostępny.
- **Niestandardowe działania:** uruchamia aplikacje, otwarte foldery lub strony internetowe, wysyła skróty i media sterujące.
- **Bezpłatne klucze:** zachować F13- F24 dostępne dla skrótów skonfigurowanych bezpośrednio w grze lub innej aplikacji.
- **Profile lokalne:** tworzyć, duplikować, zmieniać nazwę, usunąć i wybrać profile. Kalibracja jest dzielona między profile.
- **Profile wykonane z papieru:** zacząć od szablonów Music, Productivity lub Creation, a następnie dostosować je. Przełącz na konkretny, następny lub poprzedni profil z klucza fizycznego.
- **Szybsza edycja:** kopiowanie / wycinanie / wklejanie / resetowanie pełnych kluczy, nagrywanie skrótów klawiszowych, wybieranie kolorów akcentów klawiszy i cofanie zmian.
- **Narzędzia Windows:** otwarty dźwięk, wyświetlacz, ustawienia Bluetooth i sieci, Task Manager, lub zablokować sesję.
- **Kompaktowy interfejs ciemny:** ikony wektorowe, ikony aplikacji tam, gdzie są dostępne, i subtelne animacje, które respektują preferencje animacji Windows.
- **Operacja w tle:** zachować DeckPilot w obszarze powiadomienia, z uruchomieniem w systemie Windows sign- in domyślnie włączone (może być wyłączone).
- **Automatyczne aktualizacje:** zatwierdzenie nowego wydania, a następnie niech DeckPilot Pobieranie, weryfikowanie, zastępowanie i ponowne uruchamianie się.

<a id="getting-started"></a>
## Rozpoczynanie

<a id="requirements"></a>
### Wymogi

- Okna 10 lub Windows 11, **x64**.
- Arduino Leonardo- kompatybilny makropad wysyłanie 12 różne klucze od **F13 do F24**.
- Połączenie danych USB.

Opublikowane pliki do pobrania zawierają runtime .NET. **Nie trzeba instalować .NET do użycia DeckPilot.**

<a id="install-and-launch"></a>
### Zainstaluj i uruchom

1. Pobieranie **[DeckPilot- Setup.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-Setup.exe)** i uruchom instalatora. Windows prosi o zgodę administratora na instalację do **Pliki programu\DeckPilot**.
2. Instalator dodaje DeckPilot do Windows **Instalowane aplikacje**, tworzy skrót Menu Start i domyślnie wybiera skrót pulpitu i opcje uruchamiania systemu Windows. Możesz wybrać te opcjonalne zadania.
3. Połącz makro-pad i otwórz DeckPilot ze skrótu.
4. Postępuj zgodnie z kalibracją od lewej do prawej strony **4 kolumny**, następnie w następnym wierszu, dla **trzy wiersze**. Każdy przycisk musi wysłać inny klawisz F13- F24.

Podczas modernizacji od pierwszego 4- układ wiersza, DeckPilot zachowuje profile / działania i żąda jednej nowej kalibracji dla skorygowanych pozycji. Instalacja nad istniejącym przenośnym ustawieniem zachowuje również ustawienia w `%APPDATA%\DeckPilot`; rzucić starą kopię, jeśli konfiguracja poprosi.

Przenośny [DeckPilot.exe](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot.exe) oraz [ZIP](https://github.com/lolo34dr/deckpilot/releases/latest/download/DeckPilot-win-x64.zip) pozostają dostępne dla kompatybilności. Do użytku przenośnego, przechowywać wykonywalny w osobnym folderze do zapisu.

Odinstaluj z systemu Windows **Instalowane aplikacje**. Deinstalacja usuwa program i jego skróty, zachowując profile osobiste tak, aby później można je ponownie zainstalować.

Pojedynczy plik wykonywalny pobiera swoje powiązane zależności do pamięci podręcznej .NET użytkownika przy pierwszym uruchomieniu, co może potrwać nieco dłużej. Opublikowane pliki wykonywalne są obecnie niepodpisane, więc Windows może wyświetlić ostrzeżenie SmartScreen.

<a id="configure-a-key"></a>
### Konfiguracja klucza

1. Wybierz klucz w siatce.
2. Wybierz akcję na prawym pasku bocznym.
3. Konfiguracja ustawień poniżej siatki i zapis. Przełączanie klawiszy lub profili również zapisuje ustawienia bieżącego klucza.

| Działanie | Zachowanie |
| --- | --- |
| Darmowy klucz | Przesyłki F13- F24 przez normalnie. Przypisz go bezpośrednio w grze lub aplikacji. |
| Stosowanie | Otwiera zainstalowaną aplikację lub ręcznie wybrany program lub skrót. |
| Katalog | Otwiera folder w systemie Windows Explorer. |
| Strona internetowa | Otwiera adres HTTP lub HTTPS w domyślnej przeglądarce. |
| Skrót klawiszowy | Wysyła kombinację kluczy do aktywnego okna. |
| Media | Kontroluje odtwarzanie, utwory, głośność lub wyciszenie. |

W przypadku akcji aplikacji otwórz program i wyszukaj go po nazwie. Ręczny selektor plików pozostaje dostępny, z opcjonalnymi argumentami programu w zaawansowanych opcjach. Wprowadź argumenty zgodnie z składnią aplikacji docelowej.

Trzymanie przycisku uruchamia skonfigurowaną akcję tylko raz do czasu wydania. W trybie free- key, normalne zdarzenia powtarzające klawiaturę pozostają dostępne.

<a id="manage-profiles"></a>
### Zarządzanie profilami

Otwórz sekcję zarządzania profilem na prawym pasku bocznym, aby utworzyć, skopiować, zmienić nazwę lub usunąć profil. Użyj selektora powyżej siatki klawiszy, aby wybrać aktywny profil.

Kalibracja ma zastosowanie do wszystkich profili i może być powtórzona z paska bocznego.

<a id="background-operation"></a>
### Operacja w tle

Zamknięcie schowka okna DeckPilot jednocześnie zachowując aktywne działania. Double- kliknij ikonę obszaru zawiadamiania w pobliżu zegara, aby ją ponownie otworzyć, lub kliknij prawym przyciskiem myszy ikonę i wybierz polecenie zakończenia, aby całkowicie ją zatrzymać.

Uruchomienie systemu Windows jest domyślnie włączone dla bieżącego użytkownika. **Lancer avec Windows**; późniejsze aktualizacje aplikacji zachowują ten wybór. Instalacja i późniejsze uruchamia odświeżanie włączonego wpisu do bieżącej ścieżki wykonywalnej.

<a id="installed-applications"></a>
## Instalowane aplikacje

Picker odczytuje skróty z menu bieżącego użytkownika i systemowego, a następnie uzupełnia je o `Get-StartApps` wyniki z katalogu aplikacji Windows.

Szukaj ignoruje przypadek i akcent. Duplikaty nazwy są zgrupowane, przy czym wpisy w menu Start mają pierwszeństwo. Przenośne aplikacje i programy bez zarejestrowanych skrótów mogą być nadal wybierane ręcznie. Jeśli katalog Windows jest niedostępny, dostępne są czytelne skróty w menu Start.

Windows uruchamia wybrane skróty. `shell:AppsFolder`. Opcjonalne argumenty odnoszą się do programów opartych na plikach i skrótów; są one ukryte dla wpisów AppsFolder.

<a id="automatic-updates"></a>
## Automatyczne aktualizacje

Dostępne od **0.2.0**, automatyczne kontrole aktualizacji uruchomić przy starcie po kalibracji, a następnie co sześć godzin. Mogą być wyłączone w pasku bocznym. Kontrola manualna pozostaje dostępna.

Kiedy zostanie znaleziona nowsza stabilna wersja GitHub, DeckPilot Pyta, czy uaktualnić. **Zainstalowane kopie** pobierz i SHA-256 weryfikuj `DeckPilot-Setup.exe`, następnie uruchomić instalatora. Windows prosi o zgodę administratora na aktualizację plików programu. Instalator zamyka DeckPilot Z wdzięcznością, gdy jest gotowy, zachowuje profile i preferencje startowe, instaluje nową wersję i ponownie uruchamia aplikację. Anulowanie polecenia zgody systemu Windows pozostawia aplikację otwartą. Setup odmawia przerwania otwartej sesji oprogramowania firmowego.

**Kopie przenośne** zachowuje istniejący mechanizm aktualizacji. Po potwierdzeniu:

1. Pobieranie pliku wykonywalnego z obsługą raportowania postępu i anulowania.
2. Weryfikuje jego SHA-256 hash again `SHA256SUMS.txt` z tego samego wydania GitHub.
3. Oszczędza ustawienia i przygotowuje pomocnik aktualizacji.
4. Wychodzi, zastępuje wykonywalny i ponownie uruchamia.
5. Utrzymuje poprzedni wykonywalny dopóki nowy proces nie potwierdzi udanej inicjalizacji; jeśli startup zawiedzie, przywraca i ponownie uruchamia poprzednią wersję.

Wybór do aktualizacji późniejsze pocztówki automatyczne prompts dla 24 Kontrole ręczne omijają to przesunięcie.

Pliki do pobrania używają HTTPS, z przekierowaniem ograniczonym do oczekiwanych hostów GitHub. Pliki tymczasowe są przechowywane w `%LOCALAPPDATA%\DeckPilot\Updates`; pliki do pobrania starsze niż siedem dni są czyszczone. Wpis startowy systemu Windows nadal używa tej samej ścieżki wykonywalnej po aktualizacji.

Kopie przenośne wymagają zapisu osobistego folderu. Zainstalowane kopie używają instalatora dla chronionych katalogów; DeckPilot sama aplikacja nadal działa bez praw administratora.

**Aktualizacja z 0.1.x?** Zakończ starą wersję z obszaru powiadomienia i ręcznie zastąp wykonywalny najnowszym wydaniem raz. Te starsze wersje nie zawierają updater. Późniejsze aktualizacje mogą być zainstalowane z wewnątrz DeckPilot.

<a id="repository-rename-compatibility"></a>
### Kompatybilność repozycyjnej zmiany nazwy

Wersja **1.0.1** przygotowuje ruch z `lolo34dr/streamdeck-diy` do `lolo34dr/deckpilot`Repozytorium jest teraz `lolo34dr/deckpilot`. Aktualizacja kontroli użyj nowej nazwy i zachowaj zgodność z oryginalną nazwą. Udostępnianie pobrań jest walidowane na podstawie tych dwóch dokładnych ścieżek repozytorium.

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

Zainstaluj **.NET 8 SDK**, wersja 8.0.415 lub nowsza kompatybilna wersja dozwolona przez `global.json`. Visual Studio 2022 z funkcją rozwoju .NET desktop jest opcjonalny.

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

Konsola sprawdza wyjście ze statusem niezerowym przy pierwszym nieudanym stwierdzeniu.

- Mapowanie kluczy, powtarzanie klawiszy held-key, wydawanie, przełączanie profilu i kopiowanie akcji.
- Walidacja ustawień, trwałość i migracja ze starszych konfiguracji.
- Wyszukiwarka aplikacji i obsługa celów.
- Wydanie wyboru, sumy kontrolne, okrojone lub uszkodzone pliki do pobrania, przekierowanie i anulowanie.
- Wykonalne zastępstwo, zapasowe zabezpieczenie i zwroty.

Sprawdzenie blokady plików odbywa się tylko w systemie Windows.

GitHub Actions również działa `tests/windows-update-smoke.ps1` w stosunku do pliku wykonywalnego standalone. Otwiera Helper aktualizacji, wycofuje oryginalny proces, weryfikuje potwierdzenie wymiany i uruchamiania, sprawdza zachowanie ustawień, a następnie otwiera zainstalowany picker aplikacji poprzez Automation UI i zapisuje wybór. Wykonuje również zmiany nazwy plików wykonywalnych i ścieżek zawierających spacje. Skrypt ten jest zarezerwowany dla jednorazowego konta testowego GitHub Actions. `tests/windows-installer-smoke.ps1`: prawdziwa instalacja plików programu, skróty w menu pulpitu / Start, domyślne startup, uaktualnienie przy użyciu aplikacji otwartej, wyłączona konserwacja startup i nieinstalacja bez usuwania profili.

<a id="publish-a-standalone-windows-folder"></a>
### Opublikuj samodzielny folder Windows

```powershell
dotnet publish src/DeckPilot.Windows -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o artifacts/win-x64
Compress-Archive -Path artifacts/win-x64/* -DestinationPath artifacts/DeckPilot-win-x64.zip -Force
```

Dystrybucja kompletnego archiwum. System Windows build workflow produkuje również ten pakiet.

<a id="publish-a-github-release"></a>
### Opublikuj wydanie GitHub

Aktualizuj wersję aplikacji i `.github/release-notes.md`, then push a matching `v*` tag, takie jak `v1.0.0`. The release workflow checks that the tag means the project version and runs walidation before publishing:

| Pobieranie | Spis treści |
| --- | --- |
| `DeckPilot.exe` | Self- contained, single- file Windows x64 application. |
| `DeckPilot-win-x64.zip` | Kompletny samodzielny folder aplikacji. |
| `SHA256SUMS.txt` | Sha-256 hashs for both downloads. |

Dla systemu Windows ARM64, publikować oddzielnie `-r win-arm64` i potwierdzić na maszynie docelowej.

<a id="hardware-checks-on-windows"></a>
## Sprawdzanie sprzętu w systemie Windows

Z podłączonym Leonardo:

- Kalibracja wszystkich 12 pozycje w arbitralnej kolejności F13- F24. Potwierdź, że duplikaty klawiszy są odrzucane i anulowanie zachowuje poprzednie mapowanie.
- Przypisz darmowy klucz w grze i sprawdź, czy gra otrzymuje go normalnie.
- Uruchom skonfigurowany program, a następnie przytrzymaj przycisk: powinien uruchomić raz do wydania. Dołącz ścieżki zawierające spacje i opcjonalne argumenty.
- Przetestuj folder, stronę internetową, skrót w zwykłej aplikacji i sterowanie mediami.
- Wyślij F13- F24 za pomocą skrótu i potwierdź, że nie ma pętli DeckPilot działania.
- Tworzenie dwóch profili z różnych działań, przełączanie między nimi, zakończenie i wznowienie. Profile i kalibracja powinny trwać.
- Zamknąć okno, uruchomić akcję, a następnie ponownie otworzyć i zakończyć przez ikonę obszaru zgłoszenia. DeckPilot Wyjścia, wszystkie klucze powinny przejść normalnie.
- Włącz uruchamianie i testowanie systemu Windows po zalogowaniu się ponownie; wyłączyć go później, jeśli jest niechciany.

<a id="current-limitations"></a>
## Bieżące ograniczenia

- Klucz odsłuchowy jest globalny dla sesji Windows. Fizyczne klawisze F13- F24 z innej klawiatury podążają za tą samą konfiguracją. Zdarzenia klawiszowe wstrzyknięte przez Software- są ignorowane, aby zapobiec pętlom akcji.
- Niektóre gry, systemy antyoszustw lub programy za pomocą bezpośredniego wejścia urządzenia mogą nadal otrzymywać przechwycone klucze. Tryb Free- key pozostaje dostępny. Sygnał w ekranie, bezpieczny pulpit i inne sesje są poza kontrolą aplikacji.
- Windows może blokować skróty wysyłane do programów uruchomionych z uprawnieniami administratora. DeckPilot działa bez praw administratora i nie omija tego ograniczenia.
- Uwolnić Ctrl, Alt, Shift i Windows przed uruchomieniem działania skrótu. Kombinacje takie jak Alt + Tab nadal podlegają zachowaniu systemu Windows.
- Detekcja odłączenia, automatyczne profile pergaminowe i podpisany instalator to przyszłe ulepszenia. Profile są obecnie wybierane ręcznie.

<a id="project-structure"></a>
## Struktura projektu

| Ścieżka | Cel |
| --- | --- |
| `src/DeckPilot.Core` | Ustawienia, stan klucza, wyszukiwanie, walidacja wydania GitHub, zweryfikowane pobieranie i transakcje wymiany. |
| `src/DeckPilot.Windows` | Interfejs WPF, katalog aplikacji, ikony, działania w systemie Windows, integracja obszaru powiadomień i aktualizacja helper. |
| `tests/DeckPilot.Checks` | Kontrole wykonywane bez zewnętrznych ram testowych. |
| `tests/windows-update-smoke.ps1` | Native Windows update and application- picker smoke test used by CI. |
| `deckpilotlogo.png` | Logo aplikacji. |
| `deckpilotbanner.png` | Baner projektu. |

<a id="license-and-credit"></a>
## Licencja i kredyt

DeckPilot jest uwalniany w ramach **[Licencja MIT](../../LICENSE)**. Możesz używać, kopiować, modyfikować i redystrybuować go, w tym komercyjnie, tak długo jak zachowasz prawa autorskie i licencje wymagane przez licencję.

If you build on DeckPilotTen dodatkowy kredyt jest nieobowiązkowy; nadal wymagane jest utrzymanie ogłoszeń MIT.

Proponowany kredyt:

> Na podstawie [DeckPilot](https://github.com/lolo34dr/deckpilot) przez [lolo34dr](https://github.com/lolo34dr).

<a id="macropad-firmware-updates"></a>
## Aktualizacje oprogramowania Macropad

DeckPilot 1.1.0 zawiera **Firmware du macropad…** okno dialogowe i oprogramowanie firmowe 1.0.0 dla Pro Micro ATmega32U4 **5 V / 16 MHz**, przy użyciu konfiguracji Leonardo / Caterina potwierdzonej przez właściciela dziennika wysyłania. Narzędzie do migania w pakiecie Windows nie wymaga Arduino IDE. Wybór urządzenia i potwierdzenie są obowiązkowe; aktualizacje aplikacji nigdy po cichu nie świecą planszą. Istniejące mappingi, profile i kalibracja F13- F24 pozostają kompatybilne.

**Właściciel potwierdził udaną migawkę w aplikacji, oprogramowanie firmowe 1.0.0 identyfikacja i wszystkie 12 przyciski na tablicy odniesienia.** Zachowaj oryginalny szkic i dostęp do RST / GND do odzysku. Nie można automatycznie obracać migawki firmware. Zobacz [dokumentacja firmware](FIRMWARE.pl.md) dla kompatybilności, walidacji, odzyskiwania, budowania kroków i licencji trzeciej strony. Przyciski Per- tryby USB i natywnych kontroli mediów nie są częścią tej wersji oprogramowania firmowego.

<a id="building-the-windows-installer"></a>
### Budowa instalatora systemu Windows

Po produkcji `artifacts/standalone/DeckPilot.exe` z wbudowanym pakietem oprogramowania firmowego, zainstalować Inno Setup 6 i uruchomić `./installer/build.ps1` z głównego repozytorium. Wyjściem jest `artifacts/installer/DeckPilot-Setup.exe`. CI buduje i testuje tego instalatora przed dołączeniem go do wydania. Instalator używa stabilnego ID aplikacji, więc przyszłe wersje uaktualniają tę samą instalację.

<a id="connection-dashboard-and-firmware-checks-130"></a>
## Kontrola podłączenia deski rozdzielczej i oprogramowania firmowego (1.3.0)

W **Votre macropad** karta pokazuje stan połączenia, wykryte oprogramowanie firmowe i wszelkie dostępne pakiety aktualizacji. **Connexion et diagnostic** Eksponuje wybór planszy po podłączeniu kilku Leonardos, stan łącza USB na żywo, fizyczne liczenie przycisków, preferencje powiadomień firmware- i naśladowalne podsumowanie diagnostyczne. Diagnostyka nie kopiuje akcji profilu lub seryjnych identyfikatorów USB. Monitor ponownie łączy się po odłączeniu i automatycznie oddaje swój port szeregowy, podczas gdy menedżer oprogramowania firmowego jest otwarty. 1.0.0 i bez ogniwa diagnostycznego.

Sprawdzanie oprogramowania firmowego w startupie jest domyślnie włączone. Po tym, jak sprawdzanie aktualizacji i kalibracji aplikacji nie wchodzi w grę, starsze zidentyfikowane oprogramowanie firmowe może skłaniać do otwarcia instalacji; opóźnianie przypomnienia o opóźnieniach 24 godziny. Nieznane deski i wielu niewybranych kandydatów nie są automatycznie wyświetlane. Wydawnictwa firmware są połączone z aktualizacjami aplikacji z GitHub, a nie pobierane z niezależnego kanału firmware.

Oprogramowanie firmowe **1.1.1** dodaje identyfikację sesji, skorelowane odpowiedzi na bicie serca, ograniczone komunikaty i fizyczne zdarzenia stanu key-. Zdarzenia te animują mapowane przyciski i aktualizują diagnostykę; nigdy nie powołują się na działania po raz drugi. Jego pierwsza walidacja płyt fizycznych jest nadal wymagana, chociaż wcześniejsze oprogramowanie firmowe 1.0.0 i flash procedury zostały potwierdzone przez właściciela. Zobacz [dokumentacja protokołu i zgodności](FIRMWARE.pl.md).

<a id="keypad-interface-140"></a>
## Interfejs klawiatury (1.4.0)

Im większy 3 × 4 klawiatura śledzi fizyczną obudowę i czarne nasadki klawiszy. Każdy klucz pokazuje swoją główną ikonę, etykietę, mapowanie F13- F24 i małą ikonę typu action- type; wskazówki o narzędziach zachowują pełną etykietę i pozycję. Naciśnij informacje zwrotne, działania, skuteczne wysyłanie i błędy mają odmienny wygląd, w tym symbole, a nie tylko kolor. Animacje podążają za preferencjami animacji systemu Windows.

Wybierz klucz do edycji. **Options avancées · fichier et arguments** utrzymuje ręczne ścieżki aplikacji i argumenty startowe zawalone domyślnie, podczas gdy podstawowe foldery i cele strony internetowej pozostają rozszerzone. **Tester l’action** zapisuje i wysyła wybrane działanie na bieżącym komputerze PC; wskaźnik sukcesu potwierdza wysyłkę, a nie ukończenie wewnątrz innej aplikacji. Skróty klawiszowe mają na celu aktywne okno. Darmowe klawisze F13- F24 nie mają działania testowego.

Kiedy makropad jest odłączony, **Hors ligne** i towarzysząca podpowiedź wyjaśnia, że profile mogą być wciąż edytowane i testowane. Konfiguracja jest przechowywana na komputerze PC i pozostaje dostępna przez cały czas rozłączania i ponownego łączenia. Te usprawnienia interfejsu działają z istniejącym oprogramowaniem firmowym i nie wymagają dodatkowego błysku.

Menu obszaru zgłoszeniowego włącza teraz bezpośrednio profile. **Ctrl + S** Zapisuje bieżącą konfigurację przycisku. Nieprawidłowe pliki konfiguracyjne są kopiowane zanim zostanie użyta świeża konfiguracja. Instalacja aplikacji, ikony menu desktop / Start, preferencje startowe i istniejący automatyczny updater są zatrzymywane.

Do ręcznego przesyłania danych IDE Arduino, **zrezygnować DeckPilot z menu tacki** aby uwolnić port szeregowy. Zamknięcie głównego okna utrzymuje go w tle.

<a id="spotify-action-search-and-drag-and-drop-150"></a>
## Spotify, wyszukiwania akcji i drag- and- drop (1.5.0)

**Spotify** jest oddzielony od **Multimédia**. Jego odtwarzanie / pauza, następne i poprzednie polecenia adresują sesję mediów Windows w aplikacji Spotify Desktop, nawet gdy przeglądarka gra na YouTube. Spotify musi być otwarta i mieć dostępną sesję odtwarzania; uruchom raz utwór w razie potrzeby. Spotify w przeglądarce nie jest obsługiwane. Brakujące lub odrzucające sesje Spotify wywołują błąd, bez odwrotu do globalnych klawiszy medialnych lub innego gracza. Brak upoważnienia konta Spotify, subskrypcja Premium lub zapisany token jest potrzebny przez DeckPilot. Okna 10 wersja 2004 lub później, lub Windows 11, jest wymagane przez tę budowę.

Stosowanie **Rechercher une action ou une application** w przypadku zapytań takich jak: `volume`, `skip`, `Spotify` lub nazwa zainstalowanej aplikacji. Wskaźnik instalacji aplikacji ładuje się przy pierwszym wyszukiwaniu i jest buforowany przez pięć minut; ręczne wybieranie plików pozostaje dostępne. Kliknij wynik, aby skonfigurować wybrany klucz lub przeciągnąć go na inny klucz.

Przeciągnij akcję z katalogu lub wyników wyszukiwania na klucz, aby ją przypisać. Przeciągnij jeden skonfigurowany klucz na inny **wymieniają się swoimi działaniami**; fizyczna kalibracja pozostaje bez zmian. Możesz również upuścić jedną `.exe`, `.lnk`, `.appref-ms`, folder lub HTTP (S) URL na klucz. Upuszczanie tylko konfiguruje klucz i nigdy nie wykonuje działania. Ukończ brakujące ustawienia w edytorze.

Uruchomienie systemu Windows utrzymuje teraz wyraźną preferencję dla użytkownika, naprawia nieświeży lub brakujący wpis Run, a także informuje o znanych flagach uruchamiania systemu Windows. Normalne uruchamia z poszanowaniem wyłączenia systemu Windows; wyraźnie umożliwia **Lancer avec Windows** oczyszcza znane DeckPilot dezaktywować flagę dla bieżącego użytkownika. Wyrzutnie tła pozostają ukryte nawet przed kalibracją, a duplikaty wystartuje po cichu. Po aktualizacji otwórz **Préférences de l’application**, sprawdzić status startowy i ponownie aktywować opcję, jeśli Windows zablokował go. Prawdziwe sign- out / sign- in na komputerze użytkownika pozostaje ostateczna kontrola polityki Windows lub ograniczeń specyficznych dla maszyn.

<a id="sharp-keypad-rendering-profile-recovery-and-audio-controls-161"></a>
## Redukcja klawiatury Sharp, odzysk profilu i sterowanie dźwiękiem (1.6.1)

Klawiatura renderuje teraz na natywnych rozmiarach WPF zamiast skalowania tekstu przez Viewbox. Efekty cieni rodzicielskiej / kluczowej oraz animacje skali tekstu zostały usunięte; mniejsze okna przewijają się raczej niż kurczą klawiaturę. Aplikacja deklaruje świadomość DPI monitora i używa większych ikon powłoki systemu Windows do aplikacji na pulpicie.

**Annuler / Rétablir** Przywraca edycje profili, w tym swapy drag- and- drop, ustawienia akcji, import i tworzenie / usuwanie profilu. Historia utrzymuje 50 edytuje podczas sesji aplikacji. Ctrl + Z / Ctrl + Y działa na profilach poza polami tekstowymi; pola tekstowe zachowują swoje własne cofnięcie / redo. Kalibracja, preferencje startowe i wykonywane działania nie są cofane.

Pod **Gérer les profils**, eksport bieżącego profilu lub wszystkich profili jako `.deckpilot.json`, importować profile jako nowe kopie, lub przywrócić kopię zapasową wszystkich profili. Import nigdy nie wykonuje działań i nigdy nie zastępuje kalibracji komputera PC. Profile zawierają ścieżki działania, argumenty i identyfikatory audio punktów końcowych, które mogą wymagać dostosowania na innym komputerze PC. Nieznane formaty, przerośnięte pliki i nieprawidłowo ukształtowane konfiguracje gestów są odrzucane.

Przed zapisaniem zmienionych profili, DeckPilot utrzymuje poprzednie profile w `%APPDATA%\DeckPilot\Backups` (20 Wersja, content- deduplicated). Tylko preference- rates nie tworzy kopii zapasowych profilu. Błąd kopii zapasowej zapobiega nadpisywaniu aktualnych ustawień. Przywracanie zastępuje profile po potwierdzeniu i może zostać cofnięte; kalibracja pozostaje bez zmian.

**Microphone Windows** Włącza wyciszenie wszystkich aktualnie aktywnych punktów końcowych przechwytywania systemu Windows. Pasek stanu i karta Audio Windows pokazują aktywny / zmiksowany / mieszany / niedostępny status, odświeża się co dwie sekundy i po poleceniach aplikacji. Nieudane próby aktualizacji wielopunktowej w celu przywrócenia poprzednich stanów niemych. Wskaźniki te opisują stan końcowy systemu Windows, a nie przełącznik sprzętowy lub własny przycisk niemowy aplikacji.

**Changer de sortie audio** Zastępuje dwa wybrane, połączone urządzenia wyjściowe, dla konsoli Windows, multimediów i komunikacji domyślnych. Brak celu daje błąd bez wyboru innego urządzenia. Aplikacje z ich własnym wyjściem stacjonarnym nie mogą być zgodne z domyślnym Windows. Importowane identyfikatory punktów końcowych są wyświetlane jako niedostępne do czasu dostosowania do obecnego komputera PC. Nie powiodła się próba zmiany trasy przywracania poprzednich domyślnych.

Konfigurowanie **appui court / appui long**, najpierw wybierz skonfigurowane działanie, włącz opcję działania dwa, wybierz gest do edycji i ustawić próg z 200 do 3000 MS. Bez tego trybu działania zachowują natychmiastowe zachowanie klawisza. Z nim krótkie akcje przebiegają po zwolnieniu przed progiem; długie akcje przebiegają raz na progu (lub po zwolnieniu, jeśli zegar jeszcze nie działa). Długa akcja nigdy nie uruchamia się także w krótkim. Wolne klawisze F13- F24 nie mogą używać trybu dual- action. Oczekiwane gesty są anulowane na zmianie profilu / konfiguracji, kalibracji, operacji firmware, zmian sesji i zawieszenia. Nie jest potrzebne oprogramowanie firmware flash; istniejący key- down / key- up raporty klawiatury dostarczają gestów.

Kontrole podstawowe obejmują granice powtarzania / zwalniania, jednoczesne przytrzymywanie, anulowanie, historię profilu / wymiana / kopie zapasowe i wrzuty audio przy użyciu fałszywych punktów końcowych. Sprawdzanie UI systemu Windows wykonuje rzeczywiste przeciążenie cofania / redo, konfigurując długie działanie i próg, native keypad wymiary po zmianie rozmiaru i wskaźnik mikrofonu. Rzeczywiste sterowniki mikrofonu, headset / głośnik przełączanie i uchwyty fizyczne nadal wymagają walidacji na komputerze właściciela.

<a id="search-and-ci-reliability-fixes-164"></a>
## Wyszukiwarka i niezawodność CI (1.6.4)

Równoważne wyniki wyszukiwania zachowują swoje istniejące przyciski WPF. Katalog instalalled- app i debuunce timer defer result result result while a search short is capture or a drag is in progress. To zapobiega usuwaniu przycisku źródłowego podczas interakcji.

Kontrola dymu w systemie Windows nadal wykonuje natywne przeciągnięcia myszy i sprawdza zapisane zadania, swapy, cofnięcie / redo i oba gesty prasowe. Klient testowy używa fizycznych współrzędnych kursora, aby dopasować się do granic Automatyzacji UI niezależnie od DPI, skupia się przed pomiarem i skanuje wszystkie wartości procentowe przewijania. Sprawdzenie regresji sprawdza również, czy równoważne przeszukiwania zachowują ten sam dostępny przycisk. Podręczne poświadczenia startowe czekają na załadowany uchwyt WPF i wymagają pięciu kolejnych obserwacji hidden- okiennych, podczas gdy aplikacja pozostaje przy życiu.

Zbuduj i wydaj Workflows za pomocą Node.js 24 działania. Kompilator Arduino pozostaje w CLI 1.2.2; CI pobiera swoje archiwum Windows z przypiętym Sha-256 suma kontrolna. Oprogramowanie firmowe i jego wersje łańcucha narzędzi nie ulegają zmianie.

Ustawienia odczytywane w testach Windows wyraźnie pozwalają odczytywać / pisać / usuwać udostępnianie i parse UTF-8 po zamknięciu czytnika. To zapobiega temu, że test sam w sobie odmawia wymiany plików atomowych aplikacji. Test historyczny utrzymuje stare zdjęcie otwarte w trzech cyklach cofania / ponownego wykonania i sprawdza, czy pozostaje nienaruszony podczas zmiany ustawień na żywo.

W systemie Windows istniejące pliki konfiguracyjne są oddane `File.Replace` a nie tryb nadpisarski `File.Move`Wspiera to czytniki, które pozwalają na usuwanie przy zachowaniu kompletnego starego migawki. Podstawowy apartament testuje trzy zastępstwa z czytnikiem utrzymywanym w otwartym; Kontrola rdzenia systemu Windows działa przed kompilacją oprogramowania firmowego i publikacją, tak więc błędy trwałości są wykrywane wcześnie.

Czytnik migawek testowych może czekać aż 500 ms dla Windows udostępniania / naruszenia blokady podczas `ReplaceFile` commits. Poczekanie dotyczy tylko otwarcia migawki, nigdy nie wykonywania lub ponownego wykonania działania aplikacji. Inne błędy, źle ukształtowany JSON i trwałe zamki pozostają niepowodzeniem.

<a id="runtime-and-editing-optimizations-170"></a>
## Optymalizacja czasu pracy i edycji (1.7.0)

Zapisywanie identycznych ustawień sprawdza bieżący plik, ale pomija pracę zamiany i tworzenia kopii zapasowych. Zewnętrzne edycje / usuwanie i nieskuteczne zapisu są nadal wykrywane. `ButtonAction` ma równość wartości (w tym długie działanie); edytor brudnych czeków uniknąć serializacji JSON. Historia utrzymuje oddzielone migawki modelu, waliduje nowe stany i unika parsowania JSON podczas cofania / redo. 50-edit limit i 20 Pozostają kopie zapasowe profilu.

Search and picker udostępniają niezmienny katalog instalalny- aplikacji na pięć minut (15 seconds for a partial result). Współbieżni konsumenci dzielą jeden skan; zamknięcie jednego pickera nie anuluje innego konsumenta. Odśwież obejście pamięci podręcznej, a każdy picker ma swój własny filtr / widok. Skanowanie i katalog parsowanie uruchomić z gwintu UI; picker filtrowanie jest obciążone przez 120 MS. Wyszukiwarka akcji ponownie sprawdza ten cache po jego porażce i po powrocie z picker, więc odświeżony katalog jest tam dostępny również.

Aktualizacje UI planszy teraz śledzić powiadomienia o połączeniach zamiast budzenia każdego 100 ms. Zamówione powiadomienia o odłączeniu nadal anulują oczekujące uchwyty. Haki Keyboard kolejki wizualne pracy, podczas supresji i gestu czasu pozostają synchroniczne. Ukryte / zminimalizowane okna zatrzymać animacje sprzężenia zwrotnego i używać dziesięciosekundowy interwał sondowania audio zamiast dwóch sekund; ponowne otwarcie lub wydanie komendy audio żąda świeży stan. W oczekiwaniu audio odświeża udostępniać pętlę i zawierać żądania przybywające podczas istniejącego czytania. Niezmienione migawki audio i zamrożone szczotki uniknąć powtarzających się przydziałów UI. Process wrapers zwracany przez uruchomienie aplikacji są usuwane bez zamykania uruchomionego programu.

Zmiany te zmniejszają określoną pracę; nie tworzą niezależnej od maszyn wartości procentowej procesora / pamięci RAM. Native hardware audio przełącznik i fizyczne zachowanie klucza nadal wymagają walidacji na komputerze z systemem Windows PC z makro pad.

<a id="workspace-tools-and-refreshed-interface-180"></a>
## Narzędzia do pracy i odświeżony interfejs (1.8.0)

Interfejs węglowy stawia bibliotekę działania na pierwszym miejscu, oddziela kontrolę profilu od chrupiącej native- size 3 × 4 klawiatura i grupy ustawień akcji w karcie. Kluczowe akcenty wyróżniają własne kategorie; wybór, ostrość klawiatury i informacje zwrotne akcji pozostają oddzielone. **Réduire les animations** w preferencjach uzupełnia ustawienia animacji systemu Windows. Mniejsze okna zachowują dostęp do przewijania zamiast kurczenia się tekstu klucza.

- **Szablony profilu:** poniżej **Gérer les profils**, Dodaj muzykę i dźwięk, pulpit i wydajność, lub Tworzenie. Każdy szablon tworzy osobny edytowalny profil i nigdy nie wykonuje swoich działań. Istniejące profile i kalibracja fizyczna są zachowane. Skróty klawiszowe używają aktywnej aplikacji, której wiązania mogą się różnić.
- **Klucze profilu:** przypisywanie **Changer de profil**, następnie wybierz nazwany profil lub cykl do przodu / do tyłu. Referencje pomiędzy profilami są remapped podczas importowania zestawu lub powielanie profilu samoodsyłającego. Brak miejsca docelowego zgłasza błąd i może być zastąpiony w edytorze.
- **Polecenia Windows:** do stosowania **Commandes Windows** aby otworzyć wspólne ustawienia lub Task Manager, lub wyraźnie zablokować komputer PC. Polecenia pochodzą ze stałej listy, bez skryptu do wprowadzenia. Akcja blokady wymaga podpisanie z powrotem do systemu Windows.
- **Komplete- klucz schowka:** ctrl + C / X / V i Delete pracy, podczas gdy klawisz pokładowy ma ostrość klawiatury; zwykłe pola tekstowe utrzymują swoje skróty edycji. Klawisz schowka pozostaje wewnątrz DeckPilot i jest oczyszczony przy wyjściu. Te edytuje wsparcie cofnij / redo.
- **Rejestrator skrótów:** wybierz akcję klawiatury i kliknij **Enregistrer un raccourci**. Naciśnij wspieraną kombinację, a następnie zapisać go. Anulowanie ucieczki; F13- F24 jest zarezerwowany dla makro pad podczas nagrywania.
- **Szybki dostęp:** Ctrl + K lub przycisk Biblioteka skupia wyszukiwanie akcji. **Activité récente** utrzymuje ostatni 30 wysłanie / nieudana akcja w pamięci dla tej sesji, bez zapisywania argumentów programu lub ścieżek docelowych. Można ją w każdej chwili wyczyścić. Udane wysłanie nie gwarantuje zakończenia w aplikacji docelowej.

Oprogramowanie firmowe pozostaje **1.1.1**. Akceptowanie automatycznej oferty oprogramowania firmowego teraz poprawnie otwiera menedżera oprogramowania firmowego; poprzednia ochrona modalna uniemożliwiła przekazanie go. Istniejąca tożsamość oprogramowania firmowego, bicie serca, ponowne podłączenie i kontrola flash pozostają na miejscu. Nowe funkcje przestrzeni roboczej nie potrzebują błysku firmware.

Walidacja obejmuje istniejące ustawienia, walidację akcentu, nawigację profilu i remapowanie odniesienia, przenośne szablony, natywny szablon systemu Windows / cofnij / redo i przełączanie profilu, nagrywanie skrótów, kopiowanie / wklej / reset, utrzymywanie ustawień, instalację / aktualizację, zmianę układu graficznego oraz faktyczne wychwytywanie UI. Zachowanie specyficzne dla użytkownika nadal wymaga makro-pad i urządzeń audio na komputerze z systemem Windows.
