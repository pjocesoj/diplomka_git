#include "Node.hpp"
#include "../Abstract/Serializer.hpp"
#include "../Abstract/Logger.hpp"

void printEndpoint(EndPointDto *ep)
{
    char ret[512];
    Serialize(ep, ret, 512);
    Log(ret);
}