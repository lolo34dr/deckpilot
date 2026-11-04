> 이것은 완전한 릴리스 노트의 자동 번역입니다. 영어 원본은 참조입니다.

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
