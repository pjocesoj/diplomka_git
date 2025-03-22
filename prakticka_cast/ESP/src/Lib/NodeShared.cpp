#include "Node.h"
#include "../Abstract/Serializer.h"
#include "../Abstract/Logger.h"

void printEndpoint(EndPointDto *ep)
{
    char ret[512];
    Serialize(ep, ret, 512);
    Log(ret);
}