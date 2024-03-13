using EnergyCompany.WebAPI.Interfaces.Repositories;
using EnergyCompany.WebAPI.Models;

namespace EnergyCompany.WebAPI.Context.Repositories
{
    public class EndpointsRepository : IEndpointRepository
    {
        private readonly EnergyCompanyContext _context;
        public EndpointsRepository(EnergyCompanyContext context) {
            _context = context;
        }

        public void InsertEndpoint(Models.Endpoint endpoint)
        {
            _context.Add(endpoint);
            _context.SaveChanges();
        }

        public void UpdateEndPoint(Models.Endpoint endpoint)
        {
            _context.Update(endpoint);
            _context.SaveChanges();
        }

        public void DeleteEndpoint(Models.Endpoint endpoint)
        {
            _context.Remove(endpoint);
            _context.SaveChanges();
        }

        public List<Models.Endpoint> ListAllEndpoints()
        {
            return _context.Endpoints.ToList();
        }

        public Models.Endpoint? GetEndpointBySerialNumber(string serialNumber)
        {
            return _context.Endpoints.Where(i => i.SerialNumber == serialNumber).FirstOrDefault();
        }

    }
}
