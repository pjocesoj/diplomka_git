#include "../Node.h"

#include "HardwareSerial.h"
#include "../Lib/EndPointDto.h"
#include "../../global.h"
#include "../../SharedHttpEndpoints.h"
#include "../../helpers.h"

#include "../HW/OLED/OLED.h"
#include "../HW/DHT/DhtWrapper.h"

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

	_dhtNew->Floats[0]->Value = temp;
	_dhtNew->Floats[1]->Value = humid;
	_dhtNew->Ints[0]->Value = age;

	sendEndpointValues(_dhtNew);

	DhtPrint(temp, humid);
}
void getDhtValuesAny()
{
	_dhtWrapper.ReadRaw();

	float temp = _dhtWrapper.GetTemp();
	float humid = _dhtWrapper.GetHumid();
	long age = _dhtWrapper.GetDataAge();

	_dhtAny->Floats[0]->Value = temp;
	_dhtAny->Floats[1]->Value = humid;
	_dhtAny->Ints[0]->Value = age;

	sendEndpointValues(_dhtAny);

	DhtPrint(temp, humid);
}
EndPointDto *create_getDhtNew()
{
	_dhtNew = new EndPointDto(GET, "/getDhtValuesNew", 2100);
	_dhtNew->Floats.push_back(new ValueDto<float>("temp", 0));
	_dhtNew->Floats.push_back(new ValueDto<float>("humid", 0));
	_dhtNew->Ints.push_back(new ValueDto<int>("age", 0));

	endpoints.push_back(_dhtNew);

	server.on(_dhtNew->URL, getDhtValuesNew);
	return _dhtNew;
}
EndPointDto *create_getDhtAny()
{
	_dhtAny = new EndPointDto(GET, "/getDhtValuesAny");
	_dhtAny->Floats.push_back(new ValueDto<float>("temp", 0));
	_dhtAny->Floats.push_back(new ValueDto<float>("humid", 0));
	_dhtAny->Ints.push_back(new ValueDto<int>("age", 0));

	endpoints.push_back(_dhtAny);

	server.on(_dhtAny->URL, getDhtValuesAny);
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
