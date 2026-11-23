/*
 * ============================================================
 *  StreamDeck 12 touches — Pro Micro (ATmega32U4, 5V/16MHz)
 *  Touches envoyées : F13 à F24 (aucun conflit avec Windows)
 * ============================================================
 *
 *  Basé sur le projet 'Stream Cheap' Mini Macro Keyboard
 *  @author     David Madison
 *  @link       partsnotincluded.com/electronics/diy-stream-deck-mini-macro-keyboard
 *  @license    MIT - Copyright (c) 2018 David Madison
 *  (Adapté à 12 boutons — voir licence ci-dessous)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 *
 * ------------------------------------------------------------
 *  CÂBLAGE (identique au plan) :
 *  - Un côté de chaque switch -> GND commun
 *  - Autre côté -> broche dédiée : 2-10, 14, 15, 16
 *  - Aucune résistance (INPUT_PULLUP interne)
 *
 *  CARTE IDE Arduino : "Arduino Leonardo" (5V/16MHz)
 *
 *  FAILSAFE : reliez la broche 1 (RX) à GND au démarrage
 *  pour désactiver le clavier (en cas de problème).
 * ------------------------------------------------------------
 */
// F13-F24 remain available without any host connection.
#include <HID.h>
#include "DeckPilotProtocol.h"

// One bit per F13-F24 usage, so the report can represent all twelve keys.
constexpr uint8_t deckReportId = 4;
const uint8_t deckReportDescriptor[] PROGMEM = {
  0x05, 0x01,             // Generic Desktop
  0x09, 0x06,             // Keyboard
  0xA1, 0x01,             // Application collection
  0x85, deckReportId,
  0x05, 0x07,             // Keyboard usage page
  0x19, 0x68, 0x29, 0x73, // F13 through F24
  0x15, 0x00, 0x25, 0x01, // Logical range: released or pressed
  0x75, 0x01, 0x95, 0x0C, // Twelve one-bit values
  0x81, 0x02,             // Input: data, variable, absolute
  0x75, 0x04, 0x95, 0x01,
  0x81, 0x03,             // Four padding bits
  0xC0
};
struct DeckKeyboard {
  DeckKeyboard() {
    static HIDSubDescriptor descriptor(deckReportDescriptor, sizeof(deckReportDescriptor));
    HID().AppendDescriptor(&descriptor);
  }

  void update(uint16_t mask, unsigned long now) {
    if (mask == lastReport) return;
    if (retryPending && (unsigned long)(now - lastAttempt) < 20) return;
    uint8_t report[] = {static_cast<uint8_t>(mask), static_cast<uint8_t>(mask >> 8)};
    lastAttempt = now;
    retryPending = HID().SendReport(deckReportId, report, sizeof(report)) < 0;
    if (!retryPending) lastReport = mask;
  }
  bool idle() const { return lastReport == 0 && !retryPending; }

private:
  uint16_t lastReport = 0;
  unsigned long lastAttempt = 0;
  bool retryPending = false;
};
DeckKeyboard keyboard;

const uint8_t pins[] = {2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16};
bool stable[12] = {}, sampled[12] = {};
unsigned long changed[12] = {};
char command[64];
uint8_t commandLength = 0;
bool commandOverflow = false;
unsigned long lastByte = 0;
DeckPilotProtocol protocol;

void setup() {
  pinMode(1, INPUT_PULLUP);
  if (!digitalRead(1)) { for (;;) {} }
  for (uint8_t i = 0; i < 12; ++i) pinMode(pins[i], INPUT_PULLUP);
  pinMode(17, OUTPUT); digitalWrite(17, HIGH); TXLED0;
  Serial.begin(115200); // Never wait for the host.
}
void loop() {
  unsigned long now = millis();
  uint16_t mask = 0;
  for (uint8_t i = 0; i < 12; ++i) {
    bool down = !digitalRead(pins[i]);
    if (down != sampled[i]) { sampled[i] = down; changed[i] = now; }
    if (down != stable[i] && (unsigned long)(now - changed[i]) >= 30) {
      stable[i] = down;
    }
    if (stable[i]) mask |= (uint16_t)1 << i;
  }
  protocol.buttons(mask);
  if (!Serial.dtr() || (unsigned long)(now - lastByte) > 500) { commandLength = 0; commandOverflow = false; }
  for (uint8_t n = 0; n < 32 && Serial.available() && protocol.readyForCommand(); ++n) {
    char c = Serial.read(); lastByte = now;
    if (c == '\n') {
      command[commandLength] = 0;
      if (!commandOverflow) protocol.command(command, now, keyboard.idle());
      commandLength = 0; commandOverflow = false;
    } else if (c != '\r') {
      if (c < 32 || c > 126) commandOverflow = true;
      else if (commandLength < sizeof(command) - 1) command[commandLength++] = c;
      else commandOverflow = true;
    }
  }
  protocol.tick(Serial, now);
  keyboard.update(protocol.keyboardMask(), now);
}
