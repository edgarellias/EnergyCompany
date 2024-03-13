using EnergyCompany.WebAPI.Dto;
using EnergyCompany.WebAPI.Interfaces.Services;
using EnergyCompany.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergyCompany.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EndpointController : ControllerBase
    {
        private readonly IEndpointService _endpointService;
        public EndpointController(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        [HttpGet("serialNumber")]
        public IActionResult GetEndpointBySerialNumber(string serialNumber)
        {
            var endpoint = _endpointService.GetEndpointBySerialNumber(serialNumber);
            if (endpoint == null)
            {
                return NotFound();
            }
            return Ok(endpoint);
        }

        [HttpGet]
        public IActionResult GetAllEndpoints()
        {
            var endpoints = _endpointService.GetEndpoints();
            return Ok(endpoints);
        }

        [HttpDelete]
        public IActionResult DeleteEndpoint(string serialNumber)
        {
            var endpoint = _endpointService.GetEndpointBySerialNumber(serialNumber);
            if (endpoint == null)
            {
                return NotFound();
            }
            _endpointService.DeleteEndpoint(endpoint);
            return NoContent();
        }

        [HttpPost]
        public IActionResult InsertNewEndpoint(Models.Endpoint endpoint)
        {

            var endpointExist = _endpointService.GetEndpointBySerialNumber(endpoint.SerialNumber);

            if (endpointExist != null)
            {
                return BadRequest("This endpoints already exists");
            }
            try
            {
                _endpointService.InsertNewEndpoint(endpoint);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpPatch]
        public IActionResult UpdateSwitchState([FromBody] SwitchStateUpdateDto switchStateUpdate)
        {
            var endpointExist = _endpointService.GetEndpointBySerialNumber(switchStateUpdate.SerialNumber);

            if (endpointExist == null)
            {
                return BadRequest("This endpoint not exists");
            }
            try
            {
                endpointExist.SwitchState = switchStateUpdate.NewSwitchState;
                _endpointService.UpdateEndPoint(endpointExist);
            }catch(ArgumentException ex)
            {
                return  BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
