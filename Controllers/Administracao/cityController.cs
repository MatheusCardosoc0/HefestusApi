using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Controllers.Administracao
{
    [Route("api/[controller]")]
    [ApiController]
    public class cityController : ControllerBase
    {
        private readonly DataContext _context;

        public cityController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<CityDto>> GetCitys()
        {
            var city = await _context.Cities
                .ToListAsync();

            return Ok(city);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            var city = await _context.Cities
                .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            return Ok(city);
        }
        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<City>>> GetCityBySearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Não foi informado um termo de pesquisa");
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            var cities = await _context.Cities
                .Where(c => c.Name.ToLower().Contains(lowerCaseSearchTerm))
                .ToListAsync();

            return Ok(cities);
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(CityDto request)
        {
            var checkCity = await _context.Cities
                .FirstOrDefaultAsync(c => c.IBGENumber == request.IBGENumber);

            if (checkCity != null)
            {
                return BadRequest($"Cidade com o número de IBGE{checkCity.IBGENumber} já cadastrada");
            }

            var newCity = new City
            {
                Name = request.Name,
                IBGENumber = request.IBGENumber,
                State = request.State
            };

            _context.Cities.Add(newCity);
            await _context.SaveChangesAsync();

            return Ok(newCity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<City>> UpdateCity(int id, CityDto request)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound($"Cidade com este id não encontrada");
            }

            city.Name = request.Name;
            city.State = request.State;
            city.IBGENumber = request.IBGENumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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
        public async Task<ActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities
                .Include(x => x.Persons)
                .FirstOrDefaultAsync(city => city.Id == id);

            if (city == null)
            {
                return NotFound($"Cidade com o ID {id} não existe");
            }

            if (city.Persons.Any())
            {
                return BadRequest("Não é possível excluir a cidade, pois existem pessoas associadas a ele.");
            }

            _context.Cities.Remove(city);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Internal server error while deleting the city.");
            }
            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
