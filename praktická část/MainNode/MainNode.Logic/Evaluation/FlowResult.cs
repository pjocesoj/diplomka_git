﻿using MainNode.Logic.Do;

namespace MainNode.Logic.Evaluation
{
    /// <summary>
    /// obsahuje výsledek flow a informaci zda je aktuální<br/>
    /// pokud ne tak se flow spustí a vrátí výsledek
    /// </summary>
    public class FlowResult<T>:FlowResult where T : struct
    {
        public bool Finished { get; set; } = false;
        public Flow<T> Flow { get; set; }

        private ValueDo<T> _valueDo;
        public FlowResult(Flow<T> flow)
        {           
            Flow = flow;
            _valueDo = new ValueDo<T>($"{flow.Name}_out",default);
            Flow.Output = _valueDo;
        }

        public override ValueDo<T> Value 
        {
            get
            {
                if(Finished) { return _valueDo; }
                
                Flow.Run();
                //_valueDo.Value = Flow.Output.Value;
                Finished = true;
                return _valueDo;
            }
        }
        public override void NewIteration()
        {
            Finished = false;
        }
    }

    public abstract class FlowResult
    {
        public abstract valueDo Value { get; }

        public abstract void NewIteration();
    }
}
