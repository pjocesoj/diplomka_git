#include "SharedMain.hpp"
#include "../../ESP/src/Lib/ValueDto.hpp"

#include "UsbHandler.h"
#include "a__Serializer.cpp"

void setup()
{
    ValueDto<int> *a = new ValueDto<int>("aaa", 10);
    char buffer[256]; // Adjust size as needed
    Serialize(a, buffer, sizeof(buffer));
    //USB_CDC_TxHandler((uint8_t *)buffer, strlen(buffer));

    ValueDto<float> *b = new ValueDto<float>("bbb", 20.5f);
    Serialize(b, buffer, sizeof(buffer));
    //USB_CDC_TxHandler((uint8_t *)buffer, strlen(buffer));

    ValueDto<bool> *c = new ValueDto<bool>("ccc", true);
    Serialize(c, buffer, sizeof(buffer));
    //USB_CDC_TxHandler((uint8_t *)buffer, strlen(buffer));
}

void loop()
{
    int i = 0;
}
