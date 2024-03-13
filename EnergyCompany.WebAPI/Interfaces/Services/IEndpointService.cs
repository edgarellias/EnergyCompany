namespace EnergyCompany.WebAPI.Interfaces.Services
{
    public interface IEndpointService
    {
        Models.Endpoint? GetEndpointBySerialNumber(string serialNumber);
        List<Models.Endpoint> GetEndpoints();
        void DeleteEndpoint(Models.Endpoint endpoint);
        void InsertNewEndpoint(Models.Endpoint endpoint);
        void UpdateEndPoint(Models.Endpoint endpoint);
    }
}
