using HefestusApi.DTOs.Administracao;
using HefestusApi.Services.Administracao.Interfaces;
using HefestusApi.Utilities.functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Administracao
{
    [Authorize(Policy = "Policy2")]
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

        [HttpGet("{detailLevel}/{id}")]
        public async Task<ActionResult> GetSystemLocationById(string detailLevel, string id)
        {
            var serviceResponse = await _systemLocationService.GetSystemLocationByIdAsync(detailLevel, id);
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

        [Authorize(Policy = "Policy3")]
        [AllowAnonymous]
        [HttpGet("auth/{token}")]
        public async Task<ActionResult> ValidateToken(string token)
        {
            var serviceResponse = await _systemLocationService.ValidateTokenAsync(token);
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

            //return CreatedAtAction(nameof(GetSystemLocationById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
            return Ok(serviceResponse.Data);
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public async Task<ActionResult> AuthSystemLocation([FromBody] RequiredCampsForAuthentication request)
        {
            var serviceResponse = await _systemLocationService.AuthSystemLocationsAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            //return CreatedAtAction(nameof(GetSystemLocationById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
            return Ok(serviceResponse.Data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSystemLocation([FromBody] SystemLocationRequestDataDto request, string id)
        {
            var serviceResponse = await _systemLocationService.UpdateSystemLocationAsync(request, id);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
