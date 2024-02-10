using HlavniUzel.Komunikace.Enums;
using HlavniUzel.Komunikace.Interfaces;

namespace HlavniUzel.Komunikace.Dto
{
    public class HttpEndPointPath : EndPointPath
    {
        public HttpMethodEnum HttpMethod { get; set; }
        //public string Path { get; set; }

        public override string ToString()
        {
            return $"{HttpMethod}\n{Path}";
        }
    }
}
