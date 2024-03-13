using EnergyCompany.WebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEndpointApiClient
{
    Task<Endpoint> GetEndpointBySerialNumberAsync(string serialNumber);
    Task<List<Endpoint>> GetAllEndpointsAsync();
    Task<bool> InsertNewEndpointAsync(Endpoint endpoint);
    Task<bool> UpdateSwitchStateAsync(string serialNumber, int newSwitchState);
    Task<bool> DeleteEndpointAsync(string serialNumber);
}