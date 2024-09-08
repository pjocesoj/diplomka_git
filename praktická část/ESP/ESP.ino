#include "ESP8266WiFi.h"
#include "ESP8266HTTPClient.h"
#include "ESP8266WebServer.h" //https://github.com/esp8266/ESPWebServer/blob/master/src/ESP8266WebServer.h
#include <ArduinoJson.h>
#include "secret.h" //git update-index --assume-unchanged secret.h
#include "helpers.h"
#include "global.h" //global var
#include "SharedHttpEndpoints.h"
#include "src/Node.h"

ESP8266WebServer server(80);
std::vector<Endpoint*> endpoints;

void setup()
{
  Serial.begin(9600);
  Serial.println("boot");
  AvailableRAM("boot");
  
  NodeInit();
  
  WifiConnect();

  //server.on("/", handleRootPath);
  AddDefaultEndpoints();
  server.begin();
  Serial.println("Server listening");

  const char* c=MillisToTimestemp(millis());
  Serial.println(c);
  Serial.println(strlen(c));
}

void loop()
{
  server.handleClient();
}

void WifiConnect()
{
  Serial.print("Connecting to ");
	Serial.println(ssid);
	WiFi.begin(ssid, password);

  CustomWifiConnecting();
  //pokus se pripoji pomoci custom tak default 1 zkontroluje podminku a pokracuje
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.println("Connecting..");
  }
  Serial.println(WiFi.localIP());
}
