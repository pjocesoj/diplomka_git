#ifndef HELPERS_H_
#define HELPERS_H_

#include "Endpoint.h"

void AvailableRAM(char* const title);

void deserializeDTO(String json, Endpoint *ep);

#endif