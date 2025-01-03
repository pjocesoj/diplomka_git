using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class MergeflowOperation<T, U,V> : Operation<V> where T : struct where U : struct where V : struct
    {
        public FlowResult<T> FlowA { get; set; }
        public FlowResult<U> FlowB { get; set; }
        public new Func<T, U, V> Func { get; set; }
        public MergeflowOperation(FlowResult<T> A,FlowResult<U> B, Func<T, U, V> func)
        {
            FlowA = A;
            FlowB = B;
            Func = func;
        }
        /// <summary>
        /// umožní sloučit 2 flow do jednoho
        /// </summary>
        /// <param name="any">pouze kvůli dědičnosti, ale pro výpočet není použit</param>
        /// <returns></returns>
        public override V Execute(V any)
        {
            var a = FlowA.Value;
            var b = FlowB.Value;
            return Func(a.Value,b.Value);
        }
    }
}
