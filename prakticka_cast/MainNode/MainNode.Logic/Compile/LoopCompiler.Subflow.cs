using MainNode.Logic.Do;
using MainNode.Logic.Enums;
using MainNode.Logic.Evaluation;
using MainNode.Logic.Extensions;

namespace MainNode.Logic.Compile
{
    public partial class LoopCompiler
    {
        void subflowStart(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            _subFlowCounter++;
            _stack.Push(new StackValue { Type = StackValueTypeEnum.SUBFLOW_START });
        }
        void subflowEnd(char c, LCStateEnum state, StackValueTypeEnum? pushType)
        {
            _subFlowCounter--;

            if (_stack.Count < 4)
            {
                throw new ApplicationException($"Invalid stack state");
            }

            var cacheB = _stack.Pop();
            resolveUnknown(cacheB);

            //var op = PopValue(StackValueTypeEnum.OPERATOR);
            var op = _stack.Pop();

            var cacheA = _stack.Pop();
            resolveUnknown(cacheA);

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

        FlowResult GetFlow(StackValue cache)
        {
            if (cache.Type == StackValueTypeEnum.UNKNOWN)
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
            if (_subFlowCounter > 0)
            {
                throw new NotImplementedException("at this moment subflow inside subflow is not allowed");
            }

            var cacheS = PopValue(StackValueTypeEnum.FLOW);
            var cacheO = PopValue(StackValueTypeEnum.OPERATOR);
            var cacheR = PopValue(StackValueTypeEnum.FLOW);

            var sub = _flowRepo.AddFlow(cacheS.CachedValue as Flow);
            var R = (Flow)cacheR.CachedValue;

            var typeR = R.getT();
            var f = _funcRepo.GetFunction(typeR, sub.GetT(), cacheO.Value.ToString());

            //místo T nemohu použít proměnnou Type a explicitně rozepisovat všechny možné kombinace by bylo na dlouho
            var A = typeR.DefaultValue();
            FuncHelper.AddFuncion(f, A, sub, R);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = R });
        }
    }
}
