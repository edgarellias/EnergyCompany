using EnergyCompany.WebAPI.Interfaces.Repositories;
using EnergyCompany.WebAPI.Interfaces.Services;
using EnergyCompany.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace EnergyCompany.WebAPI.Services
{
    public class EndpointService : IEndpointService
    {
        private readonly IEndpointRepository _endpointRepository;
        public EndpointService(IEndpointRepository endpointRepository) {
            _endpointRepository = endpointRepository;
        }

        public Models.Endpoint? GetEndpointBySerialNumber(string serialNumber)
        {
            return _endpointRepository.GetEndpointBySerialNumber(serialNumber);
        }

        public List<Models.Endpoint> GetEndpoints()
        {
            return _endpointRepository.ListAllEndpoints();
        }

        public void DeleteEndpoint(Models.Endpoint endpoint)
        {
           
            _endpointRepository.DeleteEndpoint(endpoint);

        }
        
        public void InsertNewEndpoint(Models.Endpoint endpoint)
        {
            ValidateEndpoint(endpoint);
            _endpointRepository.InsertEndpoint(endpoint);
        }

        public void UpdateEndPoint(Models.Endpoint endpoint)
        {
            ValidateSwitchState(endpoint);
            _endpointRepository.UpdateEndPoint(endpoint);
        }

        private void ValidateEndpoint(Models.Endpoint endpoint)
        {
            bool meterModelExists = Enum.IsDefined(typeof(MeterModel), endpoint.MeterModelId);
            bool switchStateExists = Enum.IsDefined(typeof(SwitchState), endpoint.SwitchState);

            if(!switchStateExists)
            {
                throw new ArgumentException("Invalid switch states");   
            }
            if (!meterModelExists)
            {
                throw new ArgumentException("Invalid meter model id");
            }
        }
        private void ValidateSwitchState(Models.Endpoint endpoint)
        {
            bool switchStateExists = Enum.IsDefined(typeof(SwitchState), endpoint.SwitchState);
            if (!switchStateExists)
            {
                throw new ArgumentException("Invalid switch states");
            }

        }
    }
}
