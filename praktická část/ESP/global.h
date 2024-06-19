#ifndef GLOBAL_H_
#define GLOBAL_H_

#include "ESP8266WebServer.h"
#include "Endpoint.h"

ESP8266WebServer server(80);
std::vector<Endpoint *> endpoints;

#endif GLOBAL_H_