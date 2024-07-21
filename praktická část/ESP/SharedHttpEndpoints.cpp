#include "SharedHttpEndpoints.h"

#include "ESP8266WebServer.h"
#include "global.h"

// handler dotazu na seznam endpontu
void getInfo()
{
    JsonDocument doc;
    JsonArray EPs = doc.to<JsonArray>();
    for (auto &obj : endpoints)
    {
        //JsonObject nestedJsonObject = EPs.createNestedObject();
        JsonObject nestedJsonObject = EPs.add<JsonObject>();
        obj->Serialize_info(nestedJsonObject);
    }

    String ret;
    serializeJson(doc, ret);
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
    JsonDocument doc;
    JsonObject jsonObject = doc.to<JsonObject>();
    e->Serialize_values(jsonObject);

    String ret;
    serializeJson(doc, ret);
    Serial.println(ret);
    server.send(200, "text/plain", ret);
}