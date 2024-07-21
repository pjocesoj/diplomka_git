#include "Node.h"
#include "HardwareSerial.h"
#include "../Endpoint.h"

void printEndpoint(Endpoint *ep)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    ep->Serialize(jsonObject);
    String ret;
    serializeJson(doc, ret);
    Serial.println(ret);
}