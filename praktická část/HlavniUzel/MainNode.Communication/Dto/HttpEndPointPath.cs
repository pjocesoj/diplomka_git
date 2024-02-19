using MainNode.Communication.Enums;
using MainNode.Communication.Interfaces;

namespace MainNode.Communication.Dto
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
