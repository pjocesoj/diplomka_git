namespace MainNode.Logic.Interfaces
{
    public interface ILoopExecutor
    {
        bool IsRunning { get; }
        ulong Iteration { get; }
        TimeSpan IterationDuration { get; }
        int Period { get; set; }

        event EventHandler<EventArgs> LoopFinished;

        Task Run();
        void Start();
        void Stop();
    }
}