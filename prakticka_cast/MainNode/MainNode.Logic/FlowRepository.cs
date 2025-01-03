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
                var OT = output.EndPoint.Delay == null ? EnpointLoadTypeEnum.NORMAL : EnpointLoadTypeEnum.SLOW;
                Outputs[OT].Add(output);
            }

            return res;
        }

        public void AddInput(EndpointVariables input)
        {
            var IT = input.EndPoint.Delay == null ? EnpointLoadTypeEnum.NORMAL : EnpointLoadTypeEnum.SLOW;
            var list = Inputs[IT];
            if (list.Any(x => x.Node == input.Node && x.EndPoint == input.EndPoint)) { return; }
            list.Add(input);
        }
        public void AddOutput(EndpointVariables output)
        {
            var IT = output.EndPoint.Delay == null ? EnpointLoadTypeEnum.NORMAL : EnpointLoadTypeEnum.SLOW;
            var list = Outputs[IT];
            if (list.Any(x => x.Node == output.Node && x.EndPoint == output.EndPoint)) { return; }
            list.Add(output);
        }

        public FlowResult GetFlowByName(string name)
        {
            var res = Results.FindAll(x => x.Name == name);
            if (res.Count == 0)
            {
                throw new Exception($"Flow with name {name} not found");
            }
            if (res.Count > 1)
            {
                throw new Exception($"More than one flow with name {name} found");
            }
            return res.First();
        }
        public void Clear()
        {
            Results.Clear();
            Inputs[EnpointLoadTypeEnum.NORMAL].Clear();
            Inputs[EnpointLoadTypeEnum.SLOW].Clear();
            Outputs[EnpointLoadTypeEnum.NORMAL].Clear();
            Outputs[EnpointLoadTypeEnum.SLOW].Clear();
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
