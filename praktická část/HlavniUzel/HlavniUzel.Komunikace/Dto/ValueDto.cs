using HlavniUzel.Komunikace.Enums;

namespace HlavniUzel.Komunikace.Dto
{
    public class ValueDto
    {
        public string Name { get; set; } = "val";
        public ValType Type { get; set; } = ValType.INT;
    }
}
