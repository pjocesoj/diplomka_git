#include "SharedHttpEndpoints.h"

#include "ESP8266WebServer.h"
#include "global.h"
#include "src/Abstract/Serializer.h"

// handler dotazu na seznam endpontu
void getInfo()
{
    char ret[512];
    SerializeEndpoints(endpoints, ret, 512);
    Serial.println(ret);
    server.send(200, "text/plain", ret);
}

// handler pro root
void handleRootPath()
{
    Serial.println("http root");
    int headers = server.headers();
    for (int i = 0; i < headers; i++)
    {
        String val = server.header(i);
        String nam = server.headerName(i);
        Serial.print(nam);
        Serial.print("=");
        Serial.println(val);
    }

    String body = server.arg("plain");
    Serial.println(body);

    server.send(200, "text/plain", "Hello world");
}

/**
 * @brief prida serveru root + getinfo, ktere jsou spolecne pro vsechny node
 */
void AddDefaultEndpoints()
{
    server.on("/", handleRootPath);
    server.on("/getInfo", getInfo);
}

/**
 *  @brief metoda odesilajici hodnoty
 * (volana z jine ktera resi ziskani hodnot)
 * 
 *  @param e endpoint jehoz hodnoty ma odeslat
 */
void sendEndpointValues(Endpoint *e)
{
    char ret[512];
    SerializeValue(e, ret, 512);
    
    Serial.println(ret);
    server.send(200, "text/plain", ret);
}