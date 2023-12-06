using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Administracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly DataContext _context;

        public userController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Person)
                //.ThenInclude(u => u.PersonGroups)
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("id")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound($"Usuário com o id {id} não existe");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto request)
        {
            var existingPerson = await _context.Person.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == request.Person.Id);

            if (existingPerson == null)
            {
                return NotFound($"Person não encontrada com o ID {request.Person.Id}");
            }

            if (existingPerson.User != null)
            {
                return BadRequest($"Person com ID {request.Person.Id} já tem um User associado.");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Name = request.Name,
                Password = hashedPassword,
                Person = existingPerson
            };

            existingPerson.User = newUser;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UserDto request)
        {
            var user = await _context.Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound($"User com o ID {id} não encontrado");
            }

            user.Name = request.Name;
            user.Password = request.Password;

            if (request.Person.Id != 0)
            {
                var person = await _context.Person.FindAsync(request.Person.Id);
                if (person == null)
                {
                    return NotFound($"Person com o ID {request.Person.Id} não encontrado");
                }

                user.Person = person;
            }
            else
            {
                user.Person = null;
            }

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return NotFound($"Usuário com o id {id} não encontrado");
            }

            _context.Remove(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while deleting user");
            }

            return NoContent();
        }

    }
}
