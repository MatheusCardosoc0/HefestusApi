using AutoMapper;
using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFramework7Relationships.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class personController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public personController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PersonDto>> GetPersons()
        {
            var persons = await _context.Person
                .Include(c => c.PersonGroup)
                .Include(c => c.City)
                .ToListAsync();
            var personDtos = _mapper.Map<IEnumerable<PersonDto>>(persons);

            return Ok(personDtos);
        }

        [HttpGet("search/{detailLevel}/{searchTerm}")]
        public async Task<ActionResult> GetPersonsBySearchTerm(string searchTerm, string detailLevel)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
            {
                var persons = await _context.Person
                    .Where(c => c.Name.ToLower().Contains(lowerCaseSearchTerm))
                    .Select(p => new PersonSearchTermDto(p.Id, p.Name))
                    .ToListAsync();

                return Ok(persons);
            }
            else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
            {
                var personsComplete = await _context.Person
                    .Where(c => c.Name.ToLower().Contains(lowerCaseSearchTerm))
                    .Include(c => c.PersonGroup)
                    .Include(c => c.City)
                    .ToListAsync();

                var dto = _mapper.Map<IEnumerable<PersonPostOrPutDto>>(personsComplete);

                return Ok(dto);
            }
            else
            {
                return BadRequest("Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonPostOrPutDto>> GetPersonById(int id)
        {
            var person = await _context.Person
                .Include(c => c.PersonGroup)
                .Include(c => c.City)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (person == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            var dto = _mapper.Map<PersonPostOrPutDto>(person);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson(PersonPostOrPutDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (request == null)
            {
                return BadRequest("Dados do pedido inválidos ou incompletos.");
            }
            if(request.PersonGroup == null) 
            {
                return BadRequest("Grupo deve ser informada.");
            }
            if (request.City == null)
            {
                return BadRequest("Cidade deve ser informada.");
            }

            if (request.PersonGroup == null || !request.PersonGroup.Any())
            {
                return BadRequest("Pelo menos um Grupo deve ser informado.");
            }

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
                IsBlocked = false,
                MaritalStatus = request.MaritalStatus,
                Habilities = request.Habilities,
                Description = request.Description,
                PersonGroup = new List<PersonGroup>(),
                Gender = request.Gender,
                ICMSContributor = request.ICMSContributor,
                PersonType = request.PersonType
             };

           
            foreach (var group in request.PersonGroup)
            {
                var existingGroup = await _context.PersonGroup
                    .FindAsync(group.Id);

                if (existingGroup != null)
                {
                    newPerson.PersonGroup.Add(existingGroup);
                }
                else
                {
                    return BadRequest($"Grupo de pessoas {group.Id} não encontrada");
                }
            }

            
            var existingCity = await _context.Cities
                .FindAsync(request.City.Id);

            if (existingCity != null)
            {
                newPerson.City = existingCity;
                newPerson.CityId = existingCity.Id;
            }
            else
            {
                return BadRequest($"Cidade {request.City.Id} não encontrada");
            }
            

            _context.Person.Add(newPerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<PersonDto>> UpdatePerson(int id, PersonPostOrPutDto request)
        {
            var person = await _context.Person
                .Include(c => c.PersonGroup)
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
            person.Gender = request.Gender;
            person.ICMSContributor = request.ICMSContributor;
            person.PersonType = request.PersonType;

            person.PersonGroup.Clear();

            foreach (var group in request.PersonGroup)
            {
                var existingGroup = await _context.PersonGroup
                    .FindAsync(group.Id);

                if (existingGroup != null)
                {
                    person.PersonGroup.Add(existingGroup);
                }
                else
                {
                    return BadRequest($"Grupo de pessoas {group.Id} não encontrada");
                }
            }


            var existingCity = await _context.Cities
                    .FindAsync(request.City.Id);

            if (existingCity != null)
            {
                person.City = existingCity;
                person.CityId = existingCity.Id;
            }
            else
            {
                return BadRequest($"Cidade {request.City.Id} não encontrada");
            }

            await _context.SaveChangesAsync();
            
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            var person = await _context.Person
                .Include(p => p.User)
                .FirstAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            if(person.User != null)
            {
                return NotFound($"Essa pessoa não pode ser deletada, pois está associada a um usuário");
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

       

    }
}