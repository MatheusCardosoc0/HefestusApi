using HefestusApi.DTOs.Administracao;
using HefestusApi.Services.Administracao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Administracao
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService _userService;

        public userController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var serviceResponse = await _userService.GetAllUsersAsync();
            if (!serviceResponse.Success)
            {
                return StatusCode(500, serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("{detailLevel}/{locationId}/{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var serviceResponse = await _userService.GetUserByIdAsync(id);
            if (!serviceResponse.Success)
            {
                return NotFound(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> SearchUserByName(string searchTerm, string detailLevel)
        {
            var serviceResponse = await _userService.SearchUserByNameAsync(searchTerm, detailLevel);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserRequestDataDto request)
        {
            var serviceResponse = await _userService.CreateUserAsync(request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            //return CreatedAtAction(nameof(GetUserById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
            return CreatedAtAction(nameof(GetUserById), new { id = serviceResponse?.Data?.Id, detailLevel = "complete", locationId = (int?)null }, serviceResponse?.Data);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserRequestDataDto request)
        {
            var serviceResponse = await _userService.UpdateUserAsync(id, request);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return NoContent();
        }
    }
}
