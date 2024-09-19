#ifndef HELPERS_H_
#define HELPERS_H_

#include "src/Lib/EndPointDto.h"
#include "WString.h"

void AvailableRAM(const char* title);

void deserializeDTO(String json, EndPointDto *ep);

const char* MillisToTimestemp(ulong milliseconds);

#endif