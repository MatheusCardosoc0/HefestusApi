﻿using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> SignIn(RequiredCampsForAuthentication userCredentials)
        {
            var user = await _context.Users
                .Include(x => x.Person)
                .FirstOrDefaultAsync(u => u.Name == userCredentials.Username);

            if (user == null)
                return BadRequest("Usuário não encontrado");

            bool validPassword = BCrypt.Net.BCrypt.Verify(userCredentials.Password, user.Password);
            if (!validPassword)
                return BadRequest("Senha incorreta");

            var token = _tokenGenerator.GenerateToken(user);

            return Ok(new {token = token});
        }

        [HttpGet("{token}")]
        public ActionResult ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return Unauthorized("Token invalido");

            var userName = _tokenGenerator.ValidateToken(token);

            if (userName == null) return Unauthorized("Token invalido");

            return Ok(userName);
        }
    }
}
