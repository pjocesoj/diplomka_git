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
    e1->Val_Ints.push_back(new ValueDto<int>("ret", 0));
    endpoints.push_back(e1);

    communicationHandler.StartListening(e1->URL, SetAngle);
    _setAngle = e1;
    return e1;
}

EndPointDto* _get;
EndPointDto* _set;

void getValues()
{
    sendEndpointValues(_get);
}

EndPointDto *test_get()
{
    EndPointDto *e1 = new EndPointDto(GET, "/getValues");
    e1->Val_Ints.push_back(new ValueDto<int>("a", 10));
    e1->Val_Ints.push_back(new ValueDto<int>("b", 20));
    endpoints.push_back(e1);

    communicationHandler.StartListening(e1->URL, getValues);
    _get = e1;
    return e1;
}

void setValues()
{
    
    Serial.println("setValue");

char body[512];
    int bl = communicationHandler.GetBody(body, 512);
    Deserialize(body, _set);

    printEndpoint(_set);

    communicationHandler.SendOk("ok");
}
EndPointDto *test_set()
{
    EndPointDto *e2 = new EndPointDto(POST, "/setValues", EP_TYPE_SET);
    e2->Arg_Ints.push_back(new ValueDto<int>("a", 1));
    e2->Arg_Ints.push_back(new ValueDto<int>("b", 2));
    e2->Arg_Floats.push_back(new ValueDto<float>("c", 3.14));
    e2->Arg_Bools.push_back(new ValueDto<bool>("B1", true));
    endpoints.push_back(e2);
    communicationHandler.StartListening(e2->URL, setValues);
    _set = e2;
    return e2;
}

void NodeInit()
{
    Serial.println("NODE 2");

    EndPointDto *e1 = test_get();
    printEndpoint(e1);
    EndPointDto *e2 = test_set();
    printEndpoint(e2);

    EndPointDto *e3 = create_setAngle();
    printEndpoint(e3);
}

#endif
