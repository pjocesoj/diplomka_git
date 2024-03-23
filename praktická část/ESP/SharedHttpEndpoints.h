#ifndef SHARED_HTTP_H_
#define SHARED_HTTP_H_

#include "ESP8266WebServer.h"

ESP8266WebServer server(80);
/*void getInfo()
{
  DynamicJsonDocument doc(1024);
  JsonArray EPs = doc.to<JsonArray>();
  for (auto& obj : endpoints) 
  {
    JsonObject nestedJsonObject = EPs.createNestedObject();
    obj->Serialize_info(nestedJsonObject);
  }

  String ret;
  serializeJson(doc, ret);
  Serial.println(ret);
  server.send(200, "text/plain", ret);
}*/

void handleRootPath() {
  Serial.println("http root");
  int headers = server.headers();
  for (int i = 0; i < headers; i++) {
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

#endif