using MainNode.Logic.Compile;
using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Test.HardcodedFlow
{
    [TestClass]
    public partial class HardcodedLoopTests
    {
        private LoopCompiler _loopCompiler;
        private FlowRepository _flowRepo;

        private ValueDo<int> StartValue = new ValueDo<int>("a", 1);
        private FlowResult A;
        private FlowResult B;

        [TestInitialize]
        public void Initialize()
        {
            _flowRepo = new FlowRepository();
            var nodeRepo = new NodeRepository();
            var funcRepo = new FuncRepo();

            A = addA(StartValue);
            B = addB();
        }

        public FlowResult addA(ValueDo<int> a)
        {
            Flow<int> flowA = new Flow<int>("A", new List<Operation<int>>()
            {
                new Operation<int>(a,FuncIntInt.Plus),
                new Operation<int>(2, FuncIntInt.Multiply),
            });
            var resA = new FlowResult<int>(flowA);

            return _flowRepo.AddFlow(flowA);
        }
        public FlowResult addB()
        {
            var c = new ValueDo<float>("c", 3.14f);
            Flow<float> flowB = new Flow<float>("B", new List<Operation<float>>()
            {
                new Operation<float>(c,FuncFloatFloat.Plus),
                new Operation<float>(2, FuncFloatFloat.Multiply)
            });
            var resB = new FlowResult<float>(flowB);

            return _flowRepo.AddFlow(flowB);
        }
    }
}
