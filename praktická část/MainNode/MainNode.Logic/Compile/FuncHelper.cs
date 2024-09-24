using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic.Compile
{
    internal static class FuncHelper
    {
        private static void AddFuncion<T, U, V>(Delegate f, ValueDo<T> A, ValueDo<U> B, Flow<V> R) where T : struct where U : struct where V : struct
        {
            var func = (Func<T, U, V>)f;
        }
        private static void AddFuncion<U,V>(Delegate f, ValueDo A, ValueDo<U> B, Flow<V> R) where U : struct where V : struct
        {
            switch (A)
            {
                case ValueDo<int>:
                    AddFuncion(f, (ValueDo<int>)A, B, R); break;
                case ValueDo<float>:
                    AddFuncion(f, (ValueDo<float>)A, B, R); break;
                case ValueDo<bool>:
                    AddFuncion(f, (ValueDo<bool>)A, B, R); break;
            }
        }
        public static void AddFuncion<V>(Delegate f, ValueDo A, ValueDo B, Flow<V> R) where V : struct
        {
            switch (B)
            {
                case ValueDo<int>:
                    AddFuncion(f, A, (ValueDo<int>)B, R); break;
                case ValueDo<float>:
                    AddFuncion(f, A, (ValueDo<float>)B, R); break;
                case ValueDo<bool>:
                    AddFuncion(f, A, (ValueDo<bool>)B, R); break;
            }
        }
        public static void AddFuncion(Delegate f, ValueDo A, ValueDo B, Flow R)
        {
            switch (R)
            {
                case Flow<int>:
                    AddFuncion(f, A, B, (Flow<int>)R); break;
                case Flow<float>:
                    AddFuncion(f, A, B, (Flow<float>)R); break;
                case Flow<bool>:
                    AddFuncion(f, A, B, (Flow<bool>)R); break;
            }
        }
    }
}
