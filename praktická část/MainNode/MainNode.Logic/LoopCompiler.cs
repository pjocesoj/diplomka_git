using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNode.Logic
{
    /// <summary>
    /// překládá uživatelem zadaný vstup do podoby, kterou lze vykonat
    /// </summary>
    public class LoopCompiler
    {
        private FlowRepository _flowRepo = new FlowRepository();
        public LoopCompiler(FlowRepository flowRepo)
        {
            _flowRepo = flowRepo;
        }

        #region test
        public void test()
        {
            var a = new ValueDo<int>("a", 1);

            var A = addA(a);
            var B = addB();
            var C = addC(A, B);

            var res = C.Value;

            _flowRepo.Run();
            a.Value = 4;
            _flowRepo.Run();
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

            return _flowRepo.AddFlow(flowA);
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

            return _flowRepo.AddFlow(flowB);
        }
        public FlowResult addC(FlowResult A, FlowResult B)
        {
            Flow<float> flowC = new Flow<float>("C", new List<Operation<float>>()
                {
                    new SubflowOperation<float,int>((FlowResult<int>)A,FuncFloatInt.Plus),
                    new SubflowOperation<float,float>((FlowResult<float>)B, FuncFloatFloat.Plus),
                });
            var resC = new FlowResult<float>(flowC);

            return _flowRepo.AddFlow(flowC);
        }
        #endregion

        #region test - emulator
        public Node TestNode()
        {
            Node n = EmulatorNode();
            //_nodeRepo.AddNode(n);
            var A = addA(n.EndPoints[1].Values.Ints[0], n, n.EndPoints[1]);
            //A.BindOutput(n.EndPoints[1].Arguments.Ints[0]);

            var wtf = A.CompareReference(n.EndPoints[1].Arguments.Ints[0]);
            return n;
        }
        public FlowResult addA(ValueDo<int> a, Node n, EndPointDo ep)
        {
            Flow<int> flowA = new Flow<int>("A", new List<Operation<int>>()
                {
                    new Operation<int>(a,FuncIntInt.Plus),
                    new Operation<int>(2, FuncIntInt.Multiply),
                });
            var resA = new FlowResult<int>(flowA);
            resA.BindOutput(ep.Arguments.Ints[0]);
            resA.CompareReference(ep.Arguments.Ints[0]);

            var i = new EndpointVariables
            {
                Variables = new List<string> { "a" },
                Node = n,
                EndPoint = ep
            };
            var o = new EndpointVariables
            {
                Variables = new List<string> { "a" },
                Node = n,
                EndPoint = ep
            };
            return _flowRepo.AddFlow(flowA, i, o);
        }
        public Node EmulatorNode()
        {
            var eps = getEndPoints();
            var _comm = new MainNode.Communication.HttpNodeCommunication();

            Node n = new Node(_comm);
            n.Address = "localhost:8080";
            n.EndPoints = eps;

            return n;
        }
        private EndPointDo[] getEndPoints()
        {
            var get = new EndPointDo
            {
                Path = new Communication.Dto.HttpEndPointPath() { Path = "/getValuesG" },
                Type = Communication.Enums.EndpointType.GET,
                Values = new ValuesDo(),
                Arguments = new ValuesDo()
            };
            get.Values.Ints.Add(new ValueDo<int>("a", 0));
            get.Values.Floats.Add(new ValueDo<float>("b", 0));
            get.Values.Bools.Add(new ValueDo<bool>("c", false));

            var set = new EndPointDo
            {
                Path = new Communication.Dto.HttpEndPointPath() { Path = "/setValues", HttpMethod = Communication.Enums.HttpMethodEnum.POST },
                Type = Communication.Enums.EndpointType.SET,
                Values = new ValuesDo(),
                Arguments = new ValuesDo()
            };
            var a = new ValueDo<int>("a", 1);
            var b = new ValueDo<float>("b", 0);
            var c = new ValueDo<bool>("c", false);

            set.Values.Ints.Add(a);
            set.Values.Floats.Add(b);
            set.Values.Bools.Add(c);

            //set.Arguments.Ints.Add(a);
            set.Arguments.Floats.Add(b);
            set.Arguments.Bools.Add(c);

            set.Arguments.Ints.Add(new ValueDo<int>("a", 1));
            //set.Arguments.Floats.Add(new ValueDo<float>("b", 0));
            //set.Arguments.Bools.Add(new ValueDo<bool>("c", false));

            return new EndPointDo[] { get, set };
        }
        #endregion
    }
}
