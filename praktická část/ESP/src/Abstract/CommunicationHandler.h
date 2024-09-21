#ifndef CommunicationHandler_H_
#define CommunicationHandler_H_

class CommunicationHandler
{
public:
    CommunicationHandler();

    void StartListening(const char *url, std::function<void(void)> callback);

    void SendOk(const char *data);

    void SendError(const char *data);

    void loop();

private:

};
#endif