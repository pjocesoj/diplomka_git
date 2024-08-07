namespace MainNode.Logic
{
    /// <summary>
    /// objekt zajišťující běh uživatelem zadané smyčky
    /// </summary>
    public class LoopExecutor
    {
        private readonly FlowRepository _flowRepo;
        private readonly NodeRepository _nodeRepo;

        public LoopExecutor(FlowRepository flowRepo, NodeRepository nodeRepo)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
        }

        //zvážit thread
        public async Task Run()
        {         
            foreach (Node n in _nodeRepo.Nodes)
            {
                await n.GetAllValues();
            }
            _flowRepo.Run();
        }
    }
}
