using HefestusApi.DTOs.Pessoal;
using HefestusApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.PESSOAL
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class cityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public cityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<ActionResult> GetAllCities(string SystemLocationId)
        {
            var serviceResponse = await _cityService.GetAllCitiesAsync( SystemLocationId);
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<ActionResult> GetCityById(string SystemLocationId, int id)
        {
            var serviceResponse = await _cityService.GetCityByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchCityByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _cityService.SearchCityByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<ActionResult> CreateCity([FromBody] CityRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _cityService.CreateCityAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<ActionResult> UpdateCity(int id, [FromBody] CityRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _cityService.UpdateCityAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<ActionResult> DeleteCity(string SystemLocationId, int id)
        {
            var serviceResponse = await _cityService.DeleteCityAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
