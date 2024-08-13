using MainNode.Logic.Do;
using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<FlowResult> Results { get; private set; } = new List<FlowResult>();
        public Dictionary<Node, List<EndPointDo>> Inputs { get; private set; } = new Dictionary<Node, List<EndPointDo>>();

        public FlowRepository() { }

        public FlowResult AddFlow(Flow flow)
        {
            var res = flow.GetResult();
            Results.Add(res);
            return res;
        }
        public FlowResult AddFlow(Flow flow, Node node, List<EndPointDo> endPoints)
        {
            var res = flow.GetResult();
            Results.Add(res);

            if (!Inputs.ContainsKey(node))
            {
                Inputs.Add(node, endPoints);
            }
            else
            {
                Inputs[node].AddRange(endPoints);
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

        public IEnumerable<ValueDo> GetInputs()
        {
            return Results.SelectMany(x => x.GetInputs());
        }
    }
}
