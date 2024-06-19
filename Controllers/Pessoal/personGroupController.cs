using HefestusApi.DTOs.Pessoal;
using HefestusApi.Services;
using HefestusApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.PESSOAL
{
    [Authorize(Policy = "Policy1")]
    [Route("api/[controller]")]
    [ApiController]
    public class personGroupController : ControllerBase
    {
        private readonly IPersonGroupService _personGroupService;

        public personGroupController(IPersonGroupService personGroupService)
        {
            _personGroupService = personGroupService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<ActionResult> GetAllPersonGroups(string SystemLocationId)
        {
            var serviceResponse = await _personGroupService.GetAllPersonGroupsAsync( SystemLocationId);
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<ActionResult> GetPersonGroupById(string SystemLocationId, int id)
        {
            var serviceResponse = await _personGroupService.GetPersonGroupByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchPersonGroupByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _personGroupService.SearchPersonGroupByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<ActionResult> CreatePersonGroup([FromBody] PersonGroupRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _personGroupService.CreatePersonGroupAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<ActionResult> UpdatePersonGroup(int id, [FromBody] PersonGroupRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _personGroupService.UpdatePersonGroupAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }

        [HttpDelete("{SystemLocationId}/{id}")]
        public async Task<ActionResult> DeletePersonGroup(string SystemLocationId, int id)
        {
            var serviceResponse = await _personGroupService.DeletePersonGroupAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
