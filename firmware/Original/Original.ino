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
// ---------------------------------
// Définition des touches (F13 à F24)
#define BUTTON_KEY1  KEY_F13
#define BUTTON_KEY2  KEY_F14
#define BUTTON_KEY3  KEY_F15
#define BUTTON_KEY4  KEY_F16
#define BUTTON_KEY5  KEY_F17
#define BUTTON_KEY6  KEY_F18
#define BUTTON_KEY7  KEY_F19
#define BUTTON_KEY8  KEY_F20
#define BUTTON_KEY9  KEY_F21
#define BUTTON_KEY10 KEY_F22
#define BUTTON_KEY11 KEY_F23
#define BUTTON_KEY12 KEY_F24
// Définition des broches (votre câblage)
#define BUTTON_PIN1  2
#define BUTTON_PIN2  3
#define BUTTON_PIN3  4
#define BUTTON_PIN4  5
#define BUTTON_PIN5  6
#define BUTTON_PIN6  7
#define BUTTON_PIN7  8
#define BUTTON_PIN8  9
#define BUTTON_PIN9  10
#define BUTTON_PIN10 14
#define BUTTON_PIN11 15
#define BUTTON_PIN12 16
// ---------------------------------
#include "Keyboard.h"
// Classe bouton : gère appui/relâchement et anti-rebond
class button {
  public:
  const char key;
  const uint8_t pin;
  button(uint8_t k, uint8_t p) : key(k), pin(p){}
  void press(boolean state){
    if(state == pressed || (millis() - lastPressed  <= debounceTime)){
      return; // Rien à faire
    }
    lastPressed = millis();
    state ? Keyboard.press(key) : Keyboard.release(key);
    pressed = state;
  }
  void update(){
    press(!digitalRead(pin));
  }
  private:
  const unsigned long debounceTime = 30;  // anti-rebond 30 ms
  unsigned long lastPressed = 0;
  boolean pressed = 0;
} ;
// Les 12 boutons : {touche envoyée, broche}
button buttons[] = {
  {BUTTON_KEY1,  BUTTON_PIN1},   // SW1  broche 2  -> F13
  {BUTTON_KEY2,  BUTTON_PIN2},   // SW2  broche 3  -> F14
  {BUTTON_KEY3,  BUTTON_PIN3},   // SW3  broche 4  -> F15
  {BUTTON_KEY4,  BUTTON_PIN4},   // SW4  broche 5  -> F16
  {BUTTON_KEY5,  BUTTON_PIN5},   // SW5  broche 6  -> F17
  {BUTTON_KEY6,  BUTTON_PIN6},   // SW6  broche 7  -> F18
  {BUTTON_KEY7,  BUTTON_PIN7},   // SW7  broche 8  -> F19
  {BUTTON_KEY8,  BUTTON_PIN8},   // SW8  broche 9  -> F20
  {BUTTON_KEY9,  BUTTON_PIN9},   // SW9  broche 10 -> F21
  {BUTTON_KEY10, BUTTON_PIN10},  // SW10 broche 14 -> F22
  {BUTTON_KEY11, BUTTON_PIN11},  // SW11 broche 15 -> F23
  {BUTTON_KEY12, BUTTON_PIN12},  // SW12 broche 16 -> F24
};
const uint8_t NumButtons = sizeof(buttons) / sizeof(button);
const uint8_t ledPin = 17;  // LED RX du Pro Micro
void setup() {
  // Sécurité : broche 1 (RX) reliée à GND = clavier désactivé
  pinMode(1, INPUT_PULLUP);
  if(!digitalRead(1)){
    failsafe();
  }
  // LED éteinte (active à l'état bas)
  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, HIGH);
  TXLED0;
  // Toutes les broches boutons en entrée avec pull-up interne
  for(int i = 0; i < NumButtons; i++){
    pinMode(buttons[i].pin, INPUT_PULLUP);
  }
  Keyboard.begin();  // démarre l'émulation clavier USB
}
void loop() {
  for(int i = 0; i < NumButtons; i++){
    buttons[i].update();
  }
}
void failsafe(){
  for(;;){} // On reste bloqué ici (clavier inactif)
}