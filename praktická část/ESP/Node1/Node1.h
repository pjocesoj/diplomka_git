#ifndef NODE_1_H_
#define NODE_1_H_

#include "../Endpoint.h"

Endpoint* test()
{
    Endpoint* e1=new Endpoint(GET,"/getValues",0);
    return e1;
}

#endif