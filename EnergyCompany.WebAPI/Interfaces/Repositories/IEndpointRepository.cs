using EnergyCompany.WebAPI.Models;

namespace EnergyCompany.WebAPI.Interfaces.Repositories
{
    public interface IEndpointRepository
    {
        void InsertEndpoint(Models.Endpoint endpoint);
        void DeleteEndpoint(Models.Endpoint endpoint);
        List<Models.Endpoint> ListAllEndpoints();
        Models.Endpoint? GetEndpointBySerialNumber(string serialNumber);
        void UpdateEndPoint(Models.Endpoint endpoint);

    }
}
