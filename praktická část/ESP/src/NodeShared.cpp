#include "Node.h"
#include "HardwareSerial.h"
#include "Abstract/Serializer.h"

void printEndpoint(EndPointDto *ep)
{
    char ret[512];
    Serialize(ep, ret, 512);
    Serial.println(ret);
}