using MainNode.Communication.Dto;

namespace MainNode.Communication.Interfaces
{
    public interface INodeCommunication
    {
        string Address { get; }
        void Init(string address);

        Task<EndPointDto[]> GetEndPoints();
        Task<ValuesDto?> GetValues(EndPointPath path);
        Task<bool> SetValues(EndPointPath path, ValuesDto vals);
    }
}
