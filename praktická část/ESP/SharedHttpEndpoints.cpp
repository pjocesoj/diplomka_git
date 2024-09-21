#include "SharedHttpEndpoints.h"

#include "ESP8266WebServer.h"
#include "global.h"
#include "src/Abstract/Serializer.h"
#include "src/Abstract/Logger.h"
#include "src/Abstract/Arduino/LoggerExtend.h"
#include "src/Abstract/CommunicationHandler.h"



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
    /*
    Log("http root");
    int headers = server.headers();
    for (int i = 0; i < headers; i++)
    {
        String val = server.header(i);
        String nam = server.headerName(i);
        LogExt(val,"=",nam);
    }

    String body = server.arg("plain");
    LogExt(body);
*/
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