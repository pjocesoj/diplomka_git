using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<Flow> Flows { get; private set; } = new List<Flow>();

        public FlowRepository() { }

        public void AddFlow(Flow flow)
        {
            Flows.Add(flow);
        }
    }
}
