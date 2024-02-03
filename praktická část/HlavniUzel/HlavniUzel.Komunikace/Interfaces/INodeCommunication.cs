using HlavniUzel.Komunikace.Dto;

namespace HlavniUzel.Komunikace.Interfaces
{
    public interface INodeCommunication
    {
        Task<EndPointDto[]> GetEndPoints();
        Task<ValuesDto?> GetValues(string endpoint);
    }
}
