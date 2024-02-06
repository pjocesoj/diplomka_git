namespace HlavniUzel.Logika
{
    public class NodeRepository
    {
        public List<Node> Nodes { get; private set; } = new List<Node>();

        public NodeRepository() { }

        public async Task AddNode(Node node)
        {
            if (string.IsNullOrEmpty(node.Address)) { throw new ArgumentException("address"); }
            if (string.IsNullOrEmpty(node.Name)) { throw new ArgumentException("name"); }

            try
            {
                await node.GetEndPoints();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
