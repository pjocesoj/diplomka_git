using MainNode.Logic.Do;
using MainNode.Logic.Enums;
using MainNode.Logic.Extentions;
using System.Globalization;

namespace MainNode.Logic.Compile
{
    public partial class LoopCompiler
    {
        private void resolveUnknown(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            var cache = _stack.Peek();
            var str = cache.Value.ToString();

            if (cache.Type != StackValueTypeEnum.UNKNOWN)
            {
                //end of stream
                if (c == ' ')
                {
                    return;
                }
                else if (pushType == StackValueTypeEnum.OPERATOR)
                {
                    AddNewPush(c, state, StackValueTypeEnum.OPERATOR);
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
    }
}
