using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class Operation<T>
    {
        public ValueDo<T>? Ref { get; set; }
        public T? Const { get; set; }
        public Func<T, T, T> Func { get; set; }

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
        public virtual T Execute(T b)
        {
            if (Ref != null) { return Func(Ref.Value, b); }
            return Func(Const!, b);
        }
    }
}
