using AutoMapper;
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
        private readonly IMapper _mapper;

        public userController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewDto>>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Person).ToListAsync();
            var userDtos = _mapper.Map<IEnumerable<UserViewDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("id")]
        public async Task<ActionResult<UserViewDto>> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync();

            if(user == null)
            {
                return NotFound($"Usuário com o id {id} não existe");
            }

            var userDto = _mapper.Map<UserViewDto>(user);
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto request)
        {
            var existingPerson = await _context.Person.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == request.PersonId);
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.Name);

            if (existingPerson == null)
            {
                return NotFound($"Person não encontrada com o ID {request.PersonId}");
            }

            if (existingPerson.User != null)
            {
                return BadRequest($"Person com ID {request.PersonId} já tem um User associado.");
            }

            if(existingUser != null)
            {
                return BadRequest($"Já existe um usuário com esse nome");
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

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, UserDto request)
        {
            var user = await _context.Users
                .Include(u => u.Person)
                .FirstOrDefaultAsync(u => u.Id == id && u.Name == request.Name);

            if (user == null)
            {
                return NotFound($"User com o ID {id} não encontrado");
            }

            if (!string.IsNullOrWhiteSpace(request.Password) && !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.Password = hashedPassword;
            }

            if (request.PersonId != null)
            {
                var person = await _context.Person
                    .Include(p => p.User)
                    .FirstOrDefaultAsync(p => p.Id == request.PersonId);
                    
                   
                if (person == null)
                {
                    return NotFound($"Person com o ID {request.PersonId} não encontrado");
                }
                if(person.UserId != user.Id)
                {
                    return BadRequest($"Pessoa com o id {request.PersonId} já possui outro usuário relacionado");
                }

                user.Person = person;
            }
            else
            {
                return BadRequest("Deve ser associado uma pessoa ao usuário");
            }

            await _context.SaveChangesAsync();

            return NoContent();
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
