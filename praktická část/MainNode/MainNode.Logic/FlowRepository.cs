using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<Flow> Flows { get; private set; } = new List<Flow>();
        public List<FlowResult> Results { get; private set; } = new List<FlowResult>();

        public FlowRepository() { }

        /*
        public void AddFlow(Flow flow)
        {
            Flows.Add(flow);
        }
        */
        public FlowResult AddFlow(Flow flow)
        {
            Flows.Add(flow);
            var res = flow.GetResult();
            Results.Add(res);
            return res;
        }

        public void Run()
        {
            /*foreach (Flow flow in Flows)
            {
                flow.Run();
            }*/
            foreach (FlowResult r in Results)
            {
                r.NewIteration();
            }
            foreach (FlowResult res in Results)
            {
                var val = res.Value;
            }
        }

        #region test
        public void test()
        {
            var a = new ValueDo<int>("a", 1);

            var A = addA(a);
            var B = addB();
            var C = addC(A, B);

            var res = C.Value;

            Run();
            a.Value = 4;
            Run();
            res = C.Value;

        }
        public FlowResult addA(ValueDo<int> a)
        {
            Flow<int> flowA = new Flow<int>("A", new List<Operation<int>>()
                {
                    new Operation<int>(a,FuncIntInt.Plus),
                    new Operation<int>(2, FuncIntInt.Multiply),
                });
            var resA = new FlowResult<int>(flowA);

            return AddFlow(flowA);
        }
        public FlowResult addB()
        {
            var c = new ValueDo<float>("c", 3.14f);
            Flow<float> flowB = new Flow<float>("B", new List<Operation<float>>()
                {
                    new Operation<float>(c,FuncFloatFloat.Plus),
                    new Operation<float>(2, FuncFloatFloat.Multiply),
                });
            var resB = new FlowResult<float>(flowB);

            return AddFlow(flowB);
        }
        public FlowResult addC(FlowResult A, FlowResult B)
        {
            Flow<float> flowC = new Flow<float>("C", new List<Operation<float>>()
                {
                    new SubflowOperation<float,int>((FlowResult<int>)A,FuncFloatInt.Plus),
                    new SubflowOperation<float,float>((FlowResult<float>)B, FuncFloatFloat.Plus),
                });
            var resC = new FlowResult<float>(flowC);

            return AddFlow(flowC);
        }
        #endregion
    }
}
