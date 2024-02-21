using MainNode.Communication.Enums;

namespace MainNode.Communication.Dto
{
    public class EndPointDto
    {
        public HttpMethodEnum HTTP { get; set; }
        public EndpointType Type { get; set; }
        public string URL { get; set; } = "";
        public ValueDto[] Vals { get; set; } = new ValueDto[0];
        public ValueDto[] Args { get; set; } = new ValueDto[0];
    }
}
