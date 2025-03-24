#include "ServoWrapper.h"

#include <Servo.h>
int val = 0;
Servo _servo;  // create servo object to control a servo
ServoWrapper::ServoWrapper(int pin)
{
    _servo.attach(pin, 500, 2400);
    _servo.write(0);
}

void ServoWrapper::SetAngle(int angle)
{
    _servo.write(angle);

}

int ServoWrapper::GetAngle()
{
    return _servo.read();
}