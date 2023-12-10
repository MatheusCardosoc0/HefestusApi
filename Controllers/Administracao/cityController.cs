using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utils;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<City>> GetCitys()
        {
            var personGroup = await _context.Cities
                .ToListAsync();

            return Ok(personGroup);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCityById(int id)
        {
            var personGroup = await _context.Cities
                .FirstOrDefaultAsync(c => c.Id == id);

            if (personGroup == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            return Ok(personGroup);
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(CityDto request)
        {
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
            var personGroup = await _context.Cities.FindAsync(id);

            if (personGroup == null)
            {
                return NotFound();
            }

            personGroup.Name = request.Name;

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
            var personGroup = await _context.Cities
                .Include(x => x.Persons)
                .FirstOrDefaultAsync(personGroup => personGroup.Id == id);

            if (personGroup == null)
            {
                return NotFound($"Pessoa com o ID {id} não existe");
            }

            if (personGroup.Persons.Any())
            {
                return BadRequest("Não é possível excluir a cidade, pois existem pessoas associadas a ele.");
            }

            _context.Cities.Remove(personGroup);
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
