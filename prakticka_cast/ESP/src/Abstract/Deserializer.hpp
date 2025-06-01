#ifndef Deserializer_H_
#define Deserializer_H_

#include "../Lib/EndPointDto.hpp"
#include "../Lib/ValueDto.hpp"

/**
 * @brief desrializuje JSON, ktery prisel z POST a hodnoty zapise do prislusnych hodnot endpointu 
 * 
 * @param json JSON k deserializaci
 * @param ep endpoint kam se maji hodnoty zapsat
 */
void Deserialize(const char* json, EndPointDto *ep);


#endif