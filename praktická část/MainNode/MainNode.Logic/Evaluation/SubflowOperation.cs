using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class SubflowOperation<T>:Operation<T> where T:struct
    {
        public Flow<T> Subflow { get; set; }
        public Func<T, T, T> Func { get; set; }
        public SubflowOperation(Flow<T> flow, Func<T, T, T> func)
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
