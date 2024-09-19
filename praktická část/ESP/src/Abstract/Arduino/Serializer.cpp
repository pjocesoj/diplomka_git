#include "../Serializer.h"
#include <ArduinoJson.h>

void printJSON(JsonDocument &doc)
{
    String ret;
    serializeJson(doc, ret);
    Serial.println(ret);
}
void printJSON(char* arr, int size)
{
    Serial.println(arr);
    Serial.printf("size %d/%d\n",strlen(arr),size);
}

/*
 *-----------------------------------------------------  ValueDTo  helpers (arduino)-----------------------------------------------------
 */

template <class T>
void Serialize(ValueDto<T> *value,JsonObject &jsonObject)
{
    jsonObject["Type"] = value->GetType();
    jsonObject["Name"] = value->Name;
    jsonObject["Value"] = value->Value;
}

template <class T>
void SerializeInfo(ValueDto<T> *value,JsonObject &jsonObject)
{
    jsonObject["Type"] = value->GetType();
    jsonObject["Name"] = value->Name;
}

/*
 *-----------------------------------------------------  ValueDTo  -----------------------------------------------------
 */

/**
 * @brief Serializuje hodnotu ValueDto
 */
template <class T>
void Serialize(ValueDto<T> *value)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    Serialize(value,jsonObject);

    printJSON(doc);
}
void Serialize(ValueDto<int> *value){Serialize<int>(value);}
void Serialize(ValueDto<float> *value){Serialize<float>(value);}
void Serialize(ValueDto<bool> *value){Serialize<bool>(value);}

template <class T>
void SerializeInfo(ValueDto<T> *value)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    SerializeInfo(value,jsonObject);

    printJSON(doc);
}
void SerializeInfo(ValueDto<int> *value){SerializeInfo<int>(value);}
void SerializeInfo(ValueDto<float> *value){SerializeInfo<float>(value);}
void SerializeInfo(ValueDto<bool> *value){SerializeInfo<bool>(value);}

template <class T>
void SerializeValue(ValueDto<T> *value)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();

    jsonObject["Value"] = value->Value;

    printJSON(doc);
}
void SerializeValue(ValueDto<int> *value){SerializeValue<int>(value);}
void SerializeValue(ValueDto<float> *value){SerializeValue<float>(value);}
void SerializeValue(ValueDto<bool> *value){SerializeValue<bool>(value);}

/*
 *-----------------------------------------------------  Endpoint helpers (arduino)-----------------------------------------------------
 */

void Serialize(EndPointDto *ep,JsonObject &jsonObject)
{
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
        Serialize(obj,nestedJsonObject);
    }
    for (auto &obj : ep->Floats)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        Serialize(obj,nestedJsonObject);
    }
    for (auto &obj : ep->Bools)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        Serialize(obj,nestedJsonObject);
    }
}

void SerializeInfo(EndPointDto *ep,JsonObject &jsonObject)
{
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
        SerializeInfo(obj,nestedJsonObject);
    }
    for (auto &obj : ep->Floats)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        SerializeInfo(obj,nestedJsonObject);
    }
    for (auto &obj : ep->Bools)
    {
        JsonObject nestedJsonObject = vals.add<JsonObject>();
        SerializeInfo(obj,nestedJsonObject);
    }
}

void SerializeValue(EndPointDto *ep,JsonObject &jsonObject)
{
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
}

/*
 *-----------------------------------------------------  Endpoint  -----------------------------------------------------
 */

void Serialize(EndPointDto *ep,char* arr, int size)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    Serialize(ep,jsonObject);

    serializeJson(doc, arr, size);

    printJSON(arr,size);
}

void SerializeInfo(EndPointDto *ep, char* arr, int size)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    SerializeInfo(ep,jsonObject);

    serializeJson(doc, arr, size);

        printJSON(arr,size);
}

void SerializeValue(EndPointDto *ep, char* arr, int size)
{
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    SerializeValue(ep,jsonObject);

    serializeJson(doc, arr, size);

    printJSON(arr,size);
}

void SerializeEndpoints(std::vector<EndPointDto*> endpoints, char* arr, int size)
{
    JsonDocument doc;
    JsonArray EPs = doc.to<JsonArray>();
    for (auto &obj : endpoints)
    {
        JsonObject nestedJsonObject = EPs.add<JsonObject>();
        SerializeInfo(obj,nestedJsonObject);
    }

    serializeJson(doc, arr, size);

    printJSON(arr,size);
}