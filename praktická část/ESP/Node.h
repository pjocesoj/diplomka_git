#ifndef NODE_H_
#define NODE_H_

#include "secret.h"
#include "helpers.h"
#include "ESP8266WiFi.h"


void NodeInit() {
  Serial.begin(9600);
  Serial.println("boot");
  AvailableRAM("boot");


  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) 
  {
    delay(100);
    Serial.println("Connecting..");
  }
  Serial.println(WiFi.localIP());
}

#endif