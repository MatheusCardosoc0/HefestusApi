using HefestusApi.DTOs.Administracao;
using HefestusApi.Services.Administracao.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Administracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class systemLocationController : ControllerBase
    {
        private readonly ISystemLocationService _systemLocationService;

        public systemLocationController(ISystemLocationService systemLocationService)
        {
            _systemLocationService = systemLocationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllSystemLocations()
        {
            var serviceResponse = await _systemLocationService.GetAllSystemLocationsAsync();
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{locationId}/{id}")]
        public async Task<ActionResult> GetSystemLocationById(int id)
        {
            var serviceResponse = await _systemLocationService.GetSystemLocationByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchSystemLocationByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _systemLocationService.SearchSystemLocationByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSystemLocation([FromBody] SystemLocationRequestDataDto request)
        {
            var serviceResponse = await _systemLocationService.CreateSystemLocationAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return CreatedAtAction(nameof(GetSystemLocationById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSystemLocation(int id, [FromBody] SystemLocationRequestDataDto request)
        {
            var serviceResponse = await _systemLocationService.UpdateSystemLocationAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
