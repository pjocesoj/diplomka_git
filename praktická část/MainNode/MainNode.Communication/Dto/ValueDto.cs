using MainNode.Communication.Enums;

namespace MainNode.Communication.Dto
{
    public class ValueDto
    {
        public string Name { get; set; } = "val";
        public ValType Type { get; set; } = ValType.INT;
    }
}
