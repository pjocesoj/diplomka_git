using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class SubFlowOperation<T, U> : Operation<T> where T : struct where U : struct
    {
        public FlowResult<U> SubFlow { get; protected set; }
        public new Func<T, U, T> Func { get; protected set; }
        public SubFlowOperation(FlowResult<U> flow, Func<T, U, T> func)
        {
            SubFlow = flow;
            Func = func;
        }
        public override T Execute(T a)
        {
            var temp = SubFlow.Value;
            return Func(a,temp.Value);
        }
    }
}
