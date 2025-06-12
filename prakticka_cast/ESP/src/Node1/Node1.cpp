#include "../Lib/Node.hpp"

#include "HardwareSerial.h"
#include "../Lib/EndPointDto.hpp"
#include "../../global.hpp"
#include "../Lib/SharedHttpEndpoints.hpp"
#include "../../helpers.hpp"
#include "../Abstract/CommunicationHandler.hpp"

#include "../HW/OLED/OLED.hpp"
#include "../HW/DHT/DhtWrapper.hpp"

#ifdef NODE1

DhtWrapper _dhtWrapper(D4);

EndPointDto *_dhtNew;
EndPointDto *_dhtAny;

void DhtPrint(float temp, float humid)
{
	clearDisplay(0, 21);
	ShowText(temp, 2, 0, 21);
	ShowDeg(2);
	ShowText("C", 2);
	newLine();
	ShowText(humid, 2);
	ShowText(" %", 2);
	ShowText(MillisToTimestemp(millis()), 1, 0, 50);
}

void getDhtValuesNew()
{
	_dhtWrapper.WaitForNewestData();

	float temp = _dhtWrapper.GetTemp();
	float humid = _dhtWrapper.GetHumid();
	long age = _dhtWrapper.GetDataAge();

	_dhtNew->Val_Floats[0]->Value = temp;
	_dhtNew->Val_Floats[1]->Value = humid;
	_dhtNew->Val_Ints[0]->Value = age;

	sendEndpointValues(_dhtNew);

	DhtPrint(temp, humid);
}
void getDhtValuesAny()
{
	_dhtWrapper.ReadRaw();

	float temp = _dhtWrapper.GetTemp();
	float humid = _dhtWrapper.GetHumid();
	long age = _dhtWrapper.GetDataAge();

	_dhtAny->Val_Floats[0]->Value = temp;
	_dhtAny->Val_Floats[1]->Value = humid;
	_dhtAny->Val_Ints[0]->Value = age;

	sendEndpointValues(_dhtAny);

	DhtPrint(temp, humid);
}
EndPointDto *create_getDhtNew()
{
	_dhtNew = new EndPointDto(GET, "/getDhtValuesNew", 2100);
	_dhtNew->Val_Floats.push_back(new ValueDto<float>("temp", 0));
	_dhtNew->Val_Floats.push_back(new ValueDto<float>("humid", 0));
	_dhtNew->Val_Ints.push_back(new ValueDto<int>("age", 0));

	endpoints.push_back(_dhtNew);

    communicationHandler.StartListening(_dhtNew->URL, getDhtValuesNew);
	return _dhtNew;
}
EndPointDto *create_getDhtAny()
{
	_dhtAny = new EndPointDto(GET, "/getDhtValuesAny");
	_dhtAny->Val_Floats.push_back(new ValueDto<float>("temp", 0));
	_dhtAny->Val_Floats.push_back(new ValueDto<float>("humid", 0));
	_dhtAny->Val_Ints.push_back(new ValueDto<int>("age", 0));

	endpoints.push_back(_dhtAny);

    communicationHandler.StartListening(_dhtAny->URL, getDhtValuesAny);
	return _dhtAny;
}

void NodeInit()
{
	Serial.println("NODE 1");

	EndPointDto *e1 = create_getDhtNew();
	printEndpoint(e1);
	EndPointDto *e2 = create_getDhtAny();
	printEndpoint(e2);

	bool oled = OledInit();
	if (!oled)
	{
		Serial.println("OLED failed");
		while (true);
	}
	else
	{
		Serial.println("OLED OK");
	}
}

/**
 * @brief upravena verze pripojovani k Wi-Fi vyuzivajici OLED displej
 */
void WaitToConnect()
{
	while (WiFi.status() != WL_CONNECTED)
	{
		blink_wifi(0, 0);
		Serial.print(".");
		delay(200);
	}

	Serial.println("");
	print_IP();

	showWifiIco(0, 0);
}

#endif
