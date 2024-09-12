using MainNode.Logic.Do;
using MainNode.Logic.Enums;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<FlowResult> Results { get; private set; } = new List<FlowResult>();
        //public Dictionary<Node, List<EndPointDo>> Inputs { get; private set; } = new Dictionary<Node, List<EndPointDo>>();
        //public Dictionary<Node, List<EndPointDo>> Outputs { get; private set; } = new Dictionary<Node, List<EndPointDo>>();

        public Dictionary<EnpointLoadTypeEnum, List<EndpointVariables>> Inputs { get; private set; } = new Dictionary<EnpointLoadTypeEnum, List<EndpointVariables>>
        {
            { EnpointLoadTypeEnum.NORMAL, new List<EndpointVariables>() },
            { EnpointLoadTypeEnum.SLOW, new List<EndpointVariables>() }
        };
        public Dictionary<EnpointLoadTypeEnum, List<EndpointVariables>> Outputs { get; private set; } = new Dictionary<EnpointLoadTypeEnum, List<EndpointVariables>>
        {
            { EnpointLoadTypeEnum.NORMAL, new List<EndpointVariables>() },
            { EnpointLoadTypeEnum.SLOW, new List<EndpointVariables>() }
        };
        public FlowRepository() { }

        public FlowResult AddFlow(Flow flow)
        {
            var res = flow.GetResult();
            Results.Add(res);
            return res;
        }

        public FlowResult AddFlow(Flow flow, EndpointVariables input, EndpointVariables output)
        {
            var res = flow.GetResult();
            Results.Add(res);//bind správného výstupu

            /*
            if (!Inputs.ContainsKey(input.Node))
            {
                Inputs.Add(input.Node, new List<EndPointDo> { input.EndPoint });
            }
            else
            {
                Inputs[input.Node].Add(input.EndPoint);
            }

            if (!Outputs.ContainsKey(output.Node))
            {
                Outputs.Add(output.Node, new List<EndPointDo> { output.EndPoint });
            }
            else
            {
                Outputs[output.Node].Add(output.EndPoint);
            }
            */

            if (input != null)
            {
                var IT = input.EndPoint.Delay == null ? EnpointLoadTypeEnum.NORMAL : EnpointLoadTypeEnum.SLOW;
                Inputs[IT].Add(input);
            }
            if (output != null)
            {
                var OT = input.EndPoint.Delay == null ? EnpointLoadTypeEnum.NORMAL : EnpointLoadTypeEnum.SLOW;
                Outputs[OT].Add(output);
            }

            return res;
        }

        public void Run()
        {
            foreach (FlowResult r in Results)
            {
                r.NewIteration();
            }
            foreach (FlowResult res in Results)
            {
                var val = res.Value;
            }
        }
    }
}
