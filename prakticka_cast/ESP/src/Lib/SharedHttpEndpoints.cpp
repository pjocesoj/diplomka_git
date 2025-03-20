#include "SharedHttpEndpoints.h"

#include "../../global.h"
#include "../Abstract/Serializer.h"
#include "../Abstract/Logger.h"
#include "../Abstract/Arduino/LoggerExtend.h"
#include "../Abstract/CommunicationHandler.h"



// handler dotazu na seznam endpontu
void getInfo()
{
    char ret[512];
    SerializeEndpoints(endpoints, ret, 512);
    Log(ret);
    communicationHandler.SendOk(ret);
}

// handler pro root
void handleRootPath()
{
    
    Log("http root");
    char head[512];
    int hl= communicationHandler.HeaderList(head,512);
    Log(head);

    char body[512];
    int bl = communicationHandler.GetBody(body,512);
    Log(body);

    communicationHandler.SendOk("hello world");
}

/**
 * @brief prida serveru root + getinfo, ktere jsou spolecne pro vsechny node
 */
void AddDefaultEndpoints()
{
    communicationHandler.StartListening("/", handleRootPath);
    communicationHandler.StartListening("/getInfo", getInfo);
}

/**
 *  @brief metoda odesilajici hodnoty
 * (volana z jine ktera resi ziskani hodnot)
 * 
 *  @param e endpoint jehoz hodnoty ma odeslat
 */
void sendEndpointValues(EndPointDto *e)
{
    char ret[512];
    SerializeValue(e, ret, 512);
    
    Log(ret);
    communicationHandler.SendOk(ret);
}