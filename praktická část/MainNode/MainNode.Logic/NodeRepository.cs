using MainNode.Exceptions;
using System.Text.Json;

namespace MainNode.Logic
{
    public class NodeRepository
    {
        public static int Count { get; private set; } = 0;
        public List<Node> Nodes { get; private set; } = new List<Node>();

        public NodeRepository() { }

        public async Task AddNode(Node node)
        {
            if (string.IsNullOrEmpty(node.Address)) { throw new ArgumentException("address"); }
            if (string.IsNullOrEmpty(node.Name)) { throw new ArgumentException("name"); }

            if (Nodes.Any(x => x.Name == node.Name)) { throw new ArgumentException("name must be unique"); }

            try
            {
                await node.GetEndPoints();
                await node.GetAllValues();
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message, ex); }
            catch (CommunicationException ex) { throw new CommunicationException("cant access node", ex); }
            catch (InvalidDataException ex) { throw new InvalidDataException("cant read recaived data", ex); }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

            if (node.EndPoints.Length == 0) { throw new NoEndPointException("no endpoints"); }
            Nodes.Add(node);

            Count++;
        }

        public async Task<Dictionary<Node, string>> LoadNodes(string json)
        {
            var failed = new Dictionary<Node, string>();

            var loaded = new List<Node>();
            try
            {
                loaded = JsonSerializer.Deserialize<List<Node>>(json);
            }
            catch (Exception e)
            {
                throw new FileLoadException("deserialization failed",e);
            }

            foreach (var n in loaded)
            {
                try
                {
                    await AddNode(n);
                }
                catch (Exception e)
                {
                    failed.Add(n, e.Message);
                }
            }
            return failed;
        }
    }
}
