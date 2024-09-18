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

/*
 *-----------------------------------------------------  ValueDTo  -----------------------------------------------------
 */
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

/*
 *-----------------------------------------------------  Endpoint  -----------------------------------------------------
 */
void Serialize(Endpoint *ep)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    jsonObject["HTTP"] = ep->HTTP;
    jsonObject["URL"] = ep->URL;

    if (ep->Delay.has_value())
    {
        jsonObject["Delay"] = ep->Delay.value();
    }

    JsonArray vals = jsonObject["Vals"].to<JsonArray>();
    for (auto &obj : ep->Ints)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        obj->Serialize(nestedJsonObject);
    }
    for (auto &obj : ep->Floats)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        obj->Serialize(nestedJsonObject);
    }
    for (auto &obj : ep->Bools)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        obj->Serialize(nestedJsonObject);
    }

        printJSON(doc);
}

void SerializeInfo(Endpoint *ep)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    jsonObject["HTTP"] = ep->HTTP;
    jsonObject["URL"] = ep->URL;

    if (ep->Delay.has_value())
    {
        jsonObject["Delay"] = ep->Delay.value();
    }

    JsonArray vals = jsonObject["Vals"].to<JsonArray>();
    for (auto &obj : ep->Ints)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        obj->Serialize_info(nestedJsonObject);
    }
    for (auto &obj : ep->Floats)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        obj->Serialize_info(nestedJsonObject);
    }
    for (auto &obj : ep->Bools)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        obj->Serialize_info(nestedJsonObject);
    }

        printJSON(doc);
}

void SerializeValue(Endpoint *ep)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    JsonArray ints = jsonObject["Ints"].to<JsonArray>();
    for (auto &obj : ep->Ints)
    {
        ints.add(obj->Value);
    }
    JsonArray floats = jsonObject["Floats"].to<JsonArray>();
    for (auto &obj : ep->Floats)
    {
        floats.add(obj->Value);
    }
    JsonArray bools = jsonObject["Bools"].to<JsonArray>();
    for (auto &obj : ep->Bools)
    {
        bools.add(obj->Value);
    }

    printJSON(doc);
}
#endif