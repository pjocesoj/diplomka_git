#include "HardwareSerial.h"
#include "EndPoint.h"
//#include "ValueDto.h"
#include <ArduinoJson.h>

Endpoint::Endpoint(){}
Endpoint::Endpoint(HttpEnum http,const char* url)
{
  HTTP=http;
  URL=url; 
}