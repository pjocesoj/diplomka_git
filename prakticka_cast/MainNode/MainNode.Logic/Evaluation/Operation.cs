using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class Operation<T>
    {
        public ValueDo<T>? Ref { get; protected set; }
        public T? Const { get; protected set; }
        public Func<T, T, T> Func { get; protected set; }

        protected Operation() { }
        public Operation(ValueDo<T> val, Func<T, T, T> func)
        {
            Ref = val;
            Func = func;
        }
        public Operation(T val, Func<T, T, T> func)
        {
            Const = val;
            Func = func;
        }
        public virtual T Execute(T a)
        {
            if (Ref != null) { return Func(a,Ref.Value); }
            return Func(a,Const!);
        }
    }
    public class Operation<T, U> : Operation<T>
    {
        public new ValueDo<U> Ref { get; protected set; }
        public new U Const { get; protected set; }
        public new Func<T, U, T> Func { get; protected set; }
        public Operation(ValueDo<U> val, Func<T, U, T> func)
        {
            Ref = val;
            Func = func;
        }
        public Operation(U val, Func<T, U, T> func)
        {
            Const = val;
            Func = func;
        }
        public override T Execute(T a)
        {
            if (Ref != null) { return Func(a, Ref.Value); }
            return Func(a, Const!);
        }
    }
}
