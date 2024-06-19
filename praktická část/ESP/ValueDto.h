#ifndef VALUE_H_
#define VALUE_H_

#include "enums.h"
#include "WString.h"
#include <ArduinoJson.h>

// serializace si neumi poradit s generikou kdyz je ctrida rozdelena na .h a .cpp
template <class T>
class ValueDto
{
public:
  ValueDto(){};
  ValueDto(const char *name, T val)
  {
    Value = val;
    Name = name;
  }
  void Serialize(JsonObject &jsonObject)
  {
    // Serialize the nested class data
    jsonObject["Type"] = getType(Value);
    jsonObject["Name"] = Name;
    jsonObject["Value"] = Value;
  }
  void Serialize_info(JsonObject &jsonObject) // Serialize the nested class data
  {
    jsonObject["Type"] = getType(Value);
    jsonObject["Name"] = Name;
  }
  void Serialize_value(JsonObject &jsonObject) // Serialize the nested class data
  {
    jsonObject["Value"] = Value;
  }

  const char *Name = "val";
  T Value;

private:
  ValTypeEnum getType(int v){return INT;}
  ValTypeEnum getType(float v){return FLOAT;}
  ValTypeEnum getType(bool v){return BOOL;}
};
#endif