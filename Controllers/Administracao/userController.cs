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
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUserById()
        {
            var users = await _context.Users
                .Include(u => u.Person)
                .Select(u => new UserDto
                {
                    Name = u.Name,
                    Password = u.Password, 
                    Person = new PersonRequiredData(u.Person.Id, u.Person.UrlImage)
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto request)
        {
            var existingPerson = await _context.Person.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == request.Person.Id);

            if (existingPerson == null)
            {
                return NotFound($"Person não encontrada com o ID {request.Person.Id}");
            }

            // Verificar se a Person já tem um User associado
            if (existingPerson.User != null)
            {
                return BadRequest($"Person com ID {request.Person.Id} já tem um User associado.");
            }

            var newUser = new User
            {
                Name = request.Name,
                Password = request.Password,
                Person = existingPerson
            };

            // Associar o User à Person
            existingPerson.User = newUser;

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(newUser);
        }

    }
}
