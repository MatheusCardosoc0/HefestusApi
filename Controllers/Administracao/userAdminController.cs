using HefestusApi.DTOs.Administracao;
using HefestusApi.Services.Administracao.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Administracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class userAdminController : ControllerBase
    {
        private readonly IUserAdminService _userAdminService;

        public userAdminController(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }

        public class AuthRequest
        {
            public string password { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAdmin()
        {
            var serviceResponse = await _userAdminService.CreateUserAdminAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            //return CreatedAtAction(nameof(GetUserById), new { id = serviceResponse?.Data?.Id }, serviceResponse?.Data);
            return Ok(serviceResponse.Data);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser()
        {
            var serviceResponse = await _userAdminService.NewPasswordUserAdminAsync();
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }

        [HttpPut("auth")]
        public async Task<ActionResult> AuthUserAdmin([FromBody] AuthRequest request)
        {
            var serviceResponse = await _userAdminService.AuthUserAdminAsync(request.password);
            if (!serviceResponse.Success)
            {
                return BadRequest(serviceResponse.Message);
            }

            return Ok(serviceResponse.Data);
        }
    }
}
