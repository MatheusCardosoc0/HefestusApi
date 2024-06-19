using HefestusApi.DTOs.Pessoal;
using HefestusApi.Services.Pessoal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.PESSOAL

{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class personController : ControllerBase
    {
        private readonly IPersonService _personService;

        public personController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<IActionResult> GetAllPersons(string SystemLocationId)
        {
            var serviceResponse = await _personService.GetAllPersonsAsync( SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<IActionResult> GetPersonById(string SystemLocationId, int id)
        {
            var serviceResponse = await _personService.GetPersonByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _personService.CreatePersonAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            // Assumindo que a resposta inclui a entidade criada ou seu DTO
            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _personService.UpdatePersonAsync(SystemLocationId, id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<IActionResult> DeletePerson(string SystemLocationId, int id)
        {
            var serviceResponse = await _personService.DeletePersonAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchPersonByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _personService.SearchPersonByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}