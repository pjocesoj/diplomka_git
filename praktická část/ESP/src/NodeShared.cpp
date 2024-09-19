#include "Node.h"
#include "HardwareSerial.h"
#include "../Endpoint.h"
#include "Abstract/Serializer.h"

void printEndpoint(Endpoint *ep)
{
    char ret[512];
    Serialize(ep, ret, 512);
    Serial.println(ret);
}