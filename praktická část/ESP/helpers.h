#ifndef HELPERS_H_
#define HELPERS_H_

#include "Endpoint.h"
#include "WString.h"

void AvailableRAM(const char* title);

void deserializeDTO(String json, Endpoint *ep);

const char* MillisToTimestemp(ulong milliseconds);

#endif