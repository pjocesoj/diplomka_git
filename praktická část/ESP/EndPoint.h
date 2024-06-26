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

  void Serialize(JsonObject &jsonObject);
  void Serialize_info(JsonObject &jsonObject);
  void Serialize_values(JsonObject &jsonObject);

  HttpEnum HTTP = GET;
  const char *URL = "GetVal";

  std::vector<ValueDto<int>*> Ints;
  std::vector<ValueDto<float>*> Floats;
  std::vector<ValueDto<bool>*> Bools;

private:

};
#endif