#include "../Deserializer.h"
#include <ArduinoJson.h>

void Deserialize(const char* json, EndPointDto *ep)
{
  JsonDocument doc;
  deserializeJson(doc, json);

  JsonArray array1 = doc["Ints"];
  int j=0;
  for(int i : array1)   
  {
    ep->Arg_Ints[j]->Value=i;
    j++;
  }

  JsonArray array2 = doc["Floats"];
  j=0;
  for(float i : array2) 
  {
    ep->Arg_Floats[j]->Value=i;
    j++;
  }

  JsonArray array3 = doc["Bools"];
  j=0;
  for(bool i : array3) 
  {
    ep->Arg_Bools[j]->Value=i;
    j++;
  }
}