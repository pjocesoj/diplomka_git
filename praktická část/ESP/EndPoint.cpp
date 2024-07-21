#include "HardwareSerial.h"
#include "EndPoint.h"
#include "ValueDto.h"
#include <ArduinoJson.h>

Endpoint::Endpoint() {}
Endpoint::Endpoint(HttpEnum http, const char *url)
{
  HTTP = http;
  URL = url;
}

/**
 * @brief serializuje kompletni info o endpointu {GET/POST, URL, vals[] {typ, jmeno, hodnota} }
 *
 * @param jsonObject JSON kam ma zapisovat
 */
void Endpoint::Serialize(JsonObject &jsonObject)
{
  jsonObject["HTTP"] = HTTP;
  jsonObject["URL"] = URL;

  // JsonArray vals = jsonObject.createNestedArray("Vals");
  JsonArray vals = jsonObject["Vals"].to<JsonArray>();
  for (auto &obj : Ints)
  {
    // JsonObject nestedJsonObject = vals.createNestedObject();
    JsonObject nestedJsonObject = vals.add<JsonObject>();
    obj->Serialize(nestedJsonObject);
  }
  for (auto &obj : Floats)
  {
    // JsonObject nestedJsonObject = vals.createNestedObject();
    JsonObject nestedJsonObject = vals.add<JsonObject>();
    obj->Serialize(nestedJsonObject);
  }
  for (auto &obj : Bools)
  {
    // JsonObject nestedJsonObject = vals.createNestedObject();
    JsonObject nestedJsonObject = vals.add<JsonObject>();
    obj->Serialize(nestedJsonObject);
  }
}

/**
 * @brief serializuje informace o endpointu {GET/POST, URL, vals[] {typ, jmeno} }
 *
 * @param jsonObject JSON kam ma zapisovat
 */
void Endpoint::Serialize_info(JsonObject &jsonObject)
{
  jsonObject["HTTP"] = HTTP;
  jsonObject["URL"] = URL;

  // JsonArray vals = jsonObject.createNestedArray("Vals");
  JsonArray vals = jsonObject["Vals"].to<JsonArray>();
  for (auto &obj : Ints)
  {
    // JsonObject nestedJsonObject = vals.createNestedObject();
    JsonObject nestedJsonObject = vals.add<JsonObject>();
    obj->Serialize_info(nestedJsonObject);
  }
  for (auto &obj : Floats)
  {
    // JsonObject nestedJsonObject = vals.createNestedObject();
    JsonObject nestedJsonObject = vals.add<JsonObject>();
    obj->Serialize_info(nestedJsonObject);
  }
  for (auto &obj : Bools)
  {
    // JsonObject nestedJsonObject = vals.createNestedObject();
    JsonObject nestedJsonObject = vals.add<JsonObject>();
    obj->Serialize_info(nestedJsonObject);
  }
}

/**
 * @brief serializuje hodnoty jako { Ints[], Floats[], Bools[] }
 *
 * @param jsonObject JSON kam ma zapisovat
 */
void Endpoint::Serialize_values(JsonObject &jsonObject)
{
  // JsonArray ints = jsonObject.createNestedArray("Ints");
  JsonArray ints = jsonObject["Ints"].to<JsonArray>();
  for (auto &obj : Ints)
  {
    // JsonObject nestedJsonObject = ints.createNestedObject();
    JsonObject nestedJsonObject = ints.add<JsonObject>();
    obj->Serialize_value(nestedJsonObject);
  }
  // JsonArray floats = jsonObject.createNestedArray("Floats");
  JsonArray floats = jsonObject["Floats"].to<JsonArray>();
  for (auto &obj : Floats)
  {
    // JsonObject nestedJsonObject = floats.createNestedObject();
    JsonObject nestedJsonObject = floats.add<JsonObject>();
    obj->Serialize_value(nestedJsonObject);
  }
  // JsonArray bools = jsonObject.createNestedArray("Bools");
  JsonArray bools = jsonObject["Bools"].to<JsonArray>();
  for (auto &obj : Bools)
  {
    // JsonObject nestedJsonObject = bools.createNestedObject();
    JsonObject nestedJsonObject = bools.add<JsonObject>();
    obj->Serialize_value(nestedJsonObject);
  }
}