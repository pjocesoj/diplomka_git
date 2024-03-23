#include "HardwareSerial.h"
#include "EndPoint.h"
#include "ValueDto.h"
#include <ArduinoJson.h>

Endpoint::Endpoint(){}
Endpoint::Endpoint(HttpEnum http,const char* url)
{
  HTTP=http;
  URL=url; 
}

Endpoint::Endpoint(HttpEnum http,const char* url,int i)
{
  HTTP=http;
  URL=url; 

  Ints.push_back(new ValueDto<int>("a",1));
  Ints.push_back(new ValueDto<int>("b",2));
  Floats.push_back(new ValueDto<float>("c",3.14));
  Bools.push_back(new ValueDto<bool>("B1",true));
}

void  Endpoint::Serialize(JsonObject &jsonObject)// Serialize the main class data 
{  
    jsonObject["HTTP"] = HTTP;
    jsonObject["URL"] = URL;

    JsonArray vals = jsonObject.createNestedArray("Vals");
    for (auto& obj : Ints) 
    {
      JsonObject nestedJsonObject = vals.createNestedObject();
       obj->Serialize(nestedJsonObject);
    }
    for (auto& obj : Floats) 
    {
      JsonObject nestedJsonObject = vals.createNestedObject();
       obj->Serialize(nestedJsonObject);
    }
    for (auto& obj : Bools) 
    {
      JsonObject nestedJsonObject = vals.createNestedObject();
       obj->Serialize(nestedJsonObject);
    }
}

void  Endpoint::Serialize_info(JsonObject &jsonObject)// Serialize the main class data 
{  
    jsonObject["HTTP"] = HTTP;
    jsonObject["URL"] = URL;

    JsonArray vals = jsonObject.createNestedArray("Vals");
    for (auto& obj : Ints) 
    {
      JsonObject nestedJsonObject = vals.createNestedObject();
       obj->Serialize_info(nestedJsonObject);
    }
    for (auto& obj : Floats) 
    {
      JsonObject nestedJsonObject = vals.createNestedObject();
       obj->Serialize_info(nestedJsonObject);
    }
    for (auto& obj : Bools) 
    {
      JsonObject nestedJsonObject = vals.createNestedObject();
       obj->Serialize_info(nestedJsonObject);
    }
}

void  Endpoint::Serialize_values(JsonObject &jsonObject) 
{
    JsonArray ints = jsonObject.createNestedArray("Ints");
    for (auto& obj : Ints) 
    {
      JsonObject nestedJsonObject = ints.createNestedObject();
       obj->Serialize_value(nestedJsonObject);
    }
    JsonArray floats = jsonObject.createNestedArray("Floats");
    for (auto& obj : Floats) 
    {
      JsonObject nestedJsonObject = floats.createNestedObject();
       obj->Serialize_value(nestedJsonObject);
    }
    JsonArray bools = jsonObject.createNestedArray("Bools");
    for (auto& obj : Bools) 
    {
      JsonObject nestedJsonObject = bools.createNestedObject();
       obj->Serialize_value(nestedJsonObject);
    }
  }