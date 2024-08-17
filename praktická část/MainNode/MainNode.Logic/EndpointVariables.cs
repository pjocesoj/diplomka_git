using MainNode.Logic.Do;

namespace MainNode.Logic
{
    public class EndpointVariables
    {
        public List<string> Variables { get; set; } = new List<string>();

        public Node Node { get; set; }

        public EndPointDo EndPoint { get; set; }
    }
}