#include <cstdio>
#include <cstring>
#include "../Serializer.hpp"
#include "../../../../STM/shared/UsbHandler.h"

void printJSON(char* Buff, int Len)
{
    CDC_Transmit_FS((uint8_t*)Buff, Len);
}

/**
 * @brief Serializes a ValueDto object into a JSON-like string format.
 * @param value Pointer to the ValueDto object to serialize.
 * @param buffer Pointer to the character buffer where the serialized string will be stored.
 * @param bufferSize Size of the buffer in bytes.
 * @param formatedString The format string to use for serialization
 * @tparam T The type of the value contained in the ValueDto object.
 * @author GitHub Copilot
 */
template <typename T>
void Serialize(ValueDto<T>* value, char* buffer, size_t bufferSize, const char* formatedString) {
    if (buffer == nullptr || bufferSize == 0) {
        return; // Handle invalid buffer
    }

    int written = snprintf(buffer, bufferSize,formatedString,
        value->GetType(), // Assuming GetType() returns a string or can be converted
        value->Name,
		value->Value
    );

    // Ensure null-termination if the buffer is too small
    if (written >= static_cast<int>(bufferSize)) {
        buffer[bufferSize - 1] = '\0';
    }
}

void Serialize(ValueDto<int>* value) 
{
    size_t bufferSize = 256; // Adjust size as needed
    char buffer[bufferSize];
    Serialize(value, buffer, bufferSize, "{ \"Type\": \"%d\", \"Name\": \"%s\", \"Value\": %d }");
    printJSON(buffer, strlen(buffer));
}
void Serialize(ValueDto<float>* value) 
{
    size_t bufferSize = 256; // Adjust size as needed
    char buffer[bufferSize];
    Serialize(value, buffer, bufferSize, "{ \"Type\": \"%d\", \"Name\": \"%s\", \"Value\": %.2f }");
    printJSON(buffer, strlen(buffer));
}
void Serialize(ValueDto<bool>* value) 
{
    size_t bufferSize = 256; // Adjust size as needed
    char buffer[bufferSize];
    Serialize(value, buffer, bufferSize, "{ \"Type\": \"%d\", \"Name\": \"%s\", \"Value\": %s }");
    printJSON(buffer, strlen(buffer));
}