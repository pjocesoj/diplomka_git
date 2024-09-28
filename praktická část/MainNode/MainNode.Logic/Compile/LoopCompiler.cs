using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MainNode.Logic.Enums;
using System.Diagnostics;
using MainNode.Logic.Extentions;
using System.Globalization;
using MainNode.Logic.Evaluation.Funcs;

namespace MainNode.Logic.Compile
{
    /// <summary>
    /// překládá uživatelem zadaný vstup do podoby, kterou lze vykonat
    /// </summary>
    public class LoopCompiler
    {
        private FlowRepository _flowRepo = new FlowRepository();
        private NodeRepository _nodeRepo = new NodeRepository();
        private FuncRepo _funcRepo = new FuncRepo();

        public LoopCompiler(FlowRepository flowRepo, NodeRepository nodeRepo, FuncRepo funcRepo)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
            _funcRepo = funcRepo;

            InitTable();
        }
        #region finite automata
        private Stack<StackValue> _stack = new Stack<StackValue>();
        private TransitionFunc[,] _table;

        private void InitTable()
        {
            char[] chars = { 'Ø', 'A', '0', '.', '(', ')', '+', '-', '*', '/', '<', '>', '=' };
            string[] states = { "Ø", "N", "E", "V", "+", "-", "*", "/", "<", ">", "=", ">=", "<=" };
            int numberOfstates = Enum.GetValues(typeof(LCStateEnum)).Length;

            _table = new TransitionFunc[chars.Length, numberOfstates];

            //nemohu určit jestli je to node, subflow nebo true/false
            _table[getId('a'), (int)LCStateEnum.NULL] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);
            _table[getId('a'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);
            _table[getId('0'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);
            _table[getId('.'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.DOT_EP, resolveUnknown, null);

            //node
            //_table[getId('a'), (int)LCStateEnum.NULL] = new TransitionFunc(LCStateEnum.NODE, AddChar, StackValueTypeEnum.NODE);
            _table[getId('a'), (int)LCStateEnum.NODE] = new TransitionFunc(LCStateEnum.NODE, AddChar, StackValueTypeEnum.NODE);
            _table[getId('0'), (int)LCStateEnum.NODE] = new TransitionFunc(LCStateEnum.NODE, AddChar, StackValueTypeEnum.NODE);

            _table[getId('.'), (int)LCStateEnum.NODE] = new TransitionFunc(LCStateEnum.DOT_EP, validateNode, null);
            _table[getId('.'), (int)LCStateEnum.ENDPOINT] = new TransitionFunc(LCStateEnum.DOT_VAL, validateEndpoint, null);

            //endpoint
            _table[getId('a'), (int)LCStateEnum.DOT_EP] = new TransitionFunc(LCStateEnum.ENDPOINT, AddChar, StackValueTypeEnum.ENDPOINT);
            _table[getId('0'), (int)LCStateEnum.DOT_EP] = new TransitionFunc(LCStateEnum.ENDPOINT, AddChar, StackValueTypeEnum.ENDPOINT);
            _table[getId('a'), (int)LCStateEnum.ENDPOINT] = new TransitionFunc(LCStateEnum.ENDPOINT, AddChar, StackValueTypeEnum.ENDPOINT);
            _table[getId('0'), (int)LCStateEnum.ENDPOINT] = new TransitionFunc(LCStateEnum.ENDPOINT, AddChar, StackValueTypeEnum.ENDPOINT);

            //value ref + const
            _table[getId('a'), (int)LCStateEnum.DOT_VAL] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);
            _table[getId('0'), (int)LCStateEnum.DOT_VAL] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);
            _table[getId('a'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);
            _table[getId('0'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);
            _table[getId('.'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);//float
            _table[getId('0'), (int)LCStateEnum.NULL] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);

            //operation +-*/
            _table[getId('+'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.VALUE, addValue, StackValueTypeEnum.OPERATOR);//float


            //vše přečteno
            _table[0, (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.VALUE, addValue, null);

            printTable();
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
                case '-': return 6;
                case '*': return 6;
                case '/': return 6;

                case '<': return 7;
                case '>': return 8;
                case '=': return 9;

                default: return 0;
            }
        }
        private void printTable()
        {
            for (int i = 0; i < _table.GetLength(0); i++)
            {
                for (int j = 0; j < _table.GetLength(1); j++)
                {
                    if (_table[i, j] != null)
                    {
                        Debug.Write($"O");
                    }
                    else
                    {
                        Debug.Write($"X");
                    }
                }
                Debug.WriteLine("");
            }
        }

        private StackValue PopValue(StackValueTypeEnum expected)
        {
            var cache = _stack.Pop();
            if (cache == null)
            {
                throw new ApplicationException($"Invalid stack state");
            }
            if (cache.Type != expected)
            {
                throw new ApplicationException($"Invalid type {cache.Type} expected {expected}");
            }
            return cache;
        }
        #region transition functions
        private void AddChar(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            if (pushType == null)
            {
                throw new ApplicationException($" state:{state} char: {c} Push type is null");
            }

            var cache = _stack.FirstOrDefault();
            if (cache == null || cache.Type != pushType)
            {
                cache = new StackValue { Type = pushType.Value };
                _stack.Push(cache);
            }
            cache.Value.Append(c);
        }
        private void resolveUnknown(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = _stack.Peek();

            if (c == '.')
            {
                cache.Type = StackValueTypeEnum.NODE;
                validateNode(c, state, pushType);
                return;
            }

            switch (cache.Value.ToString())
            {
                case "true":
                    cache.CachedValue = true;
                    cache.Type = StackValueTypeEnum.VALUE;
                    break;
                case "false":
                    cache.CachedValue = false;
                    cache.Type = StackValueTypeEnum.VALUE;
                    break;
                default:
                    cache.Type = StackValueTypeEnum.NODE;
                    break;
            }
        }
        private void validateNode(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = PopValue(StackValueTypeEnum.NODE);

            var n = _nodeRepo.Nodes.FirstOrDefault(x => x.Name == cache.Value.ToString());
            if (n == null)
            {
                throw new ApplicationException($"Node {cache.Value} not found");
            }
            cache.CachedValue = n;
            _stack.Push(cache);
        }
        private void validateEndpoint(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cacheEp = PopValue(StackValueTypeEnum.ENDPOINT);
            var cacheN = PopValue(StackValueTypeEnum.NODE);

            var n = (Node)cacheN.CachedValue;
            if (n == null)
            {
                throw new ApplicationException($"Node {cacheN.Value} not found in cache");
            }

            var ep = n.EndPoints.FirstOrDefault(x => x.Path.Path.EndsWith(cacheEp.Value.ToString()));//kvůli / v cestě
            if (ep == null)
            {
                throw new ApplicationException($"Endpoint {cacheEp.Value} not found at Node {n.Name}");
            }
            cacheEp.CachedValue = ep;
            _stack.Push(cacheEp);
        }
        private void addValue(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var val = validateValue(c, state, pushType, out Type T);
            createOperation(val, T);

            if (pushType != null)//není poslední znak
            {
                AddChar(c, state, pushType);
            }
        }
        private ValueDo validateValue(char c, LCStateEnum state, StackValueTypeEnum? pushType, out Type T)
        {
            var chacheV = PopValue(StackValueTypeEnum.VALUE);
            ValueDo val = null;

            if (chacheV.Value[0] > '9')
            {
                var cacheEp = PopValue(StackValueTypeEnum.ENDPOINT);
                var ep = (EndPointDo)cacheEp.CachedValue;

                val = ep.Values.GetValueByname(chacheV.Value.ToString(), out T);
            }
            else
            {
                var temp = chacheV.Value.ToString();
                if (temp.Contains('.'))
                {
                    T = typeof(float);
                    val = new ValueDo<float>(temp, float.Parse(temp, CultureInfo.InvariantCulture));
                }
                else
                {
                    T = typeof(int);
                    val = new ValueDo<int>(temp, int.Parse(temp));
                }
            }

            if (val == null)
            {
                throw new ApplicationException($"Value {chacheV.Value} not found");
            }

            return val;
        }

        void createOperation(ValueDo value, Type typeB)
        {
            var cacheO = PopValue(StackValueTypeEnum.OPERATOR);

            Flow flow = null;

            Type typeA = (cacheO.CachedValue is Type) ? (Type)cacheO.CachedValue : typeB;

            if (typeB == null)
            {
                throw new ApplicationException($"cant get type of value {value.Name}");
            }

            try
            {
                var R = PopValue(StackValueTypeEnum.FLOW);
                flow = (Flow)R.CachedValue;
            }
            catch
            {
                flow = createFlow(typeB, $"flow{_flowRepo.Results.Count}");
            }

            var op = cacheO.Value.ToString();
            if (_funcRepo.FunctionsT.TryGetValue((typeA, typeB, op), out var f))
            {
                var A = typeA.DefaultValue();//místo T nemohu použít proměnnou Type a explicitně rozepisovat by bylo na dlouho
                FuncHelper.AddFuncion(f, A, value, flow);

                _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
            }
            else
            {
                throw new ApplicationException($"Function {typeA.Name} {op} {typeB.Name} not found");
            }
        }

        Flow createFlow(Type typeR, string name)
        {
            var flow = Flow.Create(typeR, name);
            return flow;
        }
        void saveFlow(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = PopValue(StackValueTypeEnum.FLOW);
            _flowRepo.AddFlow((Flow)cache.CachedValue);
        }
        #endregion

        #endregion
        public void Compile(string input)
        {
            _stack.Clear();
            _stack.Push(new StackValue { Type = StackValueTypeEnum.OPERATOR, Value = new StringBuilder("+") });

            LCStateEnum state = 0;
            //aby v chybe šlo použít i není použit foreach
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                var f = _table[getId(c), (int)state];

                if (f != null)
                {
                    f.Func(c, state, f.PushValue);
                    state = f.Next;
                }
                else
                {
                    var ok = input.Remove(i);
                    var rest = input.Substring(i);
                    throw new ApplicationException($"Invalid character {c} at position {i} \n \"{ok}\" \"{rest}\"");
                }
            }
            var eos = _table[0, (int)state];
            if (eos != null)
            {
                eos.Func(' ', state, eos.PushValue);
            }
            else
            {
                throw new ApplicationException($"transition function to end is missing");
            }
            saveFlow(' ', state, null);
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
