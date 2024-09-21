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
  EndPointDto(HttpEnum http, const char *url)
  {
    HTTP = http;
    URL = url;
  }
  EndPointDto(HttpEnum http, const char *url, int delay)
  {
    HTTP = http;
    URL = url;
    Delay = delay;
  }

  HttpEnum HTTP = GET;
  const char *URL = "GetVal";

  std::vector<ValueDto<int> *> Ints;
  std::vector<ValueDto<float> *> Floats;
  std::vector<ValueDto<bool> *> Bools;

  std::optional<int> Delay;

private:

};
#endif