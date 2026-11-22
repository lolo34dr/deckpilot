> 이것은 완전한 릴리스 노트의 자동 번역입니다. 영어 원본은 참조입니다.

DeckPilot 2.1.3 Soundboard 마이크 지연 시간을 줄이고 라이브 오디오 경로에서 1 초 변환 블록을 제거합니다.

- 마이크와 사운드 클립에 대한 출력 구동, 작은 블록 리 샘플링을 사용합니다. 샘플 속도 bypass는 완전히 재 샘플링을 일치시킵니다.
- 자주 묻는 질문 20 ms 캡처 및 40 ms 재생 버퍼 대신 이전 100 ms 설정. 실제 대기 시간은 Windows, 오디오 장치 및 VB-CABLE.
- Bound 마이크 버퍼링 100 ms 과 유지 최근 완료 프레임 후 재생 이전 연설 대신 재생 스트래치. 짧은 underruns add only the silence required by the current read.
- Media Foundation One-second read-ahead 없이 모노, 스테레오, 멀티채널 및 Windows 확장 가능한 PCM/float 입력을 지원합니다.
- Discord 처리를 유지하고 unchanged routing. Atelier는 남아 있습니다 1.0.11 펌웨어가 남아 있습니다. 1.1.1.

유효성 검사는 재 샘플링 블록 크기, 채널 변환, 확장 가능한 형식, 언런 복구, 오버 플로우 신선도, 동시 캡처 / 재생 및 클립 완료, 기존 Windows 설치 및 응용 프로그램 테스트와 함께. 하드웨어 대기 상황 수치가 주장되지 않습니다; 당신의 마이크와 오디오 드라이버와 개선을 확인합니다.
