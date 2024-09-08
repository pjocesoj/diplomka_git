#include "../Node.h"

#include "HardwareSerial.h"
#include "../../Endpoint.h"
#include "../../global.h"
#include "../../SharedHttpEndpoints.h"
#include "../../helpers.h"

#include "../HW/OLED/OLED.h"

#ifdef NODE1

void getValues()
{
	sendEndpointValues(endpoints[0]);
}

Endpoint *test_get()
{
    Endpoint *e1 = new Endpoint(GET, "/getValues");
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

Endpoint *test_set()
{
    Endpoint *e2 = new Endpoint(POST, "/setValues");
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

    Endpoint *e1 = test_get();
	printEndpoint(e1);
    Endpoint *e2 = test_set();
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
