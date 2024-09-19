#include "../Node.h"

#include "HardwareSerial.h"
#include "../Lib/EndPointDto.h"
#include "../../global.h"
#include "../../SharedHttpEndpoints.h"
#include "../../helpers.h"

#ifdef NODE2

void getValues()
{
    sendEndpointValues(EndPointDto[0]);
}

EndPointDto *test_get()
{
    EndPointDto *e1 = new EndPointDto(GET, "/getValues");
    e1->Ints.push_back(new ValueDto<int>("a", 10));
    e1->Ints.push_back(new ValueDto<int>("b", 20));
    endpoints.push_back(e1);

    server.on(e1->URL, getValues);
    return e1;
}

void NodeInit()
{
    Serial.println("NODE 2");

    EndPointDto *e1 = test_get();
    printEndpoint(e1);
}

#endif
