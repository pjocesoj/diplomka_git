#include "UsbHandler.h"

void USB_CDC_RxHandler(uint8_t* Buf, uint32_t Len)
{
    CDC_Transmit_FS(Buf, Len);
}
