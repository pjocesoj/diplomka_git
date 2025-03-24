#include "../Lib/Node.h"

#include "HardwareSerial.h"
#include "../Lib/EndPointDto.h"
#include "../../global.h"
#include "../Lib/SharedHttpEndpoints.h"
#include "../../helpers.h"
#include "../Abstract/Deserializer.h"
#include "../Abstract/CommunicationHandler.h"

#include "../HW/Servo/ServoWrapper.h"

#ifdef NODE2

ServoWrapper _servoWrapper(D5);

EndPointDto* _getAngle;
EndPointDto* _setAngle;

void SetAngle()
{
    char body[512];
    int bl = communicationHandler.GetBody(body, 512);
    Deserialize(body, _setAngle);

    int deg = _setAngle->Arg_Ints[0]->Value;
    _servoWrapper.SetAngle(deg);

	int ret = _servoWrapper.GetAngle();
    _setAngle->Val_Ints[0]->Value = ret;

	sendEndpointValues(_setAngle);
}

EndPointDto *create_setAngle()
{
    EndPointDto *e1 = new EndPointDto(POST, "/setAngle", EP_TYPE_SET);
    e1->Arg_Ints.push_back(new ValueDto<int>("angle", 0));
    e1->Val_Ints.push_back(new ValueDto<int>("deg", 0));
    endpoints.push_back(e1);

    communicationHandler.StartListening(e1->URL, SetAngle);
    _setAngle = e1;
    return e1;
}

void GetAngle()
{
	int ret = _servoWrapper.GetAngle();
    _setAngle->Val_Ints[0]->Value = ret;

	sendEndpointValues(_setAngle);
}

EndPointDto *create_getAngle()
{
    EndPointDto *e1 = new EndPointDto(GET, "/getAngle");
    e1->Val_Ints.push_back(new ValueDto<int>("deg", 0));
    endpoints.push_back(e1);

    communicationHandler.StartListening(e1->URL, SetAngle);
    _getAngle = e1;
    return e1;
}

void NodeInit()
{
    Serial.println("NODE 2");

    EndPointDto *e1 = create_setAngle();
    printEndpoint(e1);

    EndPointDto *e2 = create_getAngle();
    printEndpoint(e2);
}

#endif
