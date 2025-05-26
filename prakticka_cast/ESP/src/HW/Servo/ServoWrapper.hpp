#ifndef SERVO_WRAP_H_
#define SERVO_WRAP_H_

#include <Servo.h>

class ServoWrapper
{
public:
    ServoWrapper(int pin);

    void SetAngle(int angle);
    int GetAngle();

private:
    Servo _servo;  
};

#endif