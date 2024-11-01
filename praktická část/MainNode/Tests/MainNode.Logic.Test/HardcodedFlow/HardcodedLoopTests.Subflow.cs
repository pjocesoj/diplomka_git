using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic.Test.HardcodedFlow
{
    public partial class HardcodedLoopTests
    {
        [TestMethod]
        public void Subflow()
        {
            var C = addC(A, B);

            var res = (C.Value as ValueDo<float>).Value;
            Assert.AreEqual(8.28, res, 0.001);//do výsledku se vkradou desetiná místa a test spadne

            _flowRepo.Run();
            StartValue.Value = 4;
            _flowRepo.Run();
            res = (C.Value as ValueDo<float>).Value;
            Assert.AreEqual(14.28, res, 0.001);//do výsledku se vkradou desetiná místa a test spadne
        }
        public FlowResult addC(FlowResult A, FlowResult B)
        {
            Flow<float> flowC = new Flow<float>("C", new List<Operation<float>>()
            {
                new SubflowOperation<float,int>((FlowResult<int>)A,(float a,int b)=>{return a+b; }),
                new SubflowOperation<float,float>((FlowResult<float>)B, FuncFloatFloat.Plus)
            });
            var resC = new FlowResult<float>(flowC);

            return _flowRepo.AddFlow(flowC);
        }
    }
}
