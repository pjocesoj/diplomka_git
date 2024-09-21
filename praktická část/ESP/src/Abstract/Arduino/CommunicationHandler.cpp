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