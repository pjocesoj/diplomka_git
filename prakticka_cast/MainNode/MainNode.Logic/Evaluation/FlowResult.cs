using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    /// <summary>
    /// obsahuje výsledek flow a informaci zda je aktuální<br/>
    /// pokud ne tak se flow spustí a vrátí výsledek
    /// </summary>
    public class FlowResult<T> : FlowResult where T : struct
    {
        public bool Finished { get; set; } = false;
        public override string Name => Flow.Name;
        public Flow<T> Flow { get; set; }

        protected ValueDo<T> _valueDo;
        public FlowResult(Flow<T> flow)
        {
            Flow = flow;
            if (Flow.Output == null)
            {
                _valueDo = new ValueDo<T>($"{flow.Name}_out", default);
                Flow.Output = _valueDo;
            }
            else
            {
                _valueDo = Flow.Output;
            }
        }
        public override Type GetT() => typeof(T);
        public override ValueDo<T> Value
        {
            get
            {
                if (Finished) { return _valueDo; }

                Flow.Run();
                Finished = true;
                _lastRun = DateTime.Now;
                IsActual = true;
                return _valueDo;
            }
        }
        public override void NewIteration()
        {
            var diff=DateTime.Now-_lastRun;
            if (diff < RunFrequency) 
            {
                IsActual = false;
                return; 
            }
            Finished = false;
        }
        public override void BindOutput(ValueDo bind)
        {
            _valueDo = (ValueDo<T>)bind;
            Flow.Output = _valueDo;
        }
        public override bool CompareReference(ValueDo cmp)
        {
            var a = _valueDo == Value;
            var b = _valueDo == cmp;
            var c = Value == cmp;
            return c;
        }
    }

    public abstract class FlowResult
    {
        public abstract string Name { get; }
        public abstract ValueDo Value { get; }
        public abstract Type GetT();

        public abstract void NewIteration();
        public abstract void BindOutput(ValueDo bind);
        public abstract bool CompareReference(ValueDo value);
        
        protected DateTime _lastRun = DateTime.MinValue;
        public TimeSpan RunFrequency { get; set; } = TimeSpan.FromMicroseconds(0);
        public bool IsActual{ get; protected set; }
    }
}
