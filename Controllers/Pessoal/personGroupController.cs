using HefestusApi.DTOs.Pessoal;
using HefestusApi.Services;
using HefestusApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.PESSOAL
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class personGroupController : ControllerBase
    {
        private readonly IPersonGroupService _personGroupService;

        public personGroupController(IPersonGroupService personGroupService)
        {
            _personGroupService = personGroupService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPersonGroups()
        {
            var serviceResponse = await _personGroupService.GetAllPersonGroupsAsync();
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{locationId}/{id}")]
        public async Task<ActionResult> GetPersonGroupById(int id)
        {
            var serviceResponse = await _personGroupService.GetPersonGroupByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchPersonGroupByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _personGroupService.SearchPersonGroupByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePersonGroup([FromBody] PersonGroupRequestDataDto request)
        {
            var serviceResponse = await _personGroupService.CreatePersonGroupAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return CreatedAtAction(nameof(GetPersonGroupById), new { id = serviceResponse.Data.Id }, serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePersonGroup(int id, [FromBody] PersonGroupRequestDataDto request)
        {
            var serviceResponse = await _personGroupService.UpdatePersonGroupAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePersonGroup(int id)
        {
            var serviceResponse = await _personGroupService.DeletePersonGroupAsync(id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
