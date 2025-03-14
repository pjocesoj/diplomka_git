using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainNode.Logic.Enums;
using System.Diagnostics;
using MainNode.Logic.Extensions;
using MainNode.Logic.Repos;
using MainNode.Logic.Interfaces;

namespace MainNode.Logic.Compile
{
    /// <summary>
    /// překládá uživatelem zadaný vstup do podoby, kterou lze vykonat
    /// </summary>
    public partial class LoopCompiler : ILoopCompiler
    {
        private IFlowRepository _flowRepo = new FlowRepository();
        private INodeRepository _nodeRepo = new NodeRepository();
        private FuncRepo _funcRepo = new FuncRepo();

        public LoopCompiler(IFlowRepository flowRepo, INodeRepository nodeRepo, FuncRepo funcRepo)
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
        public void CompileMultyLine(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine, ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                line = line.TrimStart();
                try
                {
                    Compile(line);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException($"Error in line {i}:\n {ex.Message}", ex);
                }
            }
        }


    }
}
