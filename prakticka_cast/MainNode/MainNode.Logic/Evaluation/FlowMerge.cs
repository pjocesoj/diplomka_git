using MainNode.Communication.Dto;
using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    public class FlowMerge<T,U,V>:FlowResult<V> where T : struct where U : struct where V : struct
    {
        public MergeflowOperation<T,U,V> Operation { get; }

        public FlowMerge(FlowResult<T> A, FlowResult<U> B, Func<T, U, V> func):base(null)
        {
            Operation = new MergeflowOperation<T, U, V>(A, B, func);
            _valueDo = new ValueDo<V>($"{A.Flow.Name}_{B.Flow.Name}_out", default);
        }

        public override ValueDo<V> Value
        {
            get
            {
                if (Finished) { return _valueDo; }

                var res=Operation.Execute(default); // run flow
                _valueDo.Value = res;

                Finished = true;
                _lastRun = DateTime.Now;
                IsActual = true;
                return _valueDo;
            }
        }
    }
}
