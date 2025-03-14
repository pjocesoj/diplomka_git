using System.Diagnostics;
using MainNode.Logic.Enums;
using MainNode.Logic.Interfaces;

namespace MainNode.Logic
{
    /// <summary>
    /// objekt zajišťující běh uživatelem zadané smyčky
    /// </summary>
    public class LoopExecutor : ILoopExecutor
    {
        private readonly IFlowRepository _flowRepo;
        private readonly INodeRepository _nodeRepo;

        public event EventHandler<EventArgs> LoopFinished;
        public bool IsRunning { get; private set; } = false;
        public ulong Iteration { get; private set; } = 0;

        public TimeSpan IterationDuration => _iterationStopwatch.Elapsed;
        private Stopwatch _iterationStopwatch = new Stopwatch();

        public int Period { get; set; } = 1000;
        private Timer _timer;
        private bool _lock = false;
        public LoopExecutor(IFlowRepository flowRepo, INodeRepository nodeRepo)
        {
            _flowRepo = flowRepo;
            _nodeRepo = nodeRepo;
        }


        public void Start()
        {
            IsRunning = true;
            _timer = new Timer(async (e) => await Run(), null, 0, Period);
        }
        public void Stop()
        {
            IsRunning = false;
            _timer.Dispose();
        }

        public async Task Run()
        {
            if (_lock) { return; }
            _lock = true;
            _iterationStopwatch.Restart();

            await loadData();
            _flowRepo.Run();
            await writeData();
            Iteration++;
            LoopFinished?.Invoke(this, new EventArgs());

            _iterationStopwatch.Stop();
            Debug.WriteLine($"Iteration {Iteration} took {_iterationStopwatch.ElapsedMilliseconds} ms");
            _lock = false;
        }
        private async Task loadData()
        {
            var normal = _flowRepo.Inputs[EnpointLoadTypeEnum.NORMAL];
            foreach (var ep in normal)
            {
                await ep.GetValues();
            }
            var slow = _flowRepo.Inputs[EnpointLoadTypeEnum.SLOW];
            foreach (var ep in slow)
            {
                if (ep.Loaded) { _ = ep.UpdateValues(); }
                if (!ep.Loading) { _ = ep.Load(); }
            }
        }
        private async Task writeData()
        {
            var normal = _flowRepo.Inputs[EnpointLoadTypeEnum.NORMAL];
            foreach (var ep in normal)
            {
                await ep.GetValues();
            }
        }
    }
}
