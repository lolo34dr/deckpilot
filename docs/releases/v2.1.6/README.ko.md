> 이것은 완전한 릴리스 노트의 자동 번역입니다. 영어 원본은 참조입니다.

DeckPilot 2.1.6 · DeckPilot Atelier, 그리스 1.0.12 Atelier의 펌웨어 지우기 및 복구 가능.

- 파괴적인 펌웨어를 붕괴한 고급 작업으로 이동하고 태핑해야 합니다. `EFFACER` 작성하기 전에.
- 보드는 지우기 후에 키보드로 enumerating 것을 설명하고, 정확한 RST/GND 두 배 다시 놓는 회복 단계를 보여주고, 지우기 또는 중단한 섬광 후에 Atelier의 회복 활동을 이용할 수 있습니다.
- 펌웨어 유지 1.1.1; 아니 DeckPilot 하드웨어 펌웨어 변경이 필요합니다.

Validation에는 펌웨어 및 앱 체크, 고급 지우개 제어를위한 Windows UI 자동화, no-device 안전 및 RST / GND 복구 지침이 포함되어 있습니다. 리셋 자체는 여전히 물리적 보드가 필요합니다.

DeckPilot 2.1.5 softs audio-device는 새로 고침하고 상표가 붙은 시작 스크린을 추가합니다.

- 물리적 마이크와 발견 VB-CABLE UI 스레드에서. 자동 추적은 최근의 장치 스냅 샷을 공유; 수동 새로 고침 버튼은 항상 신선한 스캔을 수행합니다.
- 반복된 Windows audio-state polling를 감소시키고 일반적인 단청 마이크로폰 변환 경로를 간소화하십시오.
- 더 보기 DeckPilot 대화 형 앱이 시작되는 동안 배너. Windows와 함께 침묵하는 시작은 배경에 있습니다.
- Atelier는 남아 있습니다 1.0.11 펌웨어가 남아 있습니다. 1.1.1.

유효성 검사는 배경 검사/cache 회귀 체크, 오디오 변환 체크, Windows 구조, 설치자 향상/uninstall, 본래 splash/update 및 신청 구경 검사를 포함합니다. 실제적인 오디오 CPU 사용 및 대기권은 PC와 오디오 장치에 아직도 달려 있습니다.

DeckPilot 2.1.4 Windows Sign-in 후 배경 시작을 포함하여 앱으로 구성된 가상 마이크를 자동으로 시작합니다.

- 자동 사운드 보드 마이크 시작은 기존 및 새로운 설정을 위해 기본적으로 활성화됩니다. 그것은 선택된 물리적 마이크를 사용하고 VB-CABLE; 수동 활성화가 PC를 재시작한 후에 필요 없습니다 DeckPilot Windows로 시작하십시오.
- 장치가 로그인을 할 준비가되지 않은 경우, 또는 오디오가 차단된 후 중지됩니다. DeckPilot 자동 활성화가 활성화되는 동안 5 초마다 retries.
- 명시된 정지 비활성화 및 자동 시작 설정 저장; 다시 활성화 Soundboard 패널 또는 활성화 버튼. 오디오 클립은 자동으로 재생되지 않습니다.
- Atelier는 남아 있습니다 1.0.11 펌웨어가 남아 있습니다. 1.1.1.

Validation에는 설정 마이그레이션/라운드 트립 체크 및 기본 Windows UI 체크가 persisted automatic-start preference, 그리고 기존의 오디오 및 설치자 체크를 포함합니다. 물리적 장치 시작/리커넥션은 여전히 소유자의 하드웨어에 달려 있습니다.

DeckPilot 2.1.3 Soundboard 마이크 지연 시간을 줄이고 라이브 오디오 경로에서 1초 변환 블록을 제거합니다.

- 마이크와 사운드 클립에 대한 출력 구동, 작은 블록 샘플링을 사용합니다. 샘플 속도 우회는 완전히 재흡수합니다.
- 자주 묻는 질문 20 ms 캡처 및 40 ms 재생 버퍼 대신 이전 100 ms 설정. 실제 대기 시간은 Windows, 오디오 장치 및에 달려 있습니다. VB-CABLE.
- Bound 마이크 버퍼링 100 ms 과 유지 최근 완료 프레임 후 재생 이전 연설 대신 재생 스트래치. 짧은 underruns add only the silence required by the current read.
- Media Foundation One-second read-ahead없이 단일, 스테레오, 멀티 채널 및 Windows 확장 가능한 PCM / 플로트 입력을 지원합니다.
- Discord 처리를 유지하고 unchanged routing. Atelier는 남아 있습니다 1.0.11 펌웨어가 남아 있습니다. 1.1.1.

유효성 검사는 재 샘플링 블록 크기, 채널 변환, 확장 가능한 형식, 언런 복구, 오버 플로우 신선도, 동시 캡처 / 재생 및 클립 완료, 기존의 Windows 설치 및 응용 프로그램 테스트와 함께. 하드웨어 대기 상황 수치가 주장되지 않습니다; 당신의 마이크와 오디오 드라이버와 개선을 확인합니다.

DeckPilot 2.1.2 · DeckPilot Atelier, 그리스 1.0.11 작업장 안전, soundboard 신뢰성 및 휴대용 단면도 소리를 고치십시오.

- Atelier는 아직 대답하지 않는 레오나르도/Caterina 널을 준비할 수 있습니다. DeckPilot 프로토콜; 직렬 실패는 여전히 번쩍이고 USB 위치, bootloader 및 MCU 서명은 확인되었습니다.
- 모든 검사는 빈 결과로 시작합니다. 보고서는 테스트 보드 및 펌웨어를 유지하고 stale USB 콜백은 새로운 세션에 영향을 미치지 않습니다.
- 펌웨어 작성 또는 검증 중에 Atelier를 닫는 것은 차단됩니다.
- 단면도 수출과 자동적인 단면도 백업은 참고된 MP3/WAV 파일을 유지할 수 있습니다 (32 MiB 오디오 제한). 파일 복사가 내장 된 클립을 관리 폴더로 가져 와서 짧은 / 긴 동작 참조를 다시 맵합니다. 레거시 프로파일 가져 오기가 지원됩니다. 누락 된 백업 오디오는 명시적으로보고됩니다.
- Soundboard는 클립, 업데이트 재생 표시기를 완료하고, 중단 된 오디오 스트림을보고. 이름을 변경 VB-CABLE endpoints는 어댑터 정체성을 통해 인식됩니다.
- 정확한 번역 README 링크 및 번역 중 기술 장치 이름을 보존. Clarify VB-CABLE 제품 설명

유효성 검사, 펌웨어 프로토콜 테스트, 번역 테스트, Windows 빌드, DeckPilot 그리고 Atelier 설치/업그레이드/uninstall checks, 그리고 기본 인터페이스/업데이트 체크. 물리적인 첫번째 섬광 및 오디오 차단 행동은 아직도 소유자의 기계설비를 요구합니다. 펌웨어는 남아 있습니다 1.1.1; 굳힌모 갱신은 요구되지 않습니다.

DeckPilot 2.1.1 설치 및 업데이트 개선:

- 자주 묻는 질문 VB-CABLE 첫 번째 설치 및 업그레이드 및 다운로드 및 드라이버가 누락 될 때 공식 서명 설치 프로그램을 시작합니다.
- Windows 시작, 자동 업데이트 체크 및 설정 및 업데이트 중 감소 모션 환경 설정, 기존 선택 및 보존과 함께.
- 선택된 물리적 마이크를 유지하고 필요한 경우 마이크 설정만 요청하십시오.
- installer 초기화 및 기존 설치의 탐지를 수정합니다.

지원하다 **DeckPilot-설정.exe** 또는 업데이트 수락 DeckPilot. Windows 관리자 승인이 필요합니다. 기존 프로파일 및 교정이 보존됩니다. DeckPilot Atelier가 남아 있습니다. 1.0.10 펌웨어가 남아 있습니다. 1.1.1; 둘 다 공유된 방출에서 포함됩니다.

유효성: 핵심 검사 및 Windows 건축, 설치자 upgrade/uninstall 및 기본 신청 시험은 풀어 놓기 전에 실행합니다. 오디오 기계설비 행동은 아직도 사용자의 PC에 시험을 요구합니다.

DeckPilot 2.1.0 딥 블랙 인터페이스와 사용자 정의 색상을 유지하고 VB-Audio를 통해 사운드 보드를 루트 VB-CABLE 자동적으로. 사용자는 그들의 육체적인 마이크만 선정합니다; 사실상 재생 엔드포인트는 조정합니다 그래서 음성과 MP3/WAV 클립은 스피커에 실수로 보내질 수 없습니다. DeckPilot installer는 공식을 다운로드 할 수 있습니다 VB-CABLE 패키지는 Authenticode 게시자를 확인하고 공급업체의 드라이버 설정을 실행합니다. 패키지는 redistributed가 아닙니다. Discord 또는 게임에서, 선택 CABLE Output 마이크로서. Atelier는 버전에 남아 있습니다. 1.0.10.

DeckPilot 1.9.11 · DeckPilot Atelier, 그리스 1.0.10 사전 플래시 하드웨어 및 펌웨어 진단, 펌웨어 버전 비교, 중단 된 쓰기 후 가이드 복구 작업, 배치 보고서와 순차적 멀티 보드 번쩍이는, 및 모든 키 제어 보고서에서 물리적 위치 - F13 - F24 레이아웃 검사.

DeckPilot 1.9.11 공유 설치 프로그램에서 동일한 Atelier 업데이트가 포함되어 있습니다.

DeckPilot 1.9.10 모두 포함 된 공유 된 릴리스 복원 DeckPilot · DeckPilot Atelier, 그리스 1.0.9. 두 응용 프로그램은 다시 같은 최신 GitHub 릴리스를 확인합니다.

Atelier를 설치 한 경우 1.0.8 별도의 릴리스 채널에서, 설치 DeckPilot-Atelier-Setup.exe 이 릴리스에서 공유 업데이트로 돌아갑니다. Earlier Atelier 버전과 DeckPilot 기존 업데이 트를 사용할 수 있습니다.

DeckPilot Atelier, 그리스 1.0.7 모든 것을 정리하는 확인된 프로그램-area 지우개 추가 28 사용자 플래시의 KiB, 안전한 재설정 루프를 남겨, Caterina 및 EEPROM을 보존합니다. 재설정은 물리적 보드가 선택 될 때까지 비활성화 남아 있습니다.

DeckPilot 1.9.7 앱이 열리면서 Atelier 제거를 수정합니다. Atelier 1.0.6 또한 Windows DPI 표시를 수정하여 설치 된 앱을 실행에서 방지합니다.

DeckPilot 1.9.6 막힌 Windows DPI 표시를 수정 DeckPilot 임명 후에 오프닝에서 Atelier.

DeckPilot 1.9.5 Windows 릴리스 검사에서 상세한 Atelier 시작 진단이 포함되어 있습니다.

DeckPilot 1.9.4 Atelier Windows 시작 연기 테스트를 개선하여 애플리케이션 충돌 세부 정보를 보고합니다.

DeckPilot 1.9.3 Atelier 빌드 메타데이터를 수정하여 번들 펌웨어 아카이브에서 펌웨어가 나타납니다.

DeckPilot 1.9.2 Atelier 릴리스 포장 단계를 수정하고 빌드 진단을 향상시킵니다.

DeckPilot 1.9.1 Atelier installer 시작을 개선하고 자동 앱 및 펌웨어 업데이트 체크를 추가합니다.

- installer는 Windows 사용자 세션에서 Atelier를 시작하고 안전하게 업그레이드를 위해 /restarts를 중지합니다.
- Atelier는 시작 및 수요에 GitHub를 확인합니다. 그것은 새로운 Atelier 빌드 또는 번들 펌웨어를 발표 한 SHA-에 대한 설치 프로그램을 다운로드256 파일.
- Firmware는 사용자가 플래시 동작을 선택한 후에만 깜박입니다. 앱 업데이트는 자체에 연결된 보드로 작성하지 않습니다.

Atelier installer는 또한 신선한 설치 후에 서명한 사용자의 Windows 회의에서 발사합니다. 릴리스 워크플로우는 지금 설치된 앱을 열고, 그 동안 업그레이드 및 제거할 수 있습니다.

DeckPilot 1.9.0 더 보기 DeckPilot Atelier, 새로운 Macropads를 번쩍이는 별도의 Windows 작업장 app 및 모든 검사 12 키.

- 레오나르도 / Pro Micro ATmega32U4 보드에 포함된 펌웨어를 Caterina bootloader로 설치하십시오.
- 모든 물리적 키를 위한 짧은 압박과 긴 압박 체크를 실행하고, 그것의 F13–F24 매핑을 기록하고, 품질 관리 보고를 저장하십시오.
- 지원하다 **DeckPilot-Atelier-Setup.exe를 설치** 이 릴리스에서. 원래 DeckPilot 설정 및 펌웨어는 별도로 사용할 수 있습니다.

DeckPilot 1.8.0 새로 고침된 작업 공간과 더 빠른 방법을 추가하여 프로필을 작성하고 사용합니다.

- Clearer charcoal/teal 공용영역, 활동 도서관 첫째로, crisp 3 × 4 키, 키 악센트 색상 및 감소 모션 선호도.
- 3개의 편집 가능한 단면도 템플렛: 음악과 오디오, 탁상용 및 생산력 및 창조.
- 프로필 작업: 이름 프로필로 전환, 다음 또는 키에서 이전 프로필. 가져온된 프로필 세트는 내부 링크를 유지합니다.
- Windows 작업: 일반적인 설정 또는 작업 관리자를 열고, 또는 명시적으로 PC를 잠그십시오.
- Copy/cut/paste/reset 전체 키, 긴 압박과 색상을 포함하여, undo/redo와 app-local clipboard.
- 키보드 단축키를 직접 캡처; 취소 및 매크로 패드 키를 캡처 중에 남아.
- Ctrl+K 액션 검색 및 명확하게, 마지막의 세션 전용 역사 30 작업.
- 자동 펌웨어 프롬프트를 수정하여 펌웨어 관리자를 열 수 있습니다. 종료 제스처는 프로파일 변경에 따라 취소됩니다.

지원하다 **DeckPilot-설정.exe** 또는 앱에서 업데이트. 프로파일, 교정 및 시작 선호도가 보존됩니다. Firmware는 남아 있습니다. **1.1.1**; 이 특징을 위해 섬광 요구되지 않습니다.

Validation에는 핵심 겸용성/navigation/import/template 체크 및 기본 Windows 임명, 갱신, 피커, 끌기 및 하락, undo/redo, 템플렛 창조, 단면도 활동, 단축키 붙잡음, 클립보드 편집, 배치 및 극소화/반복 체크가 포함됩니다. 육체적인 macropad 및 오디오 장치 행동은 소유자의 Windows 기계설비에 아직도 달려 있습니다.
