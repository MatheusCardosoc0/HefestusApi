using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HefestusApi.Controllers.Others
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenGenerator;

        public class RequiredCampsForAuthentication
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public authController(DataContext context, TokenService tokenGenerator)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        public ActionResult SignIn(RequiredCampsForAuthentication userCredentials)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Name == userCredentials.Username && u.Password == userCredentials.Password);

            if (user == null)
                return Unauthorized();

            var token = _tokenGenerator.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpGet("{token}")]
        public ActionResult ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return Unauthorized();

            var userName = _tokenGenerator.ValidateToken(token);

            if (userName == null) return Unauthorized();

            return Ok(userName);
        }
    }
}
