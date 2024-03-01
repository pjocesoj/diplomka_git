using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class SubflowOperation<T,U>:Operation<T> where T:struct where U : struct
    {
        public Flow<U> Subflow { get; set; }
        public new Func<U, T, T> Func { get; set; }
        public SubflowOperation(Flow<U> flow, Func<U, T, T> func)
        {
            Subflow = flow;
            Func = func;
        }
        public override T Execute(T b)
        {
            var temp = Subflow.Evaluate();
            if (Subflow.Output != null)
            {
                Subflow.Output.Value = temp;
            }
            return Func(temp, b);
        }
    }
}
