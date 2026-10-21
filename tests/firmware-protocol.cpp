#include "../firmware/DeckPilot/DeckPilotProtocol.h"
#include <cassert>
#include <string>
#include <iostream>
struct FakeSerial {
  bool connected = true;
  int space = 64;
  std::string output;
  operator bool() const { return connected; }
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
  assert(drain(p,s,0) == "DECKPILOT|1|promicro-32u4-16mhz|1.1.0\r\n");
  p.command("DP2|HELLO|1234ABCD", 10);
  std::string hello = drain(p,s,10);
  assert(hello.find("DP2|INFO|1234ABCD|promicro-32u4-16mhz|1.1.0|12|3\r\n") == 0);
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
  std::cout << "PASS firmware: legacy identity, correlated handshake/heartbeat, key state, invalid frames, backpressure, expiry, reconnect and clock wrap\n";
}
