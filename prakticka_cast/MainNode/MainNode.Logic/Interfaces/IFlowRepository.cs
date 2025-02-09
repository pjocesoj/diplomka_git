using MainNode.Logic.Enums;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic.Interfaces
{
    public interface IFlowRepository
    {
        Dictionary<EnpointLoadTypeEnum, List<EndpointVariables>> Inputs { get; }
        Dictionary<EnpointLoadTypeEnum, List<EndpointVariables>> Outputs { get; }
        List<FlowResult> Results { get; }

        FlowResult AddFlow(Flow flow);
        FlowResult AddFlow(Flow flow, EndpointVariables input, EndpointVariables output);
        void AddInput(EndpointVariables input);
        void AddOutput(EndpointVariables output);
        void Clear();
        FlowResult GetFlowByName(string name);
        void Run();
    }
}