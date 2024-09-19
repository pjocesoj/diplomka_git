#include "helpers.h"

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
 * @brief desrializuje JSON, ktery prisel z POST a hodnoty zapise do prislusnych hodnot endpointu
 *
 * @param json JSON k deserializaci
 * @param ep endpoint kam se maji hodnoty zapsat
 */
void deserializeDTO(String json, EndPointDto *ep)
{
  JsonDocument doc;
  deserializeJson(doc, json);

  JsonArray array1 = doc["Ints"];
  int j = 0;
  for (int i : array1)
  {
    ep->Ints[j]->Value = i;
    Serial.println(i);
    j++;
  }

  JsonArray array2 = doc["Floats"];
  j = 0;
  for (float i : array2)
  {
    ep->Floats[j]->Value = i;
    Serial.println(i);
    j++;
  }

  JsonArray array3 = doc["Bools"];
  j = 0;
  for (bool i : array3)
  {
    ep->Bools[j]->Value = i;
    Serial.println(i);
    j++;
  }
}

/**
 * @brief z milisekund vytvori casovou znacku ve formatu HH:MM:SS:MSS
 * @author GitHub Copilot
 */
const char *MillisToTimestemp(ulong milliseconds)
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