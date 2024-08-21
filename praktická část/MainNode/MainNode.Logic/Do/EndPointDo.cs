using MainNode.Communication.Enums;
using MainNode.Communication.Interfaces;

namespace MainNode.Logic.Do
{
    public class EndPointDo
    {
        public EndPointPath Path { get; set; }
        public EndpointType Type { get; set; }
        /*
        public List<ValueDo<int>> Ints { get; } = new List<ValueDo<int>>();
        public List<ValueDo<float>> Flots { get; } = new List<ValueDo<float>>();
        public List<ValueDo<bool>> Bools { get; } = new List<ValueDo<bool>>();
        */
        public ValuesDo Values { get; set; }
        public ValuesDo Arguments { get; set; }
        public int? Delay { get; set; }

    }
}
