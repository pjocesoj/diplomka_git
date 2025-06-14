#ifndef GLOBAL_H_
#define GLOBAL_H_

#include "ESP8266WebServer.h"
#include <vector>
#include "src/Lib/EndPointDto.hpp"
#include "src/Abstract/CommunicationHandler.hpp"

//extern rika compileru ze definuji jinde

//extern ESP8266WebServer server; //HTTP server
extern CommunicationHandler communicationHandler; //HTTP server
extern std::vector<EndPointDto*> endpoints; //list vsech endpointu pro tento node

#endif