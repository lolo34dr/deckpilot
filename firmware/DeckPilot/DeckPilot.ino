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
#include <Keyboard.h>
#include "DeckPilotProtocol.h"

const uint8_t pins[] = {2, 3, 4, 5, 6, 7, 8, 9, 10, 14, 15, 16};
const uint8_t keys[] = {KEY_F13, KEY_F14, KEY_F15, KEY_F16, KEY_F17,
                        KEY_F18, KEY_F19, KEY_F20, KEY_F21, KEY_F22, KEY_F23, KEY_F24};
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
  Keyboard.begin();
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
      if (down) Keyboard.press(keys[i]); else Keyboard.release(keys[i]);
    }
    if (stable[i]) mask |= (uint16_t)1 << i;
  }
  protocol.buttons(mask);
  if (!Serial || (unsigned long)(now - lastByte) > 500) { commandLength = 0; commandOverflow = false; }
  for (uint8_t n = 0; n < 32 && Serial.available() && protocol.readyForCommand(); ++n) {
    char c = Serial.read(); lastByte = now;
    if (c == '\n') {
      command[commandLength] = 0;
      if (!commandOverflow) protocol.command(command, now);
      commandLength = 0; commandOverflow = false;
    } else if (c != '\r') {
      if (c < 32 || c > 126) commandOverflow = true;
      else if (commandLength < sizeof(command) - 1) command[commandLength++] = c;
      else commandOverflow = true;
    }
  }
  protocol.tick(Serial, now);
}
