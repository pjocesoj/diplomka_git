#include "SharedMain.hpp"
#include "../../ESP/src/Lib/ValueDto.hpp"

#include "UsbHandler.h"
#include "a__Serializer.cpp"
#include "../../ESP/src/Abstract/Serializer.hpp"

// Test function to be used as a custom USB handler
void TestUSBHandler(uint8_t* Buf, uint32_t Len)
{
    ValueDto<int> *a = new ValueDto<int>("aaa", 10);
    char buffer[256]; // Adjust size as needed
    Serialize(a, buffer, sizeof(buffer));
    USB_CDC_Respond((uint8_t*)buffer, strlen(buffer));
    delete a;
}

void setup()
{
    // Set the custom USB handler
    USB_SetCustomHandler(TestUSBHandler);

    ValueDto<int> *a = new ValueDto<int>("aaa", 10);
    char buffer[256]; // Adjust size as needed
    Serialize(a, buffer, sizeof(buffer));

    ValueDto<float> *b = new ValueDto<float>("bbb", 20.5f);
    Serialize(b, buffer, sizeof(buffer));

    ValueDto<bool> *c = new ValueDto<bool>("ccc", true);
    Serialize(c, buffer, sizeof(buffer));

/*	
	ValueDto<int> *a = new ValueDto<int>("aaa", 10);
	ValueDto<float> *b = new ValueDto<float>("bbb", 20.5f);
	ValueDto<bool> *c = new ValueDto<bool>("ccc", true);
	Serialize(a);
	Serialize(b);
	Serialize(c);
*/

}

void loop()
{
    int i = 0;
}
