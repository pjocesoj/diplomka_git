#ifndef NODE_1_H_
#define NODE_1_H_

#include "../Endpoint.h"

std::vector<Endpoint *> endpoints;

Endpoint *test()
{
    Endpoint *e1 = new Endpoint(GET, "/getValues");
    e1->Ints.push_back(new ValueDto<int>("a", 1));
    e1->Ints.push_back(new ValueDto<int>("b", 2));
    e1->Floats.push_back(new ValueDto<float>("c", 3.14));
    e1->Bools.push_back(new ValueDto<bool>("B1", true));
    //endpoints.push_back(e1);
    return e1;
}

#endif