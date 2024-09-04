#include "../Node.h"

#include "HardwareSerial.h"
#include "../../Endpoint.h"
#include "../../global.h"
#include "../../SharedHttpEndpoints.h"
#include "../../helpers.h"

#ifdef NODE2

void getValues()
{
    sendEndpointValues(endpoints[0]);
}

Endpoint *test_get()
{
    Endpoint *e1 = new Endpoint(GET, "/getValues");
    e1->Ints.push_back(new ValueDto<int>("a", 10));
    e1->Ints.push_back(new ValueDto<int>("b", 20));
    endpoints.push_back(e1);

    server.on(e1->URL, getValues);
    return e1;
}

void NodeInit()
{
    Serial.println("NODE 2");

    Endpoint *e1 = test_get();
    printEndpoint(e1);
}

//nepovedlo se mi definovak weak v headeru
void CustomWifiConnecting(){}
#endif
