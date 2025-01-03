#include "../Logger.h"
#include "LoggerExtend.h"
#include "HardwareSerial.h"

void Log(const char* text)
{
    Serial.println(text);
}
void Log(char* text,int len)
{
    Serial.println(text);
}
void Log(const char* A, const char* sep, const char* B)
{
    Serial.print(A);
    Serial.print(sep);
    Serial.println(B);
}

/*
*---------------------------------- LoggerExtend (arduino) ----------------------------------
*/
void LogExt(String A, const char* sep, String B)
{
    Serial.print(A);
    Serial.print(sep);
    Serial.println(B);
}
void LogExt(String text)
{
    Serial.println(text);
}
