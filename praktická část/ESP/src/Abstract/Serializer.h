#ifndef Serializer_H_
#define Serializer_H_

#include <ArduinoJson.h>

#include "../../EndPoint.h"
#include "../../ValueDto.h"

void printJSON(JsonDocument &doc)
{
    String ret;
    serializeJson(doc, ret);
    Serial.println(ret);
}

template <class T>
void Serialize(ValueDto<T> *value)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    jsonObject["Type"] = value->GetType();
    jsonObject["Name"] = value->Name;
    jsonObject["Value"] = value->Value;

    printJSON(doc);
}

template <class T>
void SerializeInfo(ValueDto<T> *value)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    jsonObject["Type"] = value->GetType();
    jsonObject["Name"] = value->Name;

    printJSON(doc);
}

template <class T>
void SerializeValue(ValueDto<T> *value)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    jsonObject["Value"] = value->Value;

    printJSON(doc);
}

#endif