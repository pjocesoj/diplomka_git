#include "../Lib/Node.h"

#include "../Lib/EndPointDto.h"
#include "../../global.h"
#include "../Lib/SharedHttpEndpoints.h"
#include "../../helpers.h"
#include "../Abstract/Deserializer.h"
#include "../Abstract/CommunicationHandler.h"

#include "../HW/ADC/AdcWrapper.h"
#include "HardwareSerial.h"

#ifdef NODE3

EndPointDto *_get;
EndPointDto *_cmp;

struct Calibration
{
    int min;
    int max;
} typedef Calibration;

int calculateDistance(int x)
{
    const Calibration adc = {0, 1023};
    const Calibration dist = {0, 100};

    const float Y=adc.max-adc.min;
    const float X=dist.max-dist.min;

    float delta = x - adc.min;

    return (int)(dist.min+delta*Y/X);
}

void getDistance()
{
	int raw = ReadADC();
    int distance = calculateDistance(raw);
    _get->Val_Ints[0]->Value = distance;

    Serial.print("Raw: ");
    Serial.print(raw);
    Serial.print(" | ");
    Serial.print(distance);
    Serial.println(" mm");

	sendEndpointValues(_get);
}
void IsFurther()
{
    char body[512];
    communicationHandler.GetBody(body, 512);
    Deserialize(body, _cmp);

    int raw = ReadADC();
    int distance = calculateDistance(raw);
    _cmp->Val_Bools[0]->Value = distance > _cmp->Arg_Ints[0]->Value;

	sendEndpointValues(_cmp);
}
EndPointDto *create_getdistance()
{
	_get = new EndPointDto(GET, "/getDistance");
    _get->Val_Ints.push_back(new ValueDto<int>("distance", 0));

	endpoints.push_back(_get);

    communicationHandler.StartListening(_get->URL, getDistance);
	return _get;
}
EndPointDto *create_isFurther()
{
    _cmp = new EndPointDto(POST, "/isFurther", EP_TYPE_GET);
    _cmp->Arg_Ints.push_back(new ValueDto<int>("distance", 0));
    _cmp->Val_Bools.push_back(new ValueDto<bool>("isFurther", 0));

	endpoints.push_back(_cmp);

    communicationHandler.StartListening(_cmp->URL, IsFurther);
	return _cmp;
}

void NodeInit()
{
	Serial.println("NODE 3");

	EndPointDto *e1 = create_getdistance();
	printEndpoint(e1);
	EndPointDto *e2 = create_isFurther();
	printEndpoint(e2);
}


#endif