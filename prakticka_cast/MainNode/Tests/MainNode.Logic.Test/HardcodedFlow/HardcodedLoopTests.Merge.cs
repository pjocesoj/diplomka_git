﻿using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic.Test.HardcodedFlow
{
    public partial class HardcodedLoopTests
    {
        [TestMethod]
        public void Merge()
        {
            var C = mergeAB(A, B);

            var res = ((ValueDo<bool>)C.Value).Value;
            Assert.AreEqual(false, res);

            _flowRepo.Run();
            StartValue.Value = 4;
            _flowRepo.Run();
            res = ((ValueDo<bool>)C.Value).Value;
            Assert.AreEqual(true, res);

            var D = addD(C);
            res = ((ValueDo<bool>)D.Value).Value;

            Assert.AreEqual(true, res);
        }
        public FlowResult mergeAB(FlowResult A, FlowResult B)
        {
            Flow<bool> flowC = new Flow<bool>("C", new List<Operation<bool>>()
                {
                    new MergeFlowOperation<int, float, bool>((FlowResult<int>)A, (FlowResult<float>)B, (a, b) => { return a > b; })
            });
            var resC = new FlowResult<bool>(flowC);
            return _flowRepo.AddFlow(flowC);
        }
        public FlowResult addD(FlowResult C)
        {
            Flow<bool> flowD = new Flow<bool>("D", new List<Operation<bool>>()
                {
                    new SubFlowOperation<bool,bool>((FlowResult<bool>)C,FuncBoolBool.Or),
                    new Operation<bool>(true, FuncBoolBool.And),
                });
            var resD = new FlowResult<bool>(flowD);

            return _flowRepo.AddFlow(flowD);
        }
    }
}
