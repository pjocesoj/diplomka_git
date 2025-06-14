#ifndef Serializer_H_
#define Serializer_H_

#include "../Lib/EndPointDto.hpp"
#include "../Lib/ValueDto.hpp"

//jelikož se každý soubor kompiluje samostatně, musí být datový typ známý předem (generika nejde vytáhnout do cpp)
//https://www.learncpp.com/cpp-tutorial/template-classes/

/*
 *-----------------------------------------------------  ValueDTo  -----------------------------------------------------
 */

void Serialize(ValueDto<int> *value);
void Serialize(ValueDto<float> *value);
void Serialize(ValueDto<bool> *value);

void SerializeInfo(ValueDto<int> *value);
void SerializeInfo(ValueDto<float> *value);
void SerializeInfo(ValueDto<bool> *value);

void SerializeValue(ValueDto<int> *value);
void SerializeValue(ValueDto<float> *value);
void SerializeValue(ValueDto<bool> *value);
/*
 *-----------------------------------------------------  Endpoint  -----------------------------------------------------
 */

/**
 * @brief serializuje kompletni info o endpointu {GET/POST, URL, vals[] {typ, jmeno, hodnota} }
 *
 * @param ep endpoint ktery ma serializovat
 * @param arr kam ma zapisovat
 * @param size kapacita pole
 */
void Serialize(EndPointDto *ep, char* arr, int size);

/**
 * @brief serializuje informace o endpointu {GET/POST, URL, vals[] {typ, jmeno} }
 *
 * @param ep endpoint ktery ma serializovat
 * @param arr kam ma zapisovat
 * @param size kapacita pole
 */
void SerializeInfo(EndPointDto *ep, char* arr, int size);

/**
 * @brief serializuje hodnoty jako { Ints[], Floats[], Bools[] }
 *
 * @param ep endpoint ktery ma serializovat
 * @param arr kam ma zapisovat
 * @param size kapacita pole
 */
void SerializeValue(EndPointDto *ep, char* arr, int size);

/**
 * @brief serializuje list endpointu
 * 
 * @param endpoints list endpointu
 * @param arr kam ma zapisovat
 * @param size kapacita pole
 */
void SerializeEndpoints(std::vector<EndPointDto*> endpoints,char* arr, int size);

#endif