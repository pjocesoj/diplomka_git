using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<Flow> Flows { get; private set; } = new List<Flow>();

        public FlowRepository() { }

        public void AddFlow(Flow flow)
        {
            Flows.Add(flow);
        }

        public void Run()
        {
            foreach (Flow flow in Flows)
            {
                flow.Run();
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

            A.Finished = false;
            B.Finished = false;
            C.Finished = false;
            a.Value = 4;

            res = C.Value;
        }
        public FlowResult<int> addA(ValueDo<int> a)
        {
            Flow<int> flowA = new Flow<int>("A", new List<Operation<int>>()
            {
                new Operation<int>(a,FuncIntInt.Plus),
                new Operation<int>(2, FuncIntInt.Multiply),
            });
            var resA = new FlowResult<int>(flowA);

            AddFlow(flowA);
            return resA;
        }
        public FlowResult<float> addB()
        {
            var c = new ValueDo<float>("c", 3.14f);
            Flow<float> flowB = new Flow<float>("B", new List<Operation<float>>()
            {
                new Operation<float>(c,FuncFloatFloat.Plus),
                new Operation<float>(2, FuncFloatFloat.Multiply),
            });
            var resB = new FlowResult<float>(flowB);

            AddFlow(flowB);
            return resB;
        }
        public FlowResult<float> addC(FlowResult<int> A, FlowResult<float> B)
        {
            Flow<float> flowC = new Flow<float>("C", new List<Operation<float>>()
            {
                new SubflowOperation<float,int>(A,FuncFloatInt.Plus),
                new SubflowOperation<float,float>(B, FuncFloatFloat.Plus),
            });
            var resC = new FlowResult<float>(flowC);

            AddFlow(flowC);
            return resC;
        }
        #endregion
    }
}
