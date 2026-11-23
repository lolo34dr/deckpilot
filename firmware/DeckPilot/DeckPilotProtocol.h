#pragma once
#include <stdint.h>
#include <stdio.h>
#include <string.h>

#define DECKPILOT_FIRMWARE_VERSION "1.2.0"
#define DECKPILOT_BOARD_ID "promicro-32u4-16mhz"

// No dynamic allocations, blocking serial reads or unconditional serial writes.
class DeckPilotProtocol {
  char token[9] = {};
  char tx[112] = {};
  uint8_t length = 0, offset = 0;
  uint16_t mask = 0;
  uint32_t sequence = 0, lastPing = 0;
  bool active = false, pendingKeys = false;
  bool input = false, waitForRelease = false, inputLost = false;
  static constexpr uint8_t queueSize = 16;
  uint16_t keyQueue[queueSize] = {};
  uint8_t firstKey = 0, queuedKeys = 0;
  uint32_t sentSequence = 0;
  bool busy() const { return offset != length; }
  void leaveInput() {
    if (input) waitForRelease = mask != 0;
    input = false;
    queuedKeys = 0;
  }
  void expire(uint32_t now) {
    if (!active || (uint32_t)(now - lastPing) <= 6000) return;
    inputLost = input;
    leaveInput();
    active = false;
    pendingKeys = false;
  }
  static bool validToken(const char* s) {
    if (strlen(s) != 8) return false;
    for (uint8_t i = 0; i < 8; ++i)
      if (!((s[i] >= '0' && s[i] <= '9') || (s[i] >= 'A' && s[i] <= 'F'))) return false;
    return true;
  }
  static bool number(const char* s, uint32_t& result) {
    if (!*s) return false;
    result = 0;
    for (; *s; ++s) {
      if (*s < '0' || *s > '9') return false;
      uint8_t digit = *s - '0';
      if (result > (UINT32_MAX - digit) / 10) return false;
      result = result * 10 + digit;
    }
    return true;
  }
  void ready(int count) {
    length = count > 0 && count < (int)sizeof(tx) ? (uint8_t)count : 0;
    offset = 0;
  }
public:
  bool readyForCommand() const { return !busy(); }
  uint16_t keyboardMask() const { return input || waitForRelease ? 0 : mask; }
  void buttons(uint16_t value) {
    value &= 0xfff;
    if (value == mask) return;
    mask = value;
    if (!mask) waitForRelease = false;
    ++sequence;
    if (!input) { pendingKeys = true; return; }
    if (queuedKeys == queueSize) {
      leaveInput();
      active = false;
      inputLost = true;
      return;
    }
    keyQueue[(firstKey + queuedKeys) % queueSize] = mask;
    ++queuedKeys;
  }
  void command(const char* line, uint32_t now, bool keyboardIdle = true) {
    if (busy()) return; // The host retries after a bounded timeout; never block key scanning.
    expire(now);
    if (!strcmp(line, "DECKPILOT?")) {
      ready(snprintf(tx, sizeof(tx), "DECKPILOT|1|" DECKPILOT_BOARD_ID "|" DECKPILOT_FIRMWARE_VERSION "\r\n"));
    } else if (!strncmp(line, "DP2|HELLO|", 10) && validToken(line + 10)) {
      leaveInput(); inputLost = false;
      memcpy(token, line + 10, 9); active = true; lastPing = now;
      sequence = 0; pendingKeys = true;
      ready(snprintf(tx, sizeof(tx), "DP2|INFO|%s|" DECKPILOT_BOARD_ID "|" DECKPILOT_FIRMWARE_VERSION "|12|7\r\n", token));
    } else if (active && !strncmp(line, "DP2|INPUT|", 10) && !strcmp(line + 10, token)) {
      if (input) return;
      if (mask || !keyboardIdle) {
        ready(snprintf(tx, sizeof(tx), "DP2|BUSY|%s\r\n", token));
        return;
      }
      input = true; lastPing = now; pendingKeys = false;
      sequence = sentSequence = 0;
      firstKey = queuedKeys = 0;
      ready(snprintf(tx, sizeof(tx), "DP2|INPUT|%s\r\n", token));
    } else if (!strncmp(line, "DP2|PING|", 9) && strlen(line + 9) >= 10 && active &&
               !strncmp(line + 9, token, 8) && line[17] == '|') {
      uint32_t request;
      if (!number(line + 18, request)) return;
      lastPing = now;
      ready(snprintf(tx, sizeof(tx), "DP2|PONG|%s|%lu|%lu|%lu|%u\r\n", token,
          (unsigned long)request, (unsigned long)now, (unsigned long)sequence, (unsigned int)mask));
    }
  }
  template<class SerialType> void tick(SerialType& serial, uint32_t now) {
    // AVR core 1.8.6 bool(Serial) sleeps for 10 ms; dtr() is an immediate read.
    if (!serial.dtr()) { leaveInput(); active = false; inputLost = false; pendingKeys = false; length = offset = 0; return; }
    expire(now);
    if (!busy() && inputLost) {
      ready(snprintf(tx, sizeof(tx), "DP2|LOST|%s\r\n", token));
      inputLost = false;
    } else if (!busy() && input && queuedKeys) {
      ready(snprintf(tx, sizeof(tx), "DP2|KEYS|%s|%lu|%u\r\n", token,
          (unsigned long)++sentSequence, (unsigned int)keyQueue[firstKey]));
      firstKey = (firstKey + 1) % queueSize;
      --queuedKeys;
    } else if (!busy() && active && pendingKeys) {
      ready(snprintf(tx, sizeof(tx), "DP2|KEYS|%s|%lu|%u\r\n", token, (unsigned long)sequence, (unsigned int)mask));
      pendingKeys = false;
    }
    int available = serial.availableForWrite();
    uint8_t count = length - offset;
    if (count > 32) count = 32;
    if (available < count) count = available > 0 ? (uint8_t)available : 0;
    if (count) offset += serial.write((const uint8_t*)tx + offset, count);
  }
};
