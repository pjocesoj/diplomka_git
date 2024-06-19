#ifndef HELPERS_H_
#define HELPERS_H_

#include "Endpoint.h"

void AvailableRAM(char* const title)
{
  Serial.print(title);

  Serial.print("\t Free heap: ");
  Serial.print(ESP.getFreeHeap());

  Serial.print("\t stack: ");
  Serial.println(ESP.getFreeContStack());
}

void deserializace(String json, Endpoint *ep)
{
  DynamicJsonDocument doc(1024);
  deserializeJson(doc, json);

  JsonArray array1 = doc["Ints"];
  int j=0;
  for(int i : array1)   
  {
    ep->Ints[j]->Value=i;
    Serial.println(i);
    j++;
  }

  JsonArray array2 = doc["Floats"];
  j=0;
  for(float i : array2) 
  {
    ep->Floats[j]->Value=i;
    Serial.println(i);
    j++;
  }

  JsonArray array3 = doc["Bools"];
  j=0;
  for(bool i : array3) 
  {
    ep->Bools[j]->Value=i;
    Serial.println(i);
    j++;
  }
}

#endif