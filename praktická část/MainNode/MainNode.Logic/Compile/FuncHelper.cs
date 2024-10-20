using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic.Compile
{
    internal static class FuncHelper
    {
        #region value value
        private static void AddFuncion<T, U, V>(Delegate f, ValueDo<T> A, ValueDo<U> B, Flow<V> R) where T : struct where U : struct where V : struct
        {
            //dočasné řešení než předělám Operatition
            if (typeof(T) == typeof(U) && typeof(U) == typeof(V))
            {
                var B_t = (ValueDo<T>)(object)B;
                var R_t = (Flow<T>)(object)R;
                R_t.Operations.Add(new Operation<T>(B_t, (Func<T, T, T>)f));
            }
            var func = (Func<T, U, V>)f;
        }
        private static void AddFuncion<U, V>(Delegate f, ValueDo A, ValueDo<U> B, Flow<V> R) where U : struct where V : struct
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
        #endregion
        #region value flow
        private static void AddFuncion<T, U, V>(Delegate f, ValueDo<T> A, FlowResult<U> B, Flow<V> R) where T : struct where U : struct where V : struct
        {
            //dočasné řešení než předělám Operatition
            if (typeof(U) == typeof(V))
            {
                var R_t = (Flow<T>)(object)R;
                R_t.Operations.Add(new SubflowOperation<T, U>(B, (Func<U, T, T>)f));
            }
            var func = (Func<T, U, V>)f;
        }
        private static void AddFuncion<U, V>(Delegate f, ValueDo A, FlowResult<U> B, Flow<V> R) where U : struct where V : struct
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
        public static void AddFuncion<V>(Delegate f, ValueDo A, FlowResult B, Flow<V> R) where V : struct
        {
            switch (B)
            {
                case FlowResult<int>:
                    AddFuncion(f, A, (FlowResult<int>)B, R); break;
                case FlowResult<float>:
                    AddFuncion(f, A, (FlowResult<float>)B, R); break;
                case FlowResult<bool>:
                    AddFuncion(f, A, (FlowResult<bool>)B, R); break;
            }
        }
        public static void AddFuncion(Delegate f, ValueDo A, FlowResult B, Flow R)
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
        #endregion
        #region flow flow
        private static void AddFuncion<T, U, V>(Delegate f, FlowResult<T> A, FlowResult<U> B, Flow<V> R) where T : struct where U : struct where V : struct
        {
            R.Operations.Add(new MergeflowOperation<T, U, V>(A, B, (Func<T, U, V>)f));
        }
        private static void AddFuncion<U, V>(Delegate f, FlowResult A, FlowResult<U> B, Flow<V> R) where U : struct where V : struct
        {
            switch (A)
            {
                case FlowResult<int>:
                    AddFuncion(f, (FlowResult<int>)A, B, R); break;
                case FlowResult<float>:
                    AddFuncion(f, (FlowResult<float>)A, B, R); break;
                case FlowResult<bool>:
                    AddFuncion(f, (FlowResult<bool>)A, B, R); break;
            }
        }
        public static void AddFuncion<V>(Delegate f, FlowResult A, FlowResult B, Flow<V> R) where V : struct
        {
            switch (B)
            {
                case FlowResult<int>:
                    AddFuncion(f, A, (FlowResult<int>)B, R); break;
                case FlowResult<float>:
                    AddFuncion(f, A, (FlowResult<float>)B, R); break;
                case FlowResult<bool>:
                    AddFuncion(f, A, (FlowResult<bool>)B, R); break;
            }
        }
        public static void AddFuncion(Delegate f, FlowResult A, FlowResult B, Flow R)
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
        #endregion
        #region flow value
        private static void AddFuncion<T, U, V>(Delegate f, FlowResult<T> A, ValueDo<U> B, Flow<V> R) where T : struct where U : struct where V : struct
        {
            //dočasné řešení než předělám Operatition
            if (typeof(T) == typeof(U) && typeof(U) == typeof(V))
            {
                var B_t = (ValueDo<T>)(object)B;
                var R_t = (Flow<T>)(object)R;
                R_t.Operations.Add(new Operation<T>(B_t, (Func<T, T, T>)f));
            }
            var func = (Func<T, U, V>)f;
        }
        private static void AddFuncion<U, V>(Delegate f, FlowResult A, ValueDo<U> B, Flow<V> R) where U : struct where V : struct
        {
            switch (A)
            {
                case FlowResult<int>:
                    AddFuncion(f, (FlowResult<int>)A, B, R); break;
                case FlowResult<float>:
                    AddFuncion(f, (FlowResult<float>)A, B, R); break;
                case FlowResult<bool>:
                    AddFuncion(f, (FlowResult<bool>)A, B, R); break;
            }
        }
        public static void AddFuncion<V>(Delegate f, FlowResult A, ValueDo B, Flow<V> R) where V : struct
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
        public static void AddFuncion(Delegate f, FlowResult A, ValueDo B, Flow R)
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
        #endregion
    }
}
