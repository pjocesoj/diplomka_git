using HlavniUzel.Exceptions;

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
                await node.GetAllValues();
            }
            catch (ApplicationException ex) { throw new ApplicationException(ex.Message, ex); }
            catch (CommunicationException ex) { throw new CommunicationException("cant access node", ex); }
            catch (InvalidDataException ex) { throw new InvalidDataException("cant read recaived data", ex); }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

            if (node.EndPoints.Length == 0) { throw new NoEndPointException("no endpoints"); }
            Nodes.Add(node);
        }
    }
}
