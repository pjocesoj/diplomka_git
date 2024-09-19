#ifndef ENDPOINT_H_
#define ENDPOINT_H_

#include "Enums.h"
#include "ValueDto.h"
#include <vector>
#include <optional> // optional = nullable

class Endpoint
{
public:
  Endpoint();
  Endpoint(HttpEnum http, const char *url);
  Endpoint(HttpEnum http, const char *url,int delay);

  HttpEnum HTTP = GET;
  const char *URL = "GetVal";

  std::vector<ValueDto<int>*> Ints;
  std::vector<ValueDto<float>*> Floats;
  std::vector<ValueDto<bool>*> Bools;

  std::optional<int> Delay;

private:

};
#endif