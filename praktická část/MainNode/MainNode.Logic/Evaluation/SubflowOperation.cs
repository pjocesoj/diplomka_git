using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class SubflowOperation<T, U> : Operation<T> where T : struct where U : struct
    {
        public FlowResult<U> Subflow { get; set; }
        public new Func<U, T, T> Func { get; set; }
        public SubflowOperation(FlowResult<U> flow, Func<U, T, T> func)
        {
            Subflow = flow;
            Func = func;
        }
        public override T Execute(T b)
        {
            var temp = Subflow.Value;
            return Func(temp.Value, b);
        }
    }
}
