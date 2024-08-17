using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<FlowResult> Results { get; private set; } = new List<FlowResult>();
        public Dictionary<Node, List<EndPointDo>> Inputs { get; private set; } = new Dictionary<Node, List<EndPointDo>>();
        public Dictionary<Node, List<EndPointDo>> Outputs { get; private set; } = new Dictionary<Node, List<EndPointDo>>();

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
