#ifndef SERVO_WRAP_H_
#define SERVO_WRAP_H_

class ServoWrapper
{
public:
    ServoWrapper(int pin);

    void SetAngle(int angle);
    int GetAngle();

private:
    int _angle = 0;

};

#endif