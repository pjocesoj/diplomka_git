namespace MainNode.Logic.Interfaces
{
    public interface INodeRepository
    {
        List<Node> Nodes { get; }

        Task AddNode(Node node);
        Task<Dictionary<Node, string>> LoadNodes(string json);
        string SaveNodes();

        static int Count { get; private set; } = 0;
    }
}