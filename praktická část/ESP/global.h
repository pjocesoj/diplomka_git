#ifndef GLOBAL_H_
#define GLOBAL_H_

#include "ESP8266WebServer.h"
#include <vector>
#include "Endpoint.h"

//extern rika compileru ze definuji jinde

extern ESP8266WebServer server; //HTTP server
extern std::vector<Endpoint*> endpoints; //list vsech endpointu pro tento node

#endif GLOBAL_H_