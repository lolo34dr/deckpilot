> 이것은 완전한 릴리스 노트의 자동 번역입니다. 영어 원본은 참조입니다.

DeckPilot 2.2.0 사운드 보드 클립을 untouched 유지하면서 조정 가능한 로컬 마이크 소음 억제를 추가합니다.

- RNNoise 필터 선택된 물리적 마이크 전에 DeckPilot soundboard 클립에 혼합합니다. 필터를 우회하고 가상 마이크에서 명확하게 유지하십시오.
- 어떤 응용 프로그램을 선택 **CABLE Output** 마이크 게임 및 음성 채팅 애플리케이션과 같은 필터링 음성 및 사운드 보드 믹스를 수신합니다. 물리적 마이크를 직접 우회하는 앱 DeckPilot's 필터.
- Enable 또는 disable 필터링 및 설정 0–100% 강도 **Soundboard · micro virtuel**. 이 PC에 오디오 처리 체재; 굳힌모 섬광은 요구되지 않습니다.
- RNNoise native library는 Windows x64에 내장되어 있으며, 내장되어 있습니다. DeckPilot 그리고 그것의 설치자. 그것의 BSD 작풍 제3자 면허는 근원 저장소에서 포함됩니다.

더 많은 정보 10 ms RNNoise 프레임 크기는 end-to-end latency를 보장하지 않습니다. Windows, 오디오 장치 및 VB-CABLE 또한 지연에 영향을 미칩니다. 마이크 품질 및 대기 시간 테스트.
