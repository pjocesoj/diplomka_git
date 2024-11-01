using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class RefConstOperation<T,U>:Operation<T>
    {
        public new ValueDo<U> Ref { get; set; }
        public new U Const { get; set; }
        public new Func<U, U, T> Func { get; set; }
        public RefConstOperation(ValueDo<U> valR,U valC, Func<U, U, T> func)
        {
            Ref = valR;
            Const = valC;
            Func = func;
        }
        public override T Execute(T a)
        {
            return Func(Ref.Value, Const);
        }
    }
}
