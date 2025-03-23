#include "ServoWrapper.h"

#include <Servo.h>
int val = 0;
Servo _servo;  // create servo object to control a servo
ServoWrapper::ServoWrapper(int pin)
{
    _servo.attach(pin, 500, 2400);
}

void ServoWrapper::SetAngle(int angle)
{
    _servo.write(angle);
    delay(500);  // waits for the servo to get there

    _angle = angle;
}

int ServoWrapper::GetAngle()
{
    return _servo.read();
}