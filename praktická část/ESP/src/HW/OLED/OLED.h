#ifndef OLED_H_
#define OLED_H_

// https://randomnerdtutorials.com/guide-for-oled-display-with-arduino/
#include <Wire.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64

// 'wifi', 16x16px
const unsigned char wifi_ico[] PROGMEM = {
	0x00, 0x00, 0x00, 0x00, 0x0f, 0xf0, 0x3f, 0xfc, 0x70, 0x0e, 0xe7, 0xe7, 0x1f, 0xf8, 0x38, 0x1c,
	0x03, 0xc0, 0x0f, 0xf0, 0x04, 0x20, 0x01, 0x80, 0x01, 0x80, 0x01, 0x80, 0x00, 0x00, 0x00, 0x00};

bool _showWifiIco = true;

// Declaration for an SSD1306 display connected to I2C (SDA, SCL pins)
Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, -1);

bool OledInit()
{
	if (!display.begin(SSD1306_SWITCHCAPVCC, 0x3C))
	{
		Serial.println(F("SSD1306 allocation failed"));
		return false;
	}
	delay(2000);
	display.clearDisplay();
	display.setTextColor(WHITE);
	display.display();
	return true;
}

void blink_wifi(int x, int y)
{
	if (_showWifiIco)
	{
		display.drawBitmap(x, y, wifi_ico, 16, 16, WHITE);
	}
	else
	{
		display.fillRect(x, y, 16, 16, BLACK);
	}
	_showWifiIco = !_showWifiIco;
	display.display();
}

void print_IP()
{
	display.setTextSize(1);
	display.setCursor(20, 5);
	// Display static text
	display.println(WiFi.localIP());
	display.display();
}

void showWifiIco(int x, int y)
{
	_showWifiIco = true;
	blink_wifi(x, y);
}

template <typename T>
void ShowText(T text, int8_t size)
{
	display.setTextSize(size);
	display.print(text);
	display.display();
}

template <typename T>
void ShowText(T text, int8_t size, int x, int y)
{
	display.setCursor(x, y);
	ShowText(text, size);
}

void ShowDeg(int8_t size)
{
	display.setTextSize(size);
	display.print((char)247); // stupen (ma jiny kod nez ASCII)
	display.display();
}
void newLine()
{
	display.println();
}
void clearDisplay(int x1,int y1,int x2,int y2)
{
	display.fillRect(x1, y1, x2, y2, BLACK);
	display.display();
}
void clearDisplay(int x1,int y1)
{
	display.fillRect(x1, y1, SCREEN_WIDTH, SCREEN_HEIGHT, BLACK);
	display.display();
}

#endif