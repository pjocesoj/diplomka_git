#ifndef DHT_WRAP_H_
#define DHT_WRAP_H_

#include "DHT.h"

// pin 1 data
// pin 2 +5V
// pin 3 GND
class DhtWrapper
{
public:
	DhtWrapper(uint8_t pin, uint8_t type = DHT11);


	/**
	* @brief zavola read a neresi stari dat <br/>
	*(pokud jsou data novejsi nez 2s tak vrati posledni hodnotu)
	*
	* @return zda se podarilo nacist data
	*/
	bool ReadRaw();
    /**
    * @brief vrati stari dat
    */
    ulong GetDataAge();
    /**
    * @brief pocka na nejnovejsi data
    */
    bool WaitForNewestData();

	float GetTemp() { return _temp; }
	float GetHumid() { return _humid; }


private:
	DHT* _dht;

	float _temp = -1;
	float _humid = -1;
	ulong _lastDhtRead = 0;

};

#endif