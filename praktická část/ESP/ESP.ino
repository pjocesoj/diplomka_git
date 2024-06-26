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
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.println("Connecting..");
  }
  Serial.println(WiFi.localIP());

  //server.on("/", handleRootPath);
  AddDefaultEndpoints();
  server.begin();
  Serial.println("Server listening");
}

void loop()
{
  server.handleClient();
}
