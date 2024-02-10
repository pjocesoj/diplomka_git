using HlavniUzel.Komunikace.Dto;

namespace HlavniUzel.Komunikace.Interfaces
{
    public interface INodeCommunication
    {
        Task<EndPointDto[]> GetEndPoints();
        Task<ValuesDto?> GetValues(EndPointPath path);
        Task<bool> SetValues(EndPointPath path, ValuesDto vals);
    }
}
