using MainNode.Logic.Evaluation;

namespace MainNode.Logic
{
    public class FlowRepository
    {
        public List<Flow<int>> Flows { get; private set; } = new List<Flow<int>>();

        public FlowRepository() { }

        public void AddFlow(Flow<int> flow)
        {
            Flows.Add(flow);
        }
    }
}
