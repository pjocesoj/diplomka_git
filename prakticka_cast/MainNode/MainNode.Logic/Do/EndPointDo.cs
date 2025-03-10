using MainNode.Communication.Enums;
using MainNode.Communication.Interfaces;

namespace MainNode.Logic.Do
{
    public class EndPointDo
    {
        public EndPointPath Path { get; set; }
        public EndPointType Type { get; set; }
        public ValuesDo Values { get; set; }
        public ValuesDo Arguments { get; set; }
        public int? Delay { get; set; }

    }
}
