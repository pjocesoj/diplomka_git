#include "helpers.hpp"

#include "HardwareSerial.h"
#include "Esp.h"
#include <ArduinoJson.h>

/**
 * @brief vypise do konzole stav heap a stack
 *
 * @param title text pred vypisem usnadnujici orientaci ve vypisu
 */
void AvailableRAM(const char *title)
{
  Serial.print(title);

  Serial.print("\t Free heap: ");
  Serial.print(ESP.getFreeHeap());

  Serial.print("\t stack: ");
  Serial.println(ESP.getFreeContStack());
}

/**
 * @brief z milisekund vytvori casovou znacku ve formatu HH:MM:SS:MSS
 * @author GitHub Copilot
 */
const char *MillisToTimestemp(unsigned long milliseconds)
{

  unsigned long hours = milliseconds / 3600000;
  milliseconds %= 3600000;
  unsigned long minutes = milliseconds / 60000;
  milliseconds %= 60000;
  unsigned long seconds = milliseconds / 1000;
  milliseconds %= 1000;

  // Allocate a buffer large enough to hold the concatenated string
  // 3x(2 digit + 3 separator)+3 digit and 1 for null terminator + 2 for safety
  static char buffer[21];

  // Format the numbers into the buffer
  sprintf(buffer, "%02lu : %02lu : %02lu : %03lu", hours, minutes, seconds, milliseconds);

  // Return the buffer
  return buffer;
}