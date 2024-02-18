using HlavniUzel.Komunikace.Enums;
using HlavniUzel.Komunikace.Interfaces;

namespace MainNode.Logic.Do
{
    public class EndPointDo
    {
        public EndPointPath Path { get; set; }

        public List<ValueDo<int>> Ints { get; } = new List<ValueDo<int>>();
        public List<ValueDo<float>> Flots { get; } = new List<ValueDo<float>>();
        public List<ValueDo<bool>> Bools { get; } = new List<ValueDo<bool>>();
    }
}
