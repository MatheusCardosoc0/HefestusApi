using HefestusApi.DTOs.Pessoal;
using HefestusApi.Services.Pessoal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.PESSOAL

{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class personController : ControllerBase
    {
        private readonly IPersonService _personService;

        public personController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var serviceResponse = await _personService.GetAllPersonsAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var serviceResponse = await _personService.GetPersonByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] PersonRequestDataDto request)
        {
            var serviceResponse = await _personService.CreatePersonAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            // Assumindo que a resposta inclui a entidade criada ou seu DTO
            return CreatedAtAction(nameof(GetPersonById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonRequestDataDto request)
        {
            var serviceResponse = await _personService.UpdatePersonAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var serviceResponse = await _personService.DeletePersonAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return NoContent();
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<IActionResult> SearchPersonByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _personService.SearchPersonByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse.Data);
        }
    }
}