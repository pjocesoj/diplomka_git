#ifndef NODE_H_
#define NODE_H_

void AvailableRAM(char* const title)
{
  Serial.print(title);

  Serial.print("\t Free heap: ");
  Serial.print(ESP.getFreeHeap());

  Serial.print("\t stack: ");
  Serial.println(ESP.getFreeContStack());
}

void NodeInit()
{
  Serial.begin(9600);
  Serial.println("boot");
  AvailableRAM("boot");
}

#endif