namespace MainNode.Logic
{
    /// <summary>
    /// objekt zajišťující běh uživatelem zadané smyčky
    /// </summary>
    public class LoopExecutor
    {
        private readonly FlowRepository _flowRepo;
        private readonly NodeRepository _nodeRepo;

        public bool IsRunning { get; private set; } = false;
        public LoopExecutor(FlowRepository flowRepo, NodeRepository nodeRepo)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
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

        //dočasné řešení než převedu na thread
        private async Task TaskLoop()
        {
            while (IsRunning)
            {
                await Run();
            }
        }

        //zvážit thread
        public async Task Run()
        {
            await loadData();
            _flowRepo.Run();
            await writeData();
        }
        private async Task loadData()
        {
            /*
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
            */
            var normal = _flowRepo.Inputs[EnpointLoadTypeEnum.NORMAL];
            foreach (var ep in normal)
            {
                await ep.getValues();
            }
            var slow = _flowRepo.Inputs[EnpointLoadTypeEnum.SLOW];
            foreach (var ep in slow)
            {
                if (!ep.Loading) { _ = ep.Load(); }
                if(!ep.Loaded) { _ = ep.UpdateValues(); }
            }
        }
        private async Task writeData()
        {
            /*
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
            */
            var normal = _flowRepo.Inputs[EnpointLoadTypeEnum.NORMAL];
            foreach (var ep in normal)
            {
                await ep.getValues();
            }
        }
    }
}
