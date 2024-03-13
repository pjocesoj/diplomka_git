#ifndef NODE_1_H_
#define NODE_1_H_

#include "../Endpoint.h"

 Endpoint* test()
{
    Endpoint* e1=new Endpoint(GET,"/getValues");
    return e1;
}

#endif