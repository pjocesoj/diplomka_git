#ifndef EndPointDto_H_
#define EndPointDto_H_

#include "Enums.hpp"
#include "ValueDto.hpp"
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

  std::vector<ValueDto<int> *> Val_Ints;
  std::vector<ValueDto<float> *> Val_Floats;
  std::vector<ValueDto<bool> *> Val_Bools;

  std::vector<ValueDto<int> *> Arg_Ints;
  std::vector<ValueDto<float> *> Arg_Floats;
  std::vector<ValueDto<bool> *> Arg_Bools;

  std::optional<int> Delay;

private:

};
#endif