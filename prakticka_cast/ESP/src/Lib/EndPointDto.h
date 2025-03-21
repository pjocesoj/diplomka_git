#ifndef EndPointDto_H_
#define EndPointDto_H_

#include "Enums.h"
#include "ValueDto.h"
#include <vector>
#include <optional> // optional = nullable

class EndPointDto
{
public:
  EndPointDto() {}
  EndPointDto(HttpEnum http, const char *url, EndPointType type=EP_TYPE_GET)
  {
    HTTP = http;
    URL = url;
    Type = type;
  }
  EndPointDto(HttpEnum http, const char *url, int delay,EndPointType type=EP_TYPE_GET)
  {
    HTTP = http;
    URL = url;
    Delay = delay;
    Type = type;
  }

  HttpEnum HTTP = HttpEnum::GET;
  EndPointType Type = EndPointType::EP_TYPE_GET;
  const char *URL = "GetVal";

  std::vector<ValueDto<int> *> Ints;
  std::vector<ValueDto<float> *> Floats;
  std::vector<ValueDto<bool> *> Bools;

  std::optional<int> Delay;

private:

};
#endif