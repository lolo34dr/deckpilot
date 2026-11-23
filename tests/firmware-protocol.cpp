#include "../firmware/DeckPilot/DeckPilotProtocol.h"
#include <cassert>
#include <string>
#include <iostream>
struct FakeSerial {
  bool connected = true;
  int space = 64;
  std::string output;
  operator bool() const = delete; // Prevent the blocking Arduino bool(Serial) pattern.
  bool dtr() const { return connected; }
  int availableForWrite() const { return space; }
  size_t write(const uint8_t* bytes, size_t count) {
    assert(count <= 32 && count <= (size_t)space);
    output.append((const char*)bytes, count); return count;
  }
};
static std::string drain(DeckPilotProtocol& p, FakeSerial& s, uint32_t now) {
  for (int i = 0; i < 12; ++i) p.tick(s, now);
  std::string result = s.output; s.output.clear(); return result;
}
int main() {
  DeckPilotProtocol p; FakeSerial s;
  p.command("DECKPILOT?", 0);
  assert(drain(p,s,0) == "DECKPILOT|1|promicro-32u4-16mhz|" DECKPILOT_FIRMWARE_VERSION "\r\n");
  p.command("DP2|HELLO|1234ABCD", 10);
  std::string hello = drain(p,s,10);
  assert(hello.find("DP2|INFO|1234ABCD|promicro-32u4-16mhz|" DECKPILOT_FIRMWARE_VERSION "|12|7\r\n") == 0);
  assert(hello.find("DP2|KEYS|1234ABCD|0|0\r\n") != std::string::npos);
  p.buttons(0x801);
  assert(drain(p,s,20) == "DP2|KEYS|1234ABCD|1|2049\r\n");
  p.command("DP2|PING|1234ABCD|42", 30);
  assert(drain(p,s,30) == "DP2|PONG|1234ABCD|42|30|1|2049\r\n");
  p.command("DP2|PING|DEADBEEF|43", 40);
  p.command("DP2|PING|1234ABCD|4294967296", 40);
  p.command("DP2|PING|1234ABCD|-1", 40);
  p.command("DP2|HELLO|not-valid", 40);
  assert(drain(p,s,40).empty());
  s.space = 0; p.buttons(0);
  for (int i = 0; i < 100; ++i) p.tick(s,50);
  assert(s.output.empty() && !p.readyForCommand());
  s.space = 7;
  assert(drain(p,s,50) == "DP2|KEYS|1234ABCD|2|0\r\n");
  s.space = 64;
  p.buttons(1); p.buttons(2); p.buttons(4);
  assert(drain(p,s,60) == "DP2|KEYS|1234ABCD|5|4\r\n");
  p.tick(s,7000); p.buttons(0);
  assert(drain(p,s,7001).empty()); // Heartbeat expiry stops unsolicited diagnostics.
  p.command("DP2|HELLO|ABCDEF01", UINT32_MAX - 100);
  assert(drain(p,s,UINT32_MAX - 100).find("DP2|INFO|ABCDEF01|") == 0);
  p.buttons(1);
  assert(drain(p,s,50).find("DP2|KEYS|ABCDEF01|1|1") == 0); // millis wraparound.
  s.connected = false; p.tick(s,60); s.connected = true; p.buttons(0);
  assert(drain(p,s,70).empty()); // Closing the host releases the session.
  p.command("DECKPILOT?",80);
  assert(drain(p,s,80).find("DECKPILOT|1|") == 0); // Old apps remain compatible.
  DeckPilotProtocol a, b; FakeSerial sa, sb;
  a.command("DP2|HELLO|AAAA0001", 0); b.command("DP2|HELLO|BBBB0002", 0);
  drain(a,sa,0); drain(b,sb,0);
  a.buttons(1);
  a.command("DP2|INPUT|AAAA0001", 30);
  assert(drain(a,sa,30).find("DP2|BUSY|AAAA0001\r\n") == 0 && a.keyboardMask() == 1);
  a.buttons(0); drain(a,sa,31);
  a.command("DP2|INPUT|AAAA0001", 32, false);
  assert(drain(a,sa,32) == "DP2|BUSY|AAAA0001\r\n");
  b.command("DP2|INPUT|AAAA0001", 33);
  assert(drain(b,sb,33).empty());
  a.command("DP2|INPUT|AAAA0001", 34); b.command("DP2|INPUT|BBBB0002", 34);
  assert(drain(a,sa,34) == "DP2|INPUT|AAAA0001\r\n");
  assert(drain(b,sb,34) == "DP2|INPUT|BBBB0002\r\n");
  a.buttons(1); b.buttons(1);
  assert(a.keyboardMask() == 0 && b.keyboardMask() == 0);
  assert(drain(a,sa,40) == "DP2|KEYS|AAAA0001|1|1\r\n");
  assert(drain(b,sb,40) == "DP2|KEYS|BBBB0002|1|1\r\n");
  sa.connected = false; a.tick(sa,41);
  assert(a.keyboardMask() == 0); // Disconnect with a held key must not replay it as HID.
  a.buttons(0); a.buttons(1);
  assert(a.keyboardMask() == 1 && b.keyboardMask() == 0);
  sb.space = 0; b.buttons(0); b.tick(sb,42); b.buttons(2); b.buttons(0);
  sb.space = 64;
  assert(drain(b,sb,43) == "DP2|KEYS|BBBB0002|2|0\r\nDP2|KEYS|BBBB0002|3|2\r\nDP2|KEYS|BBBB0002|4|0\r\n");
  b.buttons(4); drain(b,sb,44);
  b.command("DP2|PING|BBBB0002|42", 6040); // Arrival after lease expiry cannot restart the mode.
  assert(drain(b,sb,6040) == "DP2|LOST|BBBB0002\r\n" && b.keyboardMask() == 0);
  b.buttons(0); b.buttons(4);
  assert(b.keyboardMask() == 4);
  b.buttons(0);
  b.command("DP2|HELLO|BBBB0003", 6100); drain(b,sb,6100);
  b.command("DP2|INPUT|BBBB0003", 6101); drain(b,sb,6101);
  for (int i = 0; i < 17; ++i) b.buttons(i % 2 ? 0 : 8);
  assert(drain(b,sb,6102) == "DP2|LOST|BBBB0003\r\n" && b.keyboardMask() == 0);
  b.buttons(0); b.buttons(8);
  assert(b.keyboardMask() == 8);
  std::cout << "PASS hybrid firmware: two independent sessions, no duplicate keyboard events, queued edges, disconnect and expiry recovery, overflow detection\n";
  std::cout << "PASS firmware: legacy identity, correlated handshake/heartbeat, key state, invalid frames, backpressure, expiry, reconnect and clock wrap\n";
}
