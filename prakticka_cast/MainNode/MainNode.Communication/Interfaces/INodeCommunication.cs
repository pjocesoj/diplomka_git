using MainNode.Communication.Dto;

namespace MainNode.Communication.Interfaces
{
    public interface INodeCommunication
    {
        string Address { get; }
        string AddressType { get; }
        void Init(string address);

        Task<EndPointDto[]?> GetEndPoints();
        Task<ValuesDto?> GetValues(EndPointPath path,int? delay, ValuesDto? args = null);
    }
}
