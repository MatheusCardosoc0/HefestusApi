using HefestusApi.DTOs.Administracao;
using HefestusApi.Services.Administracao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Administracao
{
    [Authorize(Policy = "Policy1OrPolicy2")]
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;

        public userController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{SystemLocationId}")]
        public async Task<ActionResult> GetAllUsers(string SystemLocationId)
        {
            var serviceResponse = await _userService.GetAllUsersAsync( SystemLocationId);
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{SystemLocationId}/{detailLevel}/{id}")]
        public async Task<ActionResult> GetUserById(string SystemLocationId, string id)
        {
            var serviceResponse = await _userService.GetUserByIdAsync(SystemLocationId, id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{SystemLocationId}/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchUserByName(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var serviceResponse = await _userService.SearchUserByNameAsync(searchTerm, detailLevel,SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost("{SystemLocationId}")]
        public async Task<ActionResult> CreateUser([FromBody] UserRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _userService.CreateUserAsync(request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            //return CreatedAtAction(nameof(GetUserById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
            return Ok(serviceResponse.Data);

        }

        [HttpPut("{SystemLocationId}/{id}")]
        public async Task<ActionResult> UpdateUser(string id, [FromBody] UserRequestDataDto request, string SystemLocationId)
        {
            var serviceResponse = await _userService.UpdateUserAsync(id, request, SystemLocationId);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
