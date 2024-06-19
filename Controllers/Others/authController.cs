using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Utilities.functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HefestusApi.Controllers.Others
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenGenerator;

        public class RequiredCampsForAuthenticationUser
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public authController(DataContext context, TokenService tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }


        [Authorize(Policy = "Policy3")]
        [HttpPost]
        public async Task<ActionResult> SignIn(RequiredCampsForAuthenticationUser userCredentials)
        {
            var systemLocationIdFromToken = User.GetSystemLocationId();

            var user = await _context.Users
                .Include(x => x.Person)
                .FirstOrDefaultAsync(u => u.Name == userCredentials.Username && u.SystemLocationId == systemLocationIdFromToken);

            if (user == null)
                return BadRequest("Usuário não encontrado");

            bool validPassword = BCrypt.Net.BCrypt.Verify(userCredentials.Password, user.Password);
            if (!validPassword)
                return BadRequest("Senha incorreta");

            // Obter systemLocationId do token

            if (systemLocationIdFromToken == null)
                return Unauthorized("Token inválido ou expirado");

            // Comparar systemLocationId do token com o do usuário
            if (user.SystemLocationId.ToString() != systemLocationIdFromToken)
                return Unauthorized("systemLocationId não compatível");

            var token = _tokenGenerator.GenerateTokenUser(user);

            return Ok(new { token = token });
        }

        [HttpGet("{token}")]
        public ActionResult ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return Unauthorized("Token invalido");

            var userName = _tokenGenerator.ValidateTokenUser(token);

            if (userName == null) return Unauthorized("Token invalido");

            return Ok(userName);
        }
    }
}
