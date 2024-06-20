#ifndef GLOBAL_H_
#define GLOBAL_H_

#include "ESP8266WebServer.h"
#include <vector>
#include "Endpoint.h"

extern ESP8266WebServer server; //rika compileru ze definuji jinde
extern std::vector<Endpoint*> endpoints; //rika compileru ze definuji jinde

#endif GLOBAL_H_