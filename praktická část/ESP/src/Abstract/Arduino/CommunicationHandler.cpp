#include "../CommunicationHandler.h"
#include "ESP8266WebServer.h"

ESP8266WebServer server(80);

CommunicationHandler::CommunicationHandler()
{
    server.begin();
}

void CommunicationHandler::StartListening(const char* url,std::function<void(void)> callback)
{
    server.on(url, callback);
}

void CommunicationHandler::SendOk(const char* data)
{
    server.send(200, "text/plain", data);
}

void CommunicationHandler::SendError(const char* data)
{
    server.send(500, "text/plain", data);
}

void CommunicationHandler::loop()
{
    server.handleClient();
}

int CommunicationHandler::getBody(char* ret, int size)
{
    String body = server.arg("plain");

    strncpy(ret, body.c_str(), size - 1);
    ret[size - 1] = '\0';

    return strlen(ret);
}

int CommunicationHandler::HeaderList(char*ret, int size)
{
    String headerList = "";
    server.collectHeaders();
    int headers = server.headers();
    for (int i = 0; i < headers; i++)
    {
        String val = server.header(i);
        String nam = server.headerName(i);
        headerList.concat(val);
        headerList.concat("=");
        headerList.concat(nam);
        headerList.concat("\n");
    }

    strncpy(ret, headerList.c_str(), size - 1);
    ret[size - 1] = '\0';

    return strlen(ret);
}