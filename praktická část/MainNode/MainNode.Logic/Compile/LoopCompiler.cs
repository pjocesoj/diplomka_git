using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MainNode.Logic.Compile
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
            InitTable();
        }
        #region finite automata
        private Stack<StackValue> _stack = new Stack<StackValue>();
        private TransitionFunc[,] _table;

        private void InitTable()
        {
            char[] chars = { 'Ø', 'A', '0', '.', '(', ')', '+', '-', '*', '/', '<', '>', '=' };
            string[] states = { "Ø", "N", "E", "V", "+", "-", "*", "/", "<", ">", "=", ">=", "<=" };

            _table = new TransitionFunc[chars.Length, states.Length];

            _table[getId('a'), 0] = new TransitionFunc(new Point(getId('a'), 1), AddChar);
            _table[getId('a'), 1] = new TransitionFunc(new Point(getId('a'), 1), AddChar);
            _table[getId('0'), 1] = new TransitionFunc(new Point(getId('a'), 1), AddChar);

        }
        private int getId(char c)
        {
            if (c >= 'a' && c <= 'z') { return 1; }
            if (c >= 'A' && c <= 'Z') { return 1; }
            if (c >= '0' && c <= '9') { return 2; }

            switch (c)
            {
                case '.': return 3;
                case '(': return 4;
                case ')': return 5;
                case '+': return 6;
                case '-': return 7;
                case '*': return 8;
                case '/': return 9;
                case '<': return 10;
                case '>': return 11;
                case '=': return 12;
                default: return 0;
            }
        }
        private void AddChar(char c, StackValueTypeEnum state)
        {
            var cache = _stack.FirstOrDefault();
            if (cache == null)
            {
                cache = new StackValue { Type = state };
                _stack.Push(cache);
            }
            cache.Value.Append(c);
        }
        #endregion
        public void Compile(string input)
        {
            int state = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                var f = _table[getId(c), state];

                if (f != null) 
                {
                    f.Func(c, StackValueTypeEnum.NODE);
                    state = f.Next.Y;
                }
                else
                {
                    var ok = input.Remove(i);
                    var rest = input.Substring(i);
                    throw new ApplicationException($"Invalid character {c} at position {i} \n \"{ok}\" \"{rest}\"");
                }
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
            var B = addB(n.EndPoints[2].Values.Ints[0], n, n.EndPoints[2]);
            B.RunFrequency = TimeSpan.FromMilliseconds(1100);
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
        public FlowResult addB(ValueDo<int> a, Node n, EndPointDo ep)
        {
            Flow<int> flowB = new Flow<int>("B", new List<Operation<int>>()
                {
                    new Operation<int>(a,FuncIntInt.Plus),
                    new Operation<int>(2, FuncIntInt.Multiply),
                });
            var resB = new FlowResult<int>(flowB);

            var i = new EndpointVariables
            {
                Variables = new List<string> { "a" },
                Node = n,
                EndPoint = ep
            };
            return _flowRepo.AddFlow(flowB, i, null);
        }

        public Node EmulatorNode()
        {
            var get = endpointGet();
            var set = endpointSet();
            var slow = endpointSlow();

            //var eps = getEndPoints();
            var eps = new EndPointDo[] { get, set, slow };
            var _comm = new Communication.HttpNodeCommunication();

            Node n = new Node(_comm);
            n.Address = "localhost:8080";
            n.EndPoints = eps;

            return n;
        }

        private EndPointDo endpointGet()
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

            return get;
        }
        private EndPointDo endpointSet()
        {
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

            return set;
        }
        private EndPointDo endpointSlow()
        {
            var slow = new EndPointDo
            {
                Path = new Communication.Dto.HttpEndPointPath() { Path = "/slow" },
                Type = Communication.Enums.EndpointType.GET,
                Values = new ValuesDo(),
                Arguments = new ValuesDo(),
                Delay = 1100
            };
            slow.Values.Ints.Add(new ValueDo<int>("a", 0));

            return slow;
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
