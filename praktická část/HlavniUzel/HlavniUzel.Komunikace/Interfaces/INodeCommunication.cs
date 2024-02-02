using HlavniUzel.Komunikace.Dto;

namespace HlavniUzel.Komunikace.Interfaces
{
    public interface INodeCommunication
    {
        EndPointDto[] GetEndPoints();
    }
}
