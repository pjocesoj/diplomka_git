#ifndef NODE_1_H_
#define NODE_1_H_

#include "../Endpoint.h"
#include "../global.h"
#include "../SharedHttpEndpoints.h"
#include "../helpers.h"

//std::vector<Endpoint *> endpoints;

void getValues()
{
    sendEndpointValues(endpoints[0]);
}

Endpoint *test()
{
    Endpoint *e1 = new Endpoint(GET, "/getValues");
    e1->Ints.push_back(new ValueDto<int>("a", 1));
    e1->Ints.push_back(new ValueDto<int>("b", 2));
    e1->Floats.push_back(new ValueDto<float>("c", 3.14));
    e1->Bools.push_back(new ValueDto<bool>("B1", true));
    endpoints.push_back(e1);

    server.on(e1->URL, getValues);
    return e1;
}

void setValues()
{
  Serial.println("setValue");
  String body=server.arg("plain");
  deserializace(body,endpoints[0]);
  
  server.send(200, "text/plain", "ok"); 
}

Endpoint *test_set()
{
    Endpoint *e2 = new Endpoint(POST, "/setValues");
    e2->Ints.push_back(new ValueDto<int>("a", 1));
    e2->Ints.push_back(new ValueDto<int>("b", 2));
    e2->Floats.push_back(new ValueDto<float>("c", 3.14));
    e2->Bools.push_back(new ValueDto<bool>("B1", true));
    endpoints.push_back(e2);

    server.on(e2->URL, setValues);
    return e2;
}

#endif