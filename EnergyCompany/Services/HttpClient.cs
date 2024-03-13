using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EnergyCompany.WebAPI.Dto;
using EnergyCompany.WebAPI.Models;
using Newtonsoft.Json;

public class EndpointApiClient : IEndpointApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;

    public EndpointApiClient(string apiUrl)
    {
        _httpClient = new HttpClient();
        _apiUrl = apiUrl;
    }


    public async Task<Endpoint> GetEndpointBySerialNumberAsync(string serialNumber)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_apiUrl}/{serialNumber}");

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Endpoint>(responseBody);
        }

        return null;
    }

    public async Task<List<Endpoint>> GetAllEndpointsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Endpoint>>(responseBody);
        }

        return new List<Endpoint>();
    }

    public async Task<bool> InsertNewEndpointAsync(Endpoint endpoint)
    {
        string jsonContent = JsonConvert.SerializeObject(endpoint);
        HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(_apiUrl, content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateSwitchStateAsync(string serialNumber, int newSwitchState)
    {
        var updateData = new SwitchStateUpdateDto { SerialNumber = serialNumber, NewSwitchState = newSwitchState };
        string jsonContent = JsonConvert.SerializeObject(updateData);
        HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PatchAsync($"{_apiUrl}/UpdateSwitchState", content);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEndpointAsync(string serialNumber)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{_apiUrl}/{serialNumber}");

        return response.IsSuccessStatusCode;
    }
}