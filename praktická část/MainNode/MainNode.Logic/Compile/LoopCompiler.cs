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
        private int _subflowCounter = 0;
        private void InitTable()
        {
            //char[] chars = { 'Ø', 'A', '0', '.', '(', ')', '+', '-', '*', '/', '<', '>', '=' };
            string[] chars = { "Ø", "A-Z<br/>a-z", "0-9", ".", "(", ")", "+-*/", "&\\|", "!", "<", ">", "=", " " };

            string[] states = { "Ø", "N", "E", "V", "+", "-", "*", "/", "<", ">", "=", ">=", "<=" };
            int numberOfstates = Enum.GetValues(typeof(LCStateEnum)).Length;

            _table = new TransitionFunc[chars.Length, numberOfstates];

            //nemohu určit jestli je to node, subflow nebo true/false
            _table[getId('a'), (int)LCStateEnum.NULL] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);
            _table[getId('a'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);
            _table[getId('a'), (int)LCStateEnum.OPERATOR] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);
            _table[getId('0'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.UNKNOWN, AddChar, StackValueTypeEnum.UNKNOWN);

            //node
            _table[getId('a'), (int)LCStateEnum.NODE] = new TransitionFunc(LCStateEnum.NODE, AddChar, StackValueTypeEnum.NODE);
            _table[getId('0'), (int)LCStateEnum.NODE] = new TransitionFunc(LCStateEnum.NODE, AddChar, StackValueTypeEnum.NODE);

            //dot
            _table[getId('.'), (int)LCStateEnum.NODE] = new TransitionFunc(LCStateEnum.DOT_EP, validateNode, null);
            _table[getId('.'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.DOT_EP, resolveUnknown, null);
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
            _table[getId('0'), (int)LCStateEnum.OPERATOR] = new TransitionFunc(LCStateEnum.VALUE, AddChar, StackValueTypeEnum.VALUE);

            //operation +-*/
            _table[getId('+'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.NULL, addValue, StackValueTypeEnum.OPERATOR);
            _table[getId('+'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.NULL, resolveUnknown, StackValueTypeEnum.OPERATOR);

            //operation &|
            _table[getId('&'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.NULL, addValue, StackValueTypeEnum.OPERATOR);
            _table[getId('&'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.NULL, resolveUnknown, StackValueTypeEnum.OPERATOR);

            //operation !
            _table[getId('!'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.OPERATOR, AddNewPush, StackValueTypeEnum.OPERATOR);
            _table[getId('!'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.OPERATOR, AddNewPush, StackValueTypeEnum.OPERATOR);
            _table[getId('!'), (int)LCStateEnum.NULL] = new TransitionFunc(LCStateEnum.OPERATOR, AddNewPush, StackValueTypeEnum.OPERATOR);
            _table[getId('!'), (int)LCStateEnum.EQUALS_SIGN] = new TransitionFunc(LCStateEnum.OPERATOR, assign, StackValueTypeEnum.OPERATOR);

            //operation < >
            _table[getId('<'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.OPERATOR, addValue, StackValueTypeEnum.COMPARE);
            _table[getId('<'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.OPERATOR, resolveUnknown, StackValueTypeEnum.COMPARE);
            _table[getId('>'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.OPERATOR, addValue, StackValueTypeEnum.COMPARE);
            _table[getId('>'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.OPERATOR, resolveUnknown, StackValueTypeEnum.COMPARE);

            //=
            _table[getId('='), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.EQUALS_SIGN, AddNewPush, StackValueTypeEnum.VALUE);
            _table[getId('='), (int)LCStateEnum.FLOW] = new TransitionFunc(LCStateEnum.EQUALS_SIGN, AddNewPush, StackValueTypeEnum.FLOW);
            _table[getId('='), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.EQUALS_SIGN, AddNewPush, StackValueTypeEnum.UNKNOWN);
            _table[getId('='), (int)LCStateEnum.EQUALS_SIGN] = new TransitionFunc(LCStateEnum.NULL, changeToEqual, StackValueTypeEnum.COMPARE);
            _table[getId('='), (int)LCStateEnum.OPERATOR] = new TransitionFunc(LCStateEnum.NULL, changeToEqual, StackValueTypeEnum.COMPARE);
            _table[getId('0'), (int)LCStateEnum.EQUALS_SIGN] = new TransitionFunc(LCStateEnum.VALUE, assign, StackValueTypeEnum.VALUE);
            _table[getId('a'), (int)LCStateEnum.EQUALS_SIGN] = new TransitionFunc(LCStateEnum.UNKNOWN, assign, StackValueTypeEnum.UNKNOWN);
            _table[getId('('), (int)LCStateEnum.EQUALS_SIGN] = new TransitionFunc(LCStateEnum.NULL, assign, StackValueTypeEnum.FLOW);

            //flow
            //_table[getId('='), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.NULL, addFlowFromValue, StackValueTypeEnum.FLOW);
            //_table[getId('='), (int)LCStateEnum.FLOW] = new TransitionFunc(LCStateEnum.NULL, addFlowFromName, StackValueTypeEnum.FLOW);
            //_table[getId('='), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.NULL, resolveUnknown, StackValueTypeEnum.FLOW);
            _table[getId(' '), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.FLOW, resolveUnknown, StackValueTypeEnum.FLOW);
            _table[getId('a'), (int)LCStateEnum.FLOW] = new TransitionFunc(LCStateEnum.FLOW, AddChar, StackValueTypeEnum.FLOW);
            _table[getId('0'), (int)LCStateEnum.FLOW] = new TransitionFunc(LCStateEnum.FLOW, AddChar, StackValueTypeEnum.FLOW);

            //subflow
            _table[getId('('), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.NULL, subflowStart, StackValueTypeEnum.FLOW);
            _table[getId('('), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.NULL, subflowStart, StackValueTypeEnum.FLOW);
            _table[getId('('), (int)LCStateEnum.NULL] = new TransitionFunc(LCStateEnum.NULL, subflowStart, StackValueTypeEnum.FLOW);
            _table[getId(')'), (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.UNKNOWN, subflowEnd, StackValueTypeEnum.FLOW);
            _table[getId(')'), (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.UNKNOWN, subflowEnd, StackValueTypeEnum.FLOW);


            //vše přečteno
            _table[0, (int)LCStateEnum.VALUE] = new TransitionFunc(LCStateEnum.VALUE, addValue, null);
            _table[0, (int)LCStateEnum.UNKNOWN] = new TransitionFunc(LCStateEnum.VALUE, resolveUnknown, null);

            printTableMD(chars);
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

                case '&': return 7;
                case '|': return 7;

                case '!': return 8;

                case '<': return 9;
                case '>': return 10;
                case '=': return 11;
                case ' ': return 12;

                default:
                    throw new ApplicationException($"Invalid character {c}");
            }
        }
        private void printTableMD(string[] chars)
        {
            var states = Enum.GetValues(typeof(LCStateEnum));
            var values = string.Join("|", states.Cast<LCStateEnum>().Select(x => x.ToString()));
            var sb = new StringBuilder($"|_____|{values}|\n");
            for (int i = 0; i <= states.Length; i++)
            {
                sb.Append("|-");
            }
            sb.AppendLine("|");


            for (int i = 0; i < _table.GetLength(0); i++)
            {
                sb.Append($"|{chars[i]}");
                for (int j = 0; j < _table.GetLength(1); j++)
                {
                    var next = _table[i, j];
                    if (next != null)
                    {
                        sb.Append($"|{next.Next}");
                    }
                    else
                    {
                        sb.Append($"|-");
                    }
                }
                sb.AppendLine("|");
            }
            Debug.WriteLine(sb.ToString());
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
        private void PushVariable(EndPointDo ep, Node n)
        {
            _stack.Push(new StackValue
            {
                Type = StackValueTypeEnum.EP_VARIABLE,
                CachedValue = new EndpointVariables { Node = n, EndPoint = ep }
            });
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
        private void AddNewPush(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            if (pushType == null)
            {
                throw new ApplicationException($" state:{state} char: {c} Push type is null");
            }

            var cache = new StackValue { Type = pushType.Value };
            cache.Value.Append(c);
            _stack.Push(cache);
        }
        private void assign(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var eq = _stack.Pop();
            switch (eq.Type)
            {
                case StackValueTypeEnum.VALUE:
                    addFlowFromValue(c, state, pushType);
                    break;
                case StackValueTypeEnum.FLOW:
                    addFlowFromName(c, state, pushType);
                    break;
                case StackValueTypeEnum.UNKNOWN:
                    resolveUnknown(c, state, pushType);
                    break;
                default:
                    throw new ApplicationException($"Unexpected type {eq.Type}");
            }
            if (c == '(')
            {
                subflowStart(c, state, pushType);
                return;
            }
            AddNewPush(c, state, pushType);
        }
        private void changeToEqual(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = _stack.Peek();
            cache.Type = StackValueTypeEnum.COMPARE;
            cache.Value.Append(c);
        }
        private void resolveUnknown(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = _stack.Peek();
            var str = cache.Value.ToString();

            if (cache.Type != StackValueTypeEnum.UNKNOWN)
            {
                //end of stream
                if (cache.Type == StackValueTypeEnum.FLOW)
                {
                    return;
                }
                else
                {
                    throw new ApplicationException($"Unexpected character {c} after {cache.Type}");
                }
            }

            if (c == '.')
            {
                cache.Type = StackValueTypeEnum.NODE;
                validateNode(c, state, pushType);
                return;
            }

            switch (str)
            {
                case " ":
                    //pro jistotu
                    break;
                case "true":
                    cache.CachedValue = new ValueDo<bool>("true", true);
                    cache.Type = StackValueTypeEnum.VALUE;
                    addValue(c, state, pushType);
                    break;
                case "false":
                    cache.CachedValue = new ValueDo<bool>("false", false);
                    cache.Type = StackValueTypeEnum.VALUE;
                    addValue(c, state, pushType);
                    break;
                case "int":
                    cache.CachedValue = typeof(int);
                    cache.Type = StackValueTypeEnum.TYPE;
                    break;
                case "float":
                    cache.CachedValue = typeof(float);
                    cache.Type = StackValueTypeEnum.TYPE;
                    break;
                case "bool":
                    cache.CachedValue = typeof(bool);
                    cache.Type = StackValueTypeEnum.TYPE;
                    break;
                default:
                    if (str.Any(char.IsLetter))
                    {
                        cache.Type = StackValueTypeEnum.FLOW;
                        addValue(c, state, pushType);
                    }
                    else
                    {
                        cache.Type = StackValueTypeEnum.VALUE;
                        addValue(c, state, pushType);
                    }

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
            PushVariable(ep, n);
            _stack.Push(cacheEp);
        }
        private void addValue(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            if (_subflowCounter > 0 && state != LCStateEnum.SUBFLOW)
            {
                AddChar(c, state, pushType);
                return;
            }

            if (_stack.Peek().Type == StackValueTypeEnum.VALUE)
            {
                var val = validateValue(c, state, pushType);
                createOperation(val);
            }
            else if (_stack.Peek().Type == StackValueTypeEnum.FLOW)
            {
                var cacheF = PopValue(StackValueTypeEnum.FLOW);
                var flow = _flowRepo.GetFlowByName(cacheF.Value.ToString());
                createOperation(flow);
            }

            if (pushType != null)//není poslední znak
            {
                AddChar(c, state, pushType);
            }
        }
        private ValueDo validateValue(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cacheV = PopValue(StackValueTypeEnum.VALUE);
            var ret = validateValue(cacheV);
            addInputOutput(c);
            return ret;
        }
        private ValueDo validateValue(StackValue cacheV)
        {
            ValueDo val = null;

            if (cacheV.CachedValue != null)
            {
                val = (ValueDo)cacheV.CachedValue;
            }
            else if (cacheV.Value[0] > '9')
            {
                var cacheEp = PopValue(StackValueTypeEnum.ENDPOINT);
                var ep = (EndPointDo)cacheEp.CachedValue;

                val = ep.Values.GetValueByname(cacheV.Value.ToString());
            }
            else
            {
                var temp = cacheV.Value.ToString();
                if (temp.Contains('.'))
                {
                    val = new ValueDo<float>(temp, float.Parse(temp, CultureInfo.InvariantCulture));
                }
                else
                {
                    val = new ValueDo<int>(temp, int.Parse(temp));
                }
            }

            if (val == null)
            {
                throw new ApplicationException($"Value {cacheV.Value} not found");
            }

            return val;
        }

        #region operation
        void operationdata(Type typeB, out Type typeA, out Flow flow, out Delegate f)
        {
            var cacheO = PopValue(StackValueTypeEnum.OPERATOR);
            operationdata(typeB, cacheO, out typeA, out flow, out f);
        }
        void operationdata(Type typeB, StackValue cacheO, out Type typeA, out Flow flow, out Delegate f)
        {
            if (typeB == null)
            {
                throw new ApplicationException($"cant get typeB");
            }
            //typeA = (cacheO.CachedValue is Type) ? (Type)cacheO.CachedValue : typeB;

            try
            {
                var R = PopValue(StackValueTypeEnum.FLOW);
                flow = (Flow)R.CachedValue;
            }
            catch
            {
                flow = Flow.Create(typeB, $"<flow{_flowRepo.Results.Count}>");
            }

            typeA = flow.getT();
            f = _funcRepo.GetFunction(typeA, typeB, cacheO.Value.ToString());
        }

        void createOperation(ValueDo value)
        {
            var typeB = value.getT();
            var cacheO = PopValue(StackValueTypeEnum.OPERATOR);
            if (_stack.Count > 0 && _stack.Peek().Type == StackValueTypeEnum.OPERATOR)
            {
                var flow = createOperation(value, cacheO.Value.ToString());
                createOperation(flow);
            }
            else
            {
                operationdata(typeB, cacheO, out Type typeA, out Flow flow, out Delegate f);

                //místo T nemohu použít proměnnou Type a explicitně rozepisovat všechny možné kombinace by bylo na dlouho
                var A = typeA.DefaultValue();
                FuncHelper.AddFuncion(f, A, value, flow);

                _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
            }
        }
        void createOperation(FlowResult value)
        {
            var typeB = value.getT();
            var cacheO = PopValue(StackValueTypeEnum.OPERATOR);
            if (_stack.Peek().Type == StackValueTypeEnum.OPERATOR)
            {
                var flow = createOperation(value, cacheO.Value.ToString());
                createOperation(flow);
            }
            else
            {
                operationdata(typeB, cacheO, out Type typeA, out Flow flow, out Delegate f);

                //místo T nemohu použít proměnnou Type a explicitně rozepisovat všechny možné kombinace by bylo na dlouho
                var A = typeA.DefaultValue();
                FuncHelper.AddFuncion(f, A, value, flow);

                _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
            }
        }

        #region subflow
        void createOperation(FlowResult A, FlowResult B, string op)
        {
            var typeA = A.getT();
            var typeB = B.getT();

            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }
        void createOperation(ValueDo A, FlowResult B, string op)
        {
            var typeA = A.getT();
            var typeB = B.getT();

            var f0 = _funcRepo.GetFunction(typeA, typeA, "0");
            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            FuncHelper.AddFuncion(f0, A, A, flow);
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }
        void createOperation(FlowResult A, ValueDo B, string op)
        {
            var typeA = A.getT();
            var typeB = B.getT();

            var help = typeA.DefaultValue();
            var f0 = _funcRepo.GetFunction(typeA, typeA, "0");
            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            FuncHelper.AddFuncion(f0, help, A, flow);
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }
        void createOperation(ValueDo A, ValueDo B, string op)
        {
            var typeA = A.getT();
            var typeB = B.getT();

            var f0 = _funcRepo.GetFunction(typeA, typeA, "0");
            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            FuncHelper.AddFuncion(f0, A, A, flow);
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }
        #endregion
        FlowResult createOperation(ValueDo value, string op)
        {
            var typeB = value.getT();

            var f = _funcRepo.GetFunction(typeB, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");

            //místo T nemohu použít proměnnou Type a explicitně rozepisovat všechny možné kombinace by bylo na dlouho
            var A = typeB.DefaultValue();
            FuncHelper.AddFuncion(f, A, value, flow);

            var ret = _flowRepo.AddFlow(flow);
            return ret;
        }
        FlowResult createOperation(FlowResult value, string op)
        {
            var typeB = value.getT();

            var f = _funcRepo.GetFunction(typeB, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");

            //místo T nemohu použít proměnnou Type a explicitně rozepisovat všechny možné kombinace by bylo na dlouho
            var A = typeB.DefaultValue();
            FuncHelper.AddFuncion(f, A, value, flow);

            // _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
            return flow.GetResult();
        }

        FlowResult GetFlow(StackValue cache)
        {
            if(cache.Type==StackValueTypeEnum.UNKNOWN)
            {
                resolveUnknown(cache);
            }

            if (cache.Type == StackValueTypeEnum.FLOW)
            {
                var flow = (FlowResult)cache.CachedValue;
                if (flow != null)
                {
                    return flow;
                }
                return _flowRepo.GetFlowByName(cache.Value.ToString());
            }
            else if (cache.Type == StackValueTypeEnum.VALUE)
            {
                var val = validateValue(cache);
                return createOperation(val, "0");
            }
            else
            {
                throw new ApplicationException($"Unexpected type {cache.Type}");
            }
        }
        #endregion

        void addInputOutput(char c)
        {
            if (_stack.Peek().Type != StackValueTypeEnum.EP_VARIABLE) { return; }

            var cache = PopValue(StackValueTypeEnum.EP_VARIABLE);
            if (c == '=')
            {
                _flowRepo.AddOutput((EndpointVariables)cache.CachedValue);
            }
            else
            {
                _flowRepo.AddInput((EndpointVariables)cache.CachedValue);
            }
        }

        void addFlowFromValue(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var val = validateValue(c, state, pushType);
            var name = $"<{val.Name}>";
            createFlow(val.getT(), name);
            var res = _flowRepo.GetFlowByName(name);
            res.BindOutput(val);
        }
        void addFlowFromName(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = PopValue(StackValueTypeEnum.FLOW);

            try
            {
                var cacheT = PopValue(StackValueTypeEnum.TYPE);
                var T = (Type)cacheT.CachedValue;
                createFlow(T, cache.Value.ToString());
            }
            catch
            {
                createFlow(typeof(int), cache.Value.ToString());
            }
        }
        Flow createFlow(Type typeR, string name)
        {
            var flow = Flow.Create(typeR, name);
            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });

            //default
            _stack.Push(new StackValue { Type = StackValueTypeEnum.OPERATOR, Value = new StringBuilder("0"), CachedValue = typeR });

            _flowRepo.AddFlow(flow);
            return flow;
        }
        void saveFlow(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = PopValue(StackValueTypeEnum.FLOW);
            _flowRepo.AddFlow((Flow)cache.CachedValue);
        }

        void subflowStart(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            _subflowCounter++;
            _stack.Push(new StackValue { Type = StackValueTypeEnum.SUBFLOW_START });
        }
        void subflowEnd(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            _subflowCounter--;

            if (_stack.Count < 4)
            {
                throw new ApplicationException($"Invalid stack state");
            }

            var cacheB = _stack.Pop();
            resolveUnknown(cacheB);

            //var op = PopValue(StackValueTypeEnum.OPERATOR);
            var op = _stack.Pop();
            var cacheA = _stack.Pop();
            var start = PopValue(StackValueTypeEnum.SUBFLOW_START);

            if (op.Type == StackValueTypeEnum.COMPARE)
            {
                var A = GetFlow(cacheA);
                var B = GetFlow(cacheB);
                createOperation(A, B, op.Value.ToString());
            }
            else if (cacheB.Type == StackValueTypeEnum.FLOW)
            {
                //var B = _flowRepo.GetFlowByName(cacheB.Value.ToString());
                var B = (FlowResult)cacheB.CachedValue;
                addSubflow(cacheA, op.Value.ToString(), B);
            }
            else if (cacheB.Type == StackValueTypeEnum.VALUE)
            {
                //var B = validateValue(cacheB);
                var B = (ValueDo)cacheB.CachedValue;
                addSubflow(cacheA, op.Value.ToString(), B);
            }
            else
            {
                throw new ApplicationException($"unexpected value of type {cacheB.Type}");
            }

            addSubflow();
        }
        private void resolveUnknown(StackValue cacheA, string op, StackValue cacheB)
        {
            var str = cacheB.Value.ToString();
            switch (str)
            {
                case "true":
                    cacheB.CachedValue = new ValueDo<bool>("true", true);
                    cacheB.Type = StackValueTypeEnum.VALUE;
                    //addValue(c, state, pushType);
                    break;
                case "false":
                    cacheB.CachedValue = new ValueDo<bool>("false", false);
                    cacheB.Type = StackValueTypeEnum.VALUE;
                    //addValue(c, state, pushType);
                    break;
                default:
                    if (str.Any(char.IsLetter))
                    {
                        cacheB.Type = StackValueTypeEnum.FLOW;
                        var flow = _flowRepo.GetFlowByName(str);
                        addSubflow(cacheA, op, flow);
                    }
                    else
                    {
                        cacheB.Type = StackValueTypeEnum.VALUE;
                        var B = validateValue(cacheB);
                        addSubflow(cacheA, op, B);
                    }
                    break;
            }
        }
        private void resolveUnknown(StackValue cache)
        {

            if (cache.Type == StackValueTypeEnum.VALUE)
            {
                var val = validateValue(cache);
                cache.CachedValue = val;
                addInputOutput(')');
                return;
            }


            var str = cache.Value.ToString();
            switch (str)
            {
                case "true":
                    cache.CachedValue = new ValueDo<bool>("true", true);
                    cache.Type = StackValueTypeEnum.VALUE;
                    return;
                case "false":
                    cache.CachedValue = new ValueDo<bool>("false", false);
                    cache.Type = StackValueTypeEnum.VALUE;
                    return;
                default:
                    if (str.Any(char.IsLetter))
                    {
                        cache.Type = StackValueTypeEnum.FLOW;
                        var flow = _flowRepo.GetFlowByName(str);
                        cache.CachedValue = flow;
                        //addSubflow(cacheA, op, flow);
                    }
                    else
                    {
                        cache.Type = StackValueTypeEnum.VALUE;
                        var val = validateValue(cache);
                        cache.CachedValue = val;
                        //addSubflow(cacheA, op, B);
                    }
                    return;
            }
        }

        private void addSubflow(StackValue cacheA, string op, ValueDo B)
        {
            if (cacheA.Type == StackValueTypeEnum.FLOW)
            {
                var A = _flowRepo.GetFlowByName(cacheA.Value.ToString());
                //createOperation(A);
                createOperation(A, B, op);
            }
            else if (cacheA.Type == StackValueTypeEnum.VALUE)
            {
                var A = validateValue(cacheA);
                createOperation(A, B, op);
            }
            else
            {
                throw new ApplicationException($"unexpected value of type {cacheA.Type}");
            }
        }
        private void addSubflow(StackValue cacheA, string op, FlowResult B)
        {
            if (cacheA.Type == StackValueTypeEnum.FLOW)
            {
                var A = _flowRepo.GetFlowByName(cacheA.Value.ToString());
                createOperation(A, B, op);
            }
            else if (cacheA.Type == StackValueTypeEnum.VALUE)
            {
                var A = validateValue(cacheA);
                createOperation(A, B, op);
            }
            else
            {
                throw new ApplicationException($"unexpected value of type {cacheA.Type}");
            }
        }

        private void addSubflow()
        {
            if (_subflowCounter > 0)
            {
                throw new NotImplementedException("at this moment subflow inside subflow is not allowed");
            }

            var cacheS = PopValue(StackValueTypeEnum.FLOW);
            var cacheO = PopValue(StackValueTypeEnum.OPERATOR);
            var cacheR = PopValue(StackValueTypeEnum.FLOW);

            var sub = (cacheS.CachedValue as Flow).GetResult();
            var R = (Flow)cacheR.CachedValue;

            var typeR = R.getT();
            var f = _funcRepo.GetFunction(typeR, sub.getT(), cacheO.Value.ToString());

            //místo T nemohu použít proměnnou Type a explicitně rozepisovat všechny možné kombinace by bylo na dlouho
            var A = typeR.DefaultValue();
            FuncHelper.AddFuncion(f, A, sub, R);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = R });
        }
        #endregion

        #endregion
        public void Compile(string input)
        {
            _stack.Clear();
            _stack.Push(new StackValue { Type = StackValueTypeEnum.OPERATOR, Value = new StringBuilder("0") });

            LCStateEnum state = 0;
            //aby v chybové hlášce šlo použít i není použit foreach
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
            //saveFlow(' ', state, null);
        }

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
