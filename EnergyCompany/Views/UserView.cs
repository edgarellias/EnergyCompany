using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using EnergyCompany.WebAPI.Models;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using EnergyCompany.WebAPI.Controllers;
using Formatting = Newtonsoft.Json.Formatting;
using System.Net.Http.Headers;
using EnergyCompany.WebAPI.Dto;
using System.Net.Http.Json;

class UserView
{
    string baseApiUrl = "http://localhost:5120/api/Endpoint"; // Substitua pelo URL real da sua API

    private  string UserShowMenu()
    {
        Console.WriteLine("\nPlease, select one action to perfom(insert its respective number)\n");
        Console.WriteLine("1 - Insert a new endpoint \n" +
            "2 - Edit an existing endpoint \n" +
            "3 - Delete an existing endpoint \n" +
            "4 - List all endpoints \n" +
            "5 - Find an endpoint by your serial number \n" +
            "6 - Exit the program");
        string option = Console.ReadLine();
        return option;

    }
    public void UserShowOptions()
    {
        List<Endpoint> endpointList = new List<Endpoint>();
        
        bool continueExecution = true;
        do
        {
            string option = UserShowMenu();
            switch (option)
            {
                case "1":
                    Endpoint endpointInsert = CreateEndpointForInsert();
                    InsertNewEndpoint(baseApiUrl, endpointInsert);
                    endpointList.Add(endpointInsert);
                    break;
                case "2":
                    SwitchStateUpdateDto switchStateUpdateDto = EndpointUpdateSwitchState();
                    UpdateSwitchState(baseApiUrl, switchStateUpdateDto);
                    break;
                case "3":
                    Console.Write("Enter the serial number: ");
                    string serialNumber = Console.ReadLine();
                    Endpoint endpoint = new Endpoint();
                    endpoint.SerialNumber = serialNumber;
                    DeleteEndpoint(baseApiUrl, serialNumber);
                    break;
                case "4":
                    GetAllEndpoints(baseApiUrl);
                    break;
                case "5":
                    Console.Write("Enter the serial number: ");
                    string serialNumberGet = Console.ReadLine();
                    Endpoint endpointGetSerialNumber = new Endpoint();
                    endpointGetSerialNumber.SerialNumber = serialNumberGet;
                    GetEndpointBySerialNumber(baseApiUrl, endpointGetSerialNumber.SerialNumber);
                    break;
                case "6":
                    Console.WriteLine("Are you sure? [Y]/[N]");
                    string exit = Console.ReadLine().ToUpper();
                    if(exit == "Y")
                    {
                        Console.WriteLine("Bye bye...");
                        continueExecution = false;
                    }
                    break;
                default:
                    Console.WriteLine("Enter a valid option.");
                    break;
            }
        } while (continueExecution);


    }

    private static SwitchStateUpdateDto EndpointUpdateSwitchState()
    {
        Console.Write("Enter the serial number: ");
        string serialNumber = Console.ReadLine();
        Console.Write("Enter the switch state: ");
        string switchStateString = Console.ReadLine();
        int.TryParse(switchStateString, out int switchState);
        SwitchStateUpdateDto switchStateUpdateDto = new SwitchStateUpdateDto
        {
            SerialNumber = serialNumber,
            NewSwitchState = switchState
        };
        return switchStateUpdateDto;
    }

    private static Endpoint CreateEndpointForInsert()
    {
        Console.Write("Enter the serial number: ");
        string serialNumber = Console.ReadLine();
        Console.Write("Enter the meter model ID: ");
        string meterModelIdString = Console.ReadLine();
        int.TryParse(meterModelIdString, out int meterModelId);
        Console.Write("Enter the meter number: ");
        string meterNumberString = Console.ReadLine();
        int.TryParse(meterNumberString, out int meterNumber);
        Console.Write("Enter the meter firmware version: ");
        string meterFirmwareVersion = Console.ReadLine();
        Console.Write("Enter the switch state: ");
        string switchStateString = Console.ReadLine();
        int.TryParse(switchStateString, out int switchState);
        Endpoint endpoint = new Endpoint();
        endpoint.MeterNumber = meterNumber;
        endpoint.SerialNumber = serialNumber;
        endpoint.MeterModelId = meterModelId;
        endpoint.MeterFirmwareVersion = meterFirmwareVersion;
        endpoint.SwitchState = switchState;
        return endpoint;
    }

    static async Task GetEndpointBySerialNumber(string apiUrl, string serialNumber)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"{apiUrl}/serialNumber?serialNumber={serialNumber}";
            HttpResponseMessage response = client.Send(new HttpRequestMessage(HttpMethod.Get, url));


            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var endpoint = JsonConvert.DeserializeObject<Endpoint>(responseBody);

                Console.WriteLine("Endpoint found:");
                Console.WriteLine(JsonConvert.SerializeObject(endpoint, Formatting.Indented));
            }
            else
            {
                string message = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Error: {message}");
            }
        }
    }

    static  void GetAllEndpoints(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.Send(new HttpRequestMessage(HttpMethod.Get, apiUrl));


            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var endpoints = JsonConvert.DeserializeObject<List<Endpoint>>(responseBody);


                Console.WriteLine("All endpoints:");
                Console.WriteLine(JsonConvert.SerializeObject(endpoints, Formatting.Indented));
            }
            else
            {
                string message = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Error: {message}");
            }
        }
    }

    static void DeleteEndpoint(string apiUrl, string serialNumber)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"{apiUrl}?serialNumber={serialNumber}";

            HttpResponseMessage response = client.Send(new HttpRequestMessage(HttpMethod.Delete, url));
           
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Endpoint deleted successfully.");
            }
            else
            {
                string message = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Error: {message}");
            }
        }
    }

    static  void InsertNewEndpoint(string apiUrl, Endpoint newEndpoint)
    {
        using (HttpClient client = new HttpClient())
        {
            
            string jsonContent = JsonConvert.SerializeObject(newEndpoint);
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.Send(new HttpRequestMessage(HttpMethod.Post, apiUrl)
            {
                Content = content
            });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Endpoint inserted successfully.");
            }
            else
            {
                string message = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Error: {message}");
            }
        }
    }

    static void UpdateSwitchState(string apiUrl, SwitchStateUpdateDto switchStateUpdateDto)
    {
        using (HttpClient client = new HttpClient())
        {
            string jsonContent = JsonConvert.SerializeObject(switchStateUpdateDto);
            
            
            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.Send(new HttpRequestMessage(HttpMethod.Patch, apiUrl)
            {
                Content = content
            });

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("SwitchState updated successfully.");
            }
            else
            {
                string message = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Error: {message}");
            }
        }
    }
}