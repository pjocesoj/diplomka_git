namespace MainNode.Logic
{
    /// <summary>
    /// objekt zajišťující běh uživatelem zadané smyčky
    /// </summary>
    public class LoopExecutor
    {
        private readonly FlowRepository _flowRepo;
        private readonly NodeRepository _nodeRepo;
        private Thread _thread;
        private CancellationTokenSource _cancelLoop;
        public bool IsRunning { get; private set; } = false;

        public LoopExecutor(FlowRepository flowRepo, NodeRepository nodeRepo)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;

            _thread= new Thread(Run);
            _thread.Name = "LoopExecutor";
        }

        public void Start()
        {
            IsRunning = true;
            _cancelLoop = new CancellationTokenSource();
            _thread.Start(_cancelLoop.Token);
        }
        public void Stop()
        {
            IsRunning = false;
            _cancelLoop.Cancel();
        }


        //zvážit thread
        public async void Run()
        {         
            await loadData();
            _flowRepo.Run();
            await writeData();
        }
        private async Task loadData()
        {
            foreach (Node n in _nodeRepo.Nodes)
            {
                await n.GetAllValues();
            }
        }
        private async Task writeData()
        {
            var sb= new System.Text.StringBuilder();
            foreach(var f in _flowRepo.Results)
            {
                sb.AppendLine($"{f.Value}");
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
