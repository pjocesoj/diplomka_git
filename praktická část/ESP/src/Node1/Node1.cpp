#include "../Node.h"

#include "HardwareSerial.h"
#include "../../Endpoint.h"
#include "../../global.h"
#include "../../SharedHttpEndpoints.h"
#include "../../helpers.h"

#include "../HW/OLED/OLED.h"
#include "../HW/DHT/DhtWrapper.h"

#ifdef NODE1

DhtWrapper _dhtWrapper(D4);

Endpoint* _dhtNew;
Endpoint* _dhtAny;

void getDhtValuesNew()
{
	_dhtWrapper.WaitForNewestData();

	_dhtNew->Floats[0]->Value = _dhtWrapper.GetTemp();
	_dhtNew->Floats[1]->Value = _dhtWrapper.GetHumid();
	_dhtNew->Ints[0]->Value = _dhtWrapper.GetDataAge();

	sendEndpointValues(_dhtNew);
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

	clearDisplay(0,21);
	ShowText(temp,2,0,21);
	ShowDeg(2);
	ShowText("C",2);
	newLine();
	ShowText(humid,2);
	ShowText(" %",2);
	ShowText(MillisToTimestemp(millis()),1,0,50);
}
Endpoint* create_getDhtNew()
{
	_dhtNew = new Endpoint(GET, "/getDhtValuesNew");
	_dhtNew->Floats.push_back(new ValueDto<float>("temp", 0));
	_dhtNew->Floats.push_back(new ValueDto<float>("humid", 0));
	_dhtNew->Ints.push_back(new ValueDto<int>("age", 0));

	endpoints.push_back(_dhtNew);

	server.on(_dhtNew->URL, getDhtValuesNew);
	return _dhtNew;
}
Endpoint* create_getDhtAny()
{
	_dhtAny = new Endpoint(GET, "/getDhtValuesAny");
	_dhtAny->Floats.push_back(new ValueDto<float>("temp", 0));
	_dhtAny->Floats.push_back(new ValueDto<float>("humid", 0));
	_dhtAny->Ints.push_back(new ValueDto<int>("age", 0));

	endpoints.push_back(_dhtAny);

	server.on(_dhtAny->URL, getDhtValuesAny);
	return _dhtAny;
}

void getValues()
{
	sendEndpointValues(endpoints[0]);
}

Endpoint* test_get()
{
	Endpoint* e1 = new Endpoint(GET, "/getValues");
	e1->Ints.push_back(new ValueDto<int>("a", 1));
	e1->Ints.push_back(new ValueDto<int>("b", 2));
	e1->Floats.push_back(new ValueDto<float>("c", 3.14));
	e1->Bools.push_back(new ValueDto<bool>("B1", true));
	endpoints.push_back(e1);

	server.on(e1->URL, getValues);
	return e1;
}

void setValues()
{
	Serial.println("setValue");
	String body = server.arg("plain");
	deserializeDTO(body, endpoints[0]);

	server.send(200, "text/plain", "ok");
}

Endpoint* test_set()
{
	Endpoint* e2 = new Endpoint(POST, "/setValues");
	e2->Ints.push_back(new ValueDto<int>("a", 1));
	e2->Ints.push_back(new ValueDto<int>("b", 2));
	e2->Floats.push_back(new ValueDto<float>("c", 3.14));
	e2->Bools.push_back(new ValueDto<bool>("B1", true));
	endpoints.push_back(e2);

	server.on(e2->URL, setValues);
	return e2;
}



void NodeInit()
{
	Serial.println("NODE 1");

	Endpoint* e1 = test_get();
	printEndpoint(e1);
	Endpoint* e2 = test_set();
	printEndpoint(e2);
	Endpoint* e3 = create_getDhtNew();
	printEndpoint(e3);
	Endpoint* e4 = create_getDhtAny();
	printEndpoint(e4);

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

void CustomWifiConnecting()
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
