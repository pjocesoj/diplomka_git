#ifndef ENDPOINT_H_
#define ENDPOINT_H_

#include "WString.h"
#include "Enums.h"
#include "ValueDto.h"
#include <ArduinoJson.h>
#include <vector>

class Endpoint
{
public:
  Endpoint();
  Endpoint(HttpEnum http, const char *url);

  // serializuje kompletni info o endpointu {GET/POST, URL, vals[] {typ, jmeno, hodnota} }
  void Serialize(JsonObject &jsonObject);
  // serializuje informace o endpointu {GET/POST, URL, vals[] {typ, jmeno} }
  void Serialize_info(JsonObject &jsonObject);
  // serializuje hodnoty jako { Ints[], Floats[], Bools[] }
  void Serialize_values(JsonObject &jsonObject);

  HttpEnum HTTP = GET;
  const char *URL = "GetVal";

  std::vector<ValueDto<int>*> Ints;
  std::vector<ValueDto<float>*> Floats;
  std::vector<ValueDto<bool>*> Bools;

private:

};
#endif