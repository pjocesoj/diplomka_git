#include "ESP8266WiFi.h"
#include "ESP8266HTTPClient.h"
#include "ESP8266WebServer.h" //https://github.com/esp8266/ESPWebServer/blob/master/src/ESP8266WebServer.h
#include <ArduinoJson.h>
#include "secret.hpp" //git update-index --assume-unchanged secret.h
#include "helpers.hpp"
#include "global.hpp" //global var
#include "src/Lib/SharedHttpEndpoints.hpp"
#include "src/Lib/Node.hpp"

#include "src/Abstract/CommunicationHandler.hpp"


//ESP8266WebServer server(80);
CommunicationHandler communicationHandler;
std::vector<EndPointDto*> endpoints;

void setup()
{
  Serial.begin(9600);
  Serial.println("boot");
  AvailableRAM("boot");
  
  NodeInit();

  WifiConnect();

  //server.on("/", handleRootPath);
  AddDefaultEndpoints();
  //server.begin();
  Serial.println("Server listening");

  const char* c=MillisToTimestemp(millis());
  Serial.println(c);
  Serial.println(strlen(c));
}

void loop()
{
  communicationHandler.Loop();
}

/**
 * @brief defaultni pripojovani k Wi-Fi vyuzivajici konzoli
 */
__attribute__((weak)) void WaitToConnect()
{
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.println("Connecting...");
  }
}

void WifiConnect()
{
  Serial.print("Connecting to ");
	Serial.println(ssid);
	WiFi.begin(ssid, password);

  WaitToConnect();//weak ktery muze byt prepsan

  Serial.println(WiFi.localIP());
}
