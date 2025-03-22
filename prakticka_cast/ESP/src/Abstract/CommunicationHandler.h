#ifndef CommunicationHandler_H_
#define CommunicationHandler_H_

#include <functional>

class CommunicationHandler
{
public:
    CommunicationHandler();
    /**
     * @brief zacne poslouchat dotazy s danou cestou
     * @param url cesta
     * @param callback funkce ktera se zavola pri prijeti dotazu
     */
    void StartListening(const char *url, std::function<void(void)> callback);

    /**
     * @brief odesle odpoved s kodem 200
     * @param data text odpovedi
     */
    void SendOk(const char *data);

    /**
     * @brief odesle odpoved s kodem 500
     * @param data text chyby
     */
    void SendError(const char *data);

    /**
     * @brief kdyz prijde dotaz, tak ho zpracuje <br/>
     * umisteno v loopu
     */
    void Loop();

    /**
     * @brief ziska tedlo dotazu
     * @param ret buffer pro ulozeni dat
     * @param size velikost bufferu
     */
    int GetBody(char*ret, int size);

    /**
     * @brief ziska informace z hlavicky dotazu
     * @param ret buffer pro ulozeni dat
     * @param size velikost bufferu
     */
    int HeaderList(char*ret, int size);

private:

};
#endif