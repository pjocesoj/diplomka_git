#ifndef CommunicationHandler_H_
#define CommunicationHandler_H_

#include <functional>

class CommunicationHandler
{
public:
    CommunicationHandler();

    void StartListening(const char *url, std::function<void(void)> callback);

    void SendOk(const char *data);

    void SendError(const char *data);

    void loop();

    int getBody(char*ret, int size);

    int HeaderList(char*ret, int size);

private:

};
#endif