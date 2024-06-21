#define NODE1

#ifndef NODE_H_
#define NODE_H_

#include "../EndPoint.h"

/**
 * @brief inicializace nodu (pridani endpointu)
 */
void NodeInit();

/**
 * @brief vypise vsechny informoce o endpointu do konzole (pouzivan pro kontrolu pri inicializaci)
 */
void printEndpoint(Endpoint *ep);

#endif