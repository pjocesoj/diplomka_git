using MainNode.Logic.Enums;
using System.Drawing;

namespace MainNode.Logic.Compile
{
    /// <summary>
    /// přechodová funkce pro zásobníkový automat
    /// </summary>
    internal class TransitionFunc
    {
        public LCStateEnum Next { get; set; }
        public Action<char, LCStateEnum, StackValueTypeEnum?> Func { get; set; }

        public StackValueTypeEnum? PushValue { get; set; }
        public TransitionFunc(LCStateEnum next, Action<char, LCStateEnum, StackValueTypeEnum?> func, StackValueTypeEnum? pushValue)
        {
            Next = next;
            Func = func;
            PushValue = pushValue;
        }
    }
}
