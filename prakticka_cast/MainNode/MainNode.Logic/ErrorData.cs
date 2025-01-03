using MainNode.Logic.Do;

namespace MainNode.Logic
{
    public class ErrorData
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }

        public EndPointDo EndPoint { get; set; }
    }
}
