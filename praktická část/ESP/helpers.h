#ifndef HELPERS_H_
#define HELPERS_H_

void AvailableRAM(char* const title)
{
  Serial.print(title);

  Serial.print("\t Free heap: ");
  Serial.print(ESP.getFreeHeap());

  Serial.print("\t stack: ");
  Serial.println(ESP.getFreeContStack());
}

#endif