#include "DhtWrapper.h"

DhtWrapper::DhtWrapper(uint8_t pin, uint8_t type)
{
  _dht=new DHT(pin, type);
  _dht->begin();
}

ulong DhtWrapper::GetDataAge()
{
  return millis() - _lastDhtRead;
}

bool DhtWrapper::ReadRaw()
{
  _temp = _dht->readTemperature();
  _humid = _dht->readHumidity();
  _lastDhtRead = millis();

  return !isnan(_temp) && !isnan(_humid);
}

bool DhtWrapper::WaitForNewestData()
{
    ulong diff = GetDataAge();
  if(diff<2000 && diff>0)
  {
      delay(2000-diff);
  }
  return ReadRaw();
}