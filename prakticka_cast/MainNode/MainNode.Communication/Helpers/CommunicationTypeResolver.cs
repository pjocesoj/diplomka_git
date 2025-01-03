using MainNode.Communication.Interfaces;

namespace MainNode.Communication.Helpers
{
    public static class CommunicationTypeResolver
    {
        public static INodeCommunication GetCommunicationType(string addressType)
        {
            switch(addressType)
            {
                case "http":
                    return new HttpNodeCommunication();
                default:
                    return null;
            }
        }
    }
}
