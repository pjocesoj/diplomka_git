using HlavniUzel.Komunikace.Enums;

namespace HlavniUzel.Komunikace.Dto
{
    public class EndPointDto
    {
        public HttpMethodEnum HTTP { get; set; }
        public string URL { get; set; } = "";
        public ValueDto[] Vals { get; set; } = new ValueDto[0];
    }
}
