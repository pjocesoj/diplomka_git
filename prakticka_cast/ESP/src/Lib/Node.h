#define NODE3

#ifndef NODE_H_
#define NODE_H_

#include "EndPointDto.h"

/**
 * @brief inicializace nodu (pridani endpointu)
 */
void NodeInit();

/**
 * @brief vypise vsechny informoce o endpointu do konzole (pouzivan pro kontrolu pri inicializaci)
 */
void printEndpoint(EndPointDto *ep);

#endif