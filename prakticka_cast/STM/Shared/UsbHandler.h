#ifndef USB_HANDLER_H_
#define USB_HANDLER_H_

#include <stdint.h>

#ifdef __cplusplus
extern "C"
{
#endif

#include "../F303/Node_STM32F3Disco/USB_DEVICE/App/usbd_cdc_if.h"

  void USB_CDC_RxHandler(uint8_t *Buf, uint32_t Len);
  void USB_CDC_Respond(uint8_t *Buf, uint32_t Len);

  void USB_SetCustomHandler(void (*handler)(uint8_t *, uint32_t));

#ifdef __cplusplus
}
#endif

/*
usbd_Cdc_ it.c line 259

static int8_t CDC_Receive_FS(uint8_t* Buf, uint32_t *Len)
{
  USBD_CDC_SetRxBuffer(&hUsbDeviceFS, &Buf[0]);
  USBD_CDC_ReceivePacket(&hUsbDeviceFS);
  USB_CDC_RxHandler(UserRxBufferFS, *Len);
  memset(UserRxBufferFS, '\0', *Len);
  return (USBD_OK);
}
*/

#endif
