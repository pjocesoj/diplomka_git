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
            _ = Task.Run(async () => TaskLoop());
        }
        public void Stop()
        {
            IsRunning = false;
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
        public async Task Run()
        {
            await loadData();
            _flowRepo.Run();
            await writeData();
        }
        private async Task loadData()
        {
            foreach (var pair in _flowRepo.Inputs)
            {
                var node = pair.Key;
                var endPoints = pair.Value;
                foreach (var ep in endPoints)
                {
                    try
                    {
                        await node.GetValues(ep);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
        private async Task writeData()
        {
            foreach (var pair in _flowRepo.Outputs)
            {
                var node = pair.Key;
                var endPoints = pair.Value;
                foreach (var ep in endPoints)
                {
                    try
                    {
                        await node.GetValues(ep);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
