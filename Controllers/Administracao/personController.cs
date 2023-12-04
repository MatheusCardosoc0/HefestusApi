using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework7Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class personController : ControllerBase
    {
        private readonly DataContext _context;

        public personController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Person>> GetPersonById()
        {
            var person = await _context.Person
                .Include(c => c.PersonGroups)
                .Include(c => c.User)
                .ToListAsync();

            return Ok(person);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var person = await _context.Person
                .Include(c => c.PersonGroups)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (person == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<List<Person>>> CreatePerson(PersonDto request)
        {
            var newPerson = new Person
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Age = request.Age,
                CPF = request.CPF,
                Address = request.Address,
                BirthDate = request.BirthDate,
                IBGE = request.IBGE,
                Razao = request.Razao,
                InscricaoEstadual = request.InscricaoEstadual,
                CEP = request.CEP,
                UrlImage = request.UrlImage,
                IsBlocked = request.IsBlocked,
                MaritalStatus = request.MaritalStatus,
                Habilities = request.Habilities,
                Description = request.Description,
                PersonGroups = new List<PersonGroup>()
            };

            if (request.PersonGroup != null)
            {
                foreach (var groupDto in request.PersonGroup)
                {
                    var existingGroup = await _context.PersonGroup
                        .FirstOrDefaultAsync(pg => pg.Name == groupDto.Name);

                    if (existingGroup != null)
                    {
                        newPerson.PersonGroups.Add(existingGroup);
                    }
                    else
                    {
                        var newGroup = new PersonGroup { Name = groupDto.Name };
                        newPerson.PersonGroups.Add(newGroup);
                    }
                }
            }

            _context.Person.Add(newPerson);
            await _context.SaveChangesAsync();

            return Ok(newPerson);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Person>> UpdatePerson(int id, PersonDto request)
        {
            var person = await _context.Person
                .Include(c => c.PersonGroups)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (person == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            person.Name = request.Name;
            person.Email = request.Email;
            person.Phone = request.Phone;
            person.Age = request.Age;
            person.CPF = request.CPF;
            person.Address = request.Address;
            person.BirthDate = request.BirthDate;
            person.IBGE = request.IBGE;
            person.Razao = request.Razao;
            person.InscricaoEstadual = request.InscricaoEstadual;
            person.CEP = request.CEP;
            person.UrlImage = request.UrlImage;
            person.IsBlocked = request.IsBlocked;
            person.MaritalStatus = request.MaritalStatus;
            person.Habilities = request.Habilities;
            person.Description = request.Description;

            if (request.PersonGroup != null)
            {
                var newGroupsList = new List<PersonGroup>();

                foreach (var groupDto in request.PersonGroup)
                {
                    var existingGroup = await _context.PersonGroup
                        .FirstOrDefaultAsync(pg => pg.Name == groupDto.Name);

                    if (existingGroup != null)
                    {
                        newGroupsList.Add(existingGroup);
                    }
                    else
                    {
                        var newGroup = new PersonGroup { Name = groupDto.Name };
                        newGroupsList.Add(newGroup);
                    }
                }

                person.PersonGroups = newGroupsList;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            _context.Person.Remove(person);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while deleting the person.");
            }

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

    }
}