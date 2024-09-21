using System.Drawing;

namespace MainNode.Logic.Compile
{
    /// <summary>
    /// přechodová funkce pro zásobníkový automat
    /// </summary>
    internal class TransitionFunc
    {
        public Point Next { get; set; }
        public Action<char,StackValueTypeEnum> Func { get; set; }

        public TransitionFunc(Point next, Action<char, StackValueTypeEnum> func)
        {
            Next = next;
            Func = func;
        }
    }
}
