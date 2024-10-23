using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class SubflowOperation<T, U> : Operation<T> where T : struct where U : struct
    {
        public FlowResult<U> Subflow { get; set; }
        public new Func<T, U, T> Func { get; set; }
        public SubflowOperation(FlowResult<U> flow, Func<T, U, T> func)
        {
            Subflow = flow;
            Func = func;
        }
        public override T Execute(T a)
        {
            var temp = Subflow.Value;
            return Func(a,temp.Value);
        }
    }
}
