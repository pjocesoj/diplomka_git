#ifndef VALUE_H_
#define VALUE_H_

#include "enums.h"
#include <type_traits>
#include <stdexcept>

// serializace si neumi poradit s generikou kdyz je ctrida rozdelena na .h a .cpp
template <class T>
class ValueDto
{
public:
  ValueDto() {};
  ValueDto(const char *name, T val)
  {
    Value = val;
    Name = name;
  }

  const char *Name = "val";
  T Value;

  ValTypeEnum GetType()
  {
    if constexpr (std::is_same_v<T, int>)
    {
      return INT;
    }
    else if constexpr (std::is_same_v<T, float>)
    {
      return FLOAT;
    }
    else if constexpr (std::is_same_v<T, bool>)
    {
      return BOOL;
    }
    else
    {
      throw std::runtime_error("Unsupported type");
    }
  }

private:

};
#endif