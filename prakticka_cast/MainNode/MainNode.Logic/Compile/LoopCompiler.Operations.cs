using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;
using MainNode.Logic.Extentions;

namespace MainNode.Logic.Compile
{
    public partial class LoopCompiler
    {
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
            var typeB = value.GetT();
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

        FlowResult createOperation(ValueDo value, string op)
        {
            var typeB = value.GetT();

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
        /*
         * -------------------------------
         * subflow
         * -------------------------------
         */
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
            var typeA = A.GetT();
            var typeB = B.getT();

            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            var f0 = _funcRepo.GetFunction(typeR, typeA, "0");
            var help = typeR.DefaultValue();
            FuncHelper.AddFuncion(f0, help, A, flow);
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }
        void createOperation(FlowResult A, ValueDo B, string op)
        {
            var typeA = A.getT();
            var typeB = B.GetT();

            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            var help = typeR.DefaultValue();
            var f0 = _funcRepo.GetFunction(typeR, typeA, "0");
            FuncHelper.AddFuncion(f0, help, A, flow);
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }
        void createOperation(ValueDo A, ValueDo B, string op)
        {
            var typeA = A.GetT();
            var typeB = B.GetT();

            var f = _funcRepo.GetFunction(typeA, typeB, op);
            var typeR = f.GetType().GetGenericArguments().Last();
            var flow = Flow.Create(typeR, $"<subflow{_flowRepo.Results.Count}>");
            var help = typeR.DefaultValue();
            var f0 = _funcRepo.GetFunction(typeR, typeA, "0");
            FuncHelper.AddFuncion(f0, help, A, flow);
            FuncHelper.AddFuncion(f, A, B, flow);

            _stack.Push(new StackValue { Type = StackValueTypeEnum.FLOW, CachedValue = flow });
        }

    }
}
