#include <cstdio>
#include <cstring>
#include "../../ESP/src/Abstract/Serializer.h"

template <typename T>
void Serialize(ValueDto<T>* value, char* buffer, size_t bufferSize) {
    if (buffer == nullptr || bufferSize == 0) {
        return; // Handle invalid buffer
    }

    // Format the ValueDto into the buffer
    int written = snprintf(buffer, bufferSize, 
        "{ \"Type\": \"%d\", \"Name\": \"%s\", \"Value\": \"%d\" }",
        value->GetType(), // Assuming GetType() returns a string or can be converted
        value->Name,     // Assuming Name is a string
				value->Value
        //std::to_string(value->Value).c_str() // Convert Value to string
    );

    // Ensure null-termination if the buffer is too small
    if (written >= static_cast<int>(bufferSize)) {
        buffer[bufferSize - 1] = '\0';
    }
}

