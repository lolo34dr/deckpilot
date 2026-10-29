> 이것은 완전한 펌웨어 문서의 자동 번역입니다. 영어 원본은 참고입니다. 명령과 식별자는 정확히 보존됩니다.

<a id="deckpilot-firmware"></a>
# DeckPilot 펌웨어

시험된 기계설비 윤곽을 위해: **직업적인 마이크로 ATmega32U4, 5 ₢ 킹 16 MHz의**, 사용 **Arduino, 이탈리아** 보드 프로필 및 a **카메리나 / AVR109** bootloader. USB-C는 연결관을, 널 겸용성을 설명합니다. 소유자의 본래 sketch는 성공적으로 업로드되었습니다 1200- 보드 리셋 및 AVR109 57600 baud. 소유자는 또한 성공적인 인앱 번쩍이기, 펌웨어를 확인했습니다 1.0.0 모든 것의 식별 및 작동 12 그 참고 널에 단추.

<a id="wiring-and-behavior"></a>
## 배선 및 행동

각 스위치는 GND에 1개의 핀을 연결합니다. 내부 끌어당기는 것은 활성화됩니다. 핀 **2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16** 이름 * **F13를 통해 F24** 그 순서에서. 안정되어 있는 30 ms debounce는 압박을 자주적으로 취급하고 풀어 놓습니다. 키보드 행동, Windows 측 단면도 및 구경측정은 호환이 남아 있습니다; USB 키보드 rollover는 Arduino의 키보드 도서관에 의해 제한됩니다. 굳힌모는 per-button USB 형태 또는 본래 매체 통제를 아직 소개하지 않습니다.

핀 핀 **1** 시작에서 GND에 개최되는 키보드 작동, 원래의 실패를 보존. USB 코어는 여전히 bootloader 항목을 지원할 수 있습니다. GND에 VCC를 연결하지 마십시오.

현재 USB CDC 직렬 인터페이스 답변 `DECKPILOT?\n` (주) 115200 태그 :

```
DECKPILOT|1|promicro-32u4-16mhz|1.1.1
```

직렬 연결은 정상적인 키보드 가동을 위해 요구되지 않습니다. ID는 호환성 체크, 암호로 고쳐 쓰기 기계설비 입증입니다. Windows는 아직도 Arduino 레오나르도 USB 식별자를 보고합니다; 다른 많은 널은 그 식별자를 공유합니다.

<a id="install-from-deckpilot"></a>
## 설치하기 DeckPilot

1. 공급된 회복 sketch 유지 (`firmware/Original/Original.ino`) 및 복구 옵션으로 작업 Arduino 업로드 설정. RST 및 GND 물리적으로 액세스 유지.
2. Arduino IDE, 시리얼 모니터 및 기타 직렬 도구. 다른 레오나르도 보드를 분리합니다.
3. ᄋᄋᄋ **Firmware du macropad…**, 정확한 장치를 선택하고 널과 배선을 확인합니다. unidentified 본래 sketch는 급행 확인 후에 단지에 설치될 수 있습니다.
4. 설치를 확인하고 완료 될 때까지 연결된 USB 케이블을 남깁니다. 키보드 작업이 중단됩니다. DeckPilot 펌웨어 대화상자가 열리면서 일반 F13–F24 이벤트는 여전히 Windows에 도달합니다.

펌웨어 및 Windows AVRDUDE 8.0 게시된 앱 내에서 번들됩니다. Arduino IDE 또는 별도의 드라이버 설치가 수행되지 않습니다. DeckPilot. Windows는 이미 소유자의 성공적인 업로드를 위해 한대로 이사회의 USB 직렬 포트를 노출해야합니다. 새로운 펌웨어 버전은 GitHub의 앱 업데이트로 전달됩니다. 앱 업데이트 **아니다.** 자동적으로 플래시 하드웨어. 각 플래시는 확인을 요구합니다.

DeckPilot 체크 SHA-256, 인텔 HEX 기록 체크섬, 재시동력 존재, 오버랩 및 **28672-바이트** 응용 프로그램을 재설정하기 전에 경계. 그것은 런타임과 bootloader 포트에 걸쳐 물리적 USB 위치를 일치, 사용 AVR109 에 57600 baud는, AVRDUDE의 쓰기 검증을 요구하고, 그 후에 실행 펌웨어의 정체성과 버전을 검사합니다. 그것은 신관, EEPROM 또는 부트 로더를 쓰지 않습니다. 레오나르도 IDs만 `2341:8036` / `2A03:8036` 그리고 Caterina `2341:0036` / `2A03:0036` 허용됩니다. Windows가 일치하는 물리적 연결을 제공 할 수 없다면, 깜박임은 포트를 추측하는 것보다 오히려 차단됩니다.

번쩍이는 것은 원자가 아닙니다: 케이블 단선 또는 힘 손실은 신청 굳힌모를 남겨둘 수 있습니다. 자동적인 굳힌모 백업 또는 rollback가 없습니다. 필요한 경우에, Arduino IDE 세트를 가진 본래 sketch를 다시 로드하십시오 **Arduino, 이탈리아**. 널의 bootloader에 따라서, 간접적으로 연결 RST에 GND 두번 임시 bootloader 항구를 노출할지도 모릅니다; 그 항구가 유효하다 동안 올려주기하십시오. 손상된 bootloader는 ISP 기계설비를 요구하고 이 USB updater에 의해 고쳐질 수 없습니다. 다른 널 또는 시계 변종을 쓰지 마십시오.

마지막 완료된 AVRDUDE 프로세스 로그에 저장됩니다 `%LOCALAPPDATA%\DeckPilot\Firmware\last-flash.log`.

<a id="build"></a>
## (주)더스트

Windows에서 저장소 루트에서 Arduino CLI를 설치하십시오. **1.2.2** 그리고 .NET SDK 8, 다음 실행:

```powershell
./firmware/tools/build.ps1
dotnet build DeckPilot.sln -c Release -p:RequireFirmwareBundle=true
dotnet run --project tests/DeckPilot.Checks -c Release --no-build
```

스크립트 핀 Arduino AVR 코어 **1.8.6**, 키보드 **1.0.6**, 및 상류 AVRDUDE **8.0**. CLI 관리 패키지 사용 Arduino의 패키지 체크섬 검증; AVRDUDE 바이너리 / 소스 아카이브는 SHA-를 명시256 스크립트에 핀. `artifacts/firmware/DeckPilot-firmware.zip` HEX, 표, Windows 도구, 라이센스 및 대응 업스트림 AVRDUDE 소스 아카이브가 포함되어 있습니다. 출판 CI는 번들을 요구합니다. 앱을 실행할 수 없는 일반 개발자 빌드는 플래시할 수 없습니다.

이름 * 12- 버튼 스케치는 데이비드 매디슨 (David Madison)의 적응되었습니다. **Stream 저렴한** 프로젝트, 저작권 2018, MIT. 그것의 통보는 안으로 남아 있습니다 `DeckPilot.ino`. DeckPilot 저장소 MIT 라이선스를 사용할 수 있습니다. **AVRDUDE는 GPL-의 밑에 분리되는 소프트웨어입니다2.0-또는-later**, 그것의 공지/라이센스 및 소스 번들; 저장소 MIT 라이센스는 그 라이센스를 대체하지 않습니다. 참조 https://github.com/avrdudes/avrdude/tree/v8.0 업스트림 빌드 지침 및 의존성 통지.

<a id="firmware-111-and-usb-protocol-2"></a>
## 회사 소개 1.1.1 및 USB 프로토콜 2

회사 소개 1.1.1 동일한 F13–F24 키보드 매핑 및 핀을 유지하면서 읽기 전용 진단 링크를 추가합니다.1 시작 실패 안전. **소유자의 물리적 시험은 펌웨어를 덮었습니다. 1.0.0. 펌웨어 1.1.1 자동화된 프로토콜 테스트와 AVR 빌드를 가지고 있지만, 실제 보드에서 첫 번째 테스트를 필요로 합니다.** 단면도와 구경측정은 PC에 남아 있고 이 굳힌모 갱신을 위해 재창조될 필요가 없습니다.

새로운 앱은 연결된 카드를 식별하고, 직렬 포트가 열리고, 플러그를 해제 한 후 다시 연결하고, 펌웨어 버전이 시작 후 다시 연결됩니다. 펌웨어 업데이트는 앱 출시로 전달됩니다. 시작 체크는 설치된 앱에 포함 된 검증 된 번들과 연결 펌웨어를 비교합니다. 앱 업데이트는 GitHub에서 더 새로운 릴리스를 얻을 수 있습니다. 별도의 펌웨어 전용 다운로드 채널이 없습니다.

앱 세션 당 한 번 펌웨어 프롬프트를 감지했습니다. 24-일시 알림 지연. 알림은 아래에서 비활성화 될 수 있습니다. **Connexion et diagnostic**. 알려진 Arduino sketches, 분리 장치 및 주변 다중 카드 선택은 자동 깜박임 또는 업데이트 프롬프트를 트리거하지 않습니다. 펌웨어 설치는 항상 확인을 요구합니다. 펌웨어 관리자를 열고 시리얼 포트에 대한 모니터와 대기를 중단합니다. 대화 상자가 닫힌 후 다시 모니터링합니다.

**Arduino IDE의 매뉴얼 업로드를 위해, 종료 DeckPilot 첫 번째 트레이 아이콘에서.** 메인 창을 닫는 것은 숨기므로 연속 연결이 계속됩니다. DeckPilot 또 다른 시리얼 모니터는 동시에 동일한 포트를 사용할 수 없습니다. 또 다른 도구가 이미 포트를 소유하면, 응용 프로그램은 일반 키보드 키에 영향을 미치지 않고 문제와 retries를보고합니다.

새 소식 `DeckPilotProtocol.h` 동일한 구현은 다음과 같습니다. `tests/firmware-protocol.cpp`. 그것은 고정 크기 버퍼를 사용, 경계 입력 처리 및 사용 가능한 USB 전송 공간; 버튼 스캐닝은 호스트를 기다리는 동안 절대 지연되지 않습니다. 최신 종료 버튼 마스크는 역압 아래 석탄입니다. 매크로를 실행하는 명령이 없습니다, EEPROM을 작성, 키보드를 비활성화, 이 진단 프로토콜을 통해 프로파일 또는 플래시 펌웨어를 수정.

(주) 115200 LF (CRLF는 허용됩니다)를 가진 배드, 선 끝. 세션 토큰은 정확하게 8개의 위문자 hexadecimal 특성을 포함합니다. 모든 수는 불행한 소수입니다. 예:

```
PC:    DECKPILOT?
Board: DECKPILOT|1|promicro-32u4-16mhz|1.1.1
PC:    DP2|HELLO|1234ABCD
Board: DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.1|12|3
PC:    DP2|PING|1234ABCD|7
Board: DP2|PONG|1234ABCD|7|42000|9|2049
Board: DP2|KEYS|1234ABCD|10|1
```

`INFO` 광고안내 12 열쇠와 기능 조금: 1 = heartbeat/state 스냅 샷, 2 = key-state 이벤트. `PONG` 요청 카운터를 에코, 다음 milliseconds, state sequence 및에서 가동 시간을보고 12-bit 버튼 마스크. `KEYS` 순서와 가면을 보고하십시오; 조금 0 F13 및 비트 11 F24, 물리적 교정의 독립적 인. 세션 및 요청 카운터는 stale replies를 거부합니다. Sequence 비교 핸들 32-bit wraparound. Keys는 진단 데이터 만 남아 있습니다. 앱 작업은 기존의 키보드 경로를 통해 한 번 실행되므로 연속 이벤트는 두 번 동일한 작업을 수행 할 수 없습니다.

Heartbeat 요청은 일반적으로 2 초마다 전송됩니다. 일치하는 응답이 누락되면 앱 재연결 1.5 초; 굳힌모는 심장부 없이 6 초 후에 unsolicited 진단을 멈추거나 연속되는 연결이 닫을 때 멈춥니다. Firmware 1.0.0 실시간 키 진단 없이 레거시 정체 쿼리를 통해 지원됩니다. 이전 DeckPilot 앱은 여전히 펌웨어의 유산 정체성을 읽을 수 있습니다. 1.1.1.

소스 버전에서 `DeckPilotProtocol.h` 릴리스 표시판을 공급합니다. 지금 펌웨어 번들을 게시하면 현재 sketch/header가 포함되어 있습니다. `source/DeckPilot`, 회복 스케치 및 DeckPilot MIT 라이센스.

진단 링크 deliberately 용도 `Serial.dtr()` 더 많은 것 `bool(Serial)`: Arduino AVR 코어 1.8.6 연결하기 10 ms 지연에 `Serial_::operator bool()`. 읽기 DTR은 직접 각 키 스칸 사이클에 일시적을 추가하는 진단 경로를 유지합니다. 호스트 프로토콜 테스트는 implicit bool 변환을 삭제하므로이 차단 패턴은 공유 프로토콜 구현에 대한 통합이 불가능합니다.
