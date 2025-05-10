#include "UsbHandler.h"

  // Define a function pointer for a custom handler
  void (*USB_CustomHandler)(uint8_t *Buf, uint32_t Len) = NULL;

void USB_CDC_RxHandler(uint8_t* Buf, uint32_t Len)
{
    if (USB_CustomHandler != NULL)
    {
        // Call the custom handler if set
        USB_CustomHandler(Buf, Len);
    }
    else
    {
        // Default behavior
        CDC_Transmit_FS(Buf, Len);
    }
}

void USB_CDC_Respond(uint8_t* Buf, uint32_t Len)
{
    CDC_Transmit_FS(Buf, Len);
}

void USB_SetCustomHandler(void (*handler)(uint8_t*, uint32_t))
{
    USB_CustomHandler = handler;
}
