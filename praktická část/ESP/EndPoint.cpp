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
Endpoint::Endpoint(HttpEnum http, const char *url,int delay)
{
  HTTP = http;
  URL = url;
  Delay = delay;
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

  if (Delay.has_value()) 
  {
    jsonObject["Delay"] = Delay.value();
  }

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

  if (Delay.has_value()) 
  {
    jsonObject["Delay"] = Delay.value();
  }

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
  // JsonArray ints = jsonObject.createNestedArray("Ints"); //ArduinoJson 6.x
  JsonArray ints = jsonObject["Ints"].to<JsonArray>();
  for (auto &obj : Ints)
  {
    // JsonObject nestedJsonObject = ints.createNestedObject(); //ArduinoJson 6.x
    
    //JsonObject nestedJsonObject = ints.add<JsonObject>(); //ArduinoJson 7.1
    //obj->Serialize_value(nestedJsonObject);

    ints.add(obj->Value);
  }
  // JsonArray floats = jsonObject.createNestedArray("Floats"); //ArduinoJson 6.x
  JsonArray floats = jsonObject["Floats"].to<JsonArray>();
  for (auto &obj : Floats)
  {
    // JsonObject nestedJsonObject = floats.createNestedObject(); //ArduinoJson 6.x
    
    //JsonObject nestedJsonObject = floats.add<JsonObject>(); //ArduinoJson 7.1
    //obj->Serialize_value(nestedJsonObject);

    floats.add(obj->Value);
  }
  // JsonArray bools = jsonObject.createNestedArray("Bools"); //ArduinoJson 6.x
  JsonArray bools = jsonObject["Bools"].to<JsonArray>();
  for (auto &obj : Bools)
  {
    // JsonObject nestedJsonObject = bools.createNestedObject(); //ArduinoJson 6.x
    
    //JsonObject nestedJsonObject = bools.add<JsonObject>(); //ArduinoJson 7.1
    //obj->Serialize_value(nestedJsonObject);

    bools.add(obj->Value);
  }
}