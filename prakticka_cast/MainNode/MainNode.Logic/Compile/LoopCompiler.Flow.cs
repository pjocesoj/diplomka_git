using MainNode.Logic.Enums;
using MainNode.Logic.Evaluation;
using System.Text;

namespace MainNode.Logic.Compile
{
    public partial class LoopCompiler
    {
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

    }
}
