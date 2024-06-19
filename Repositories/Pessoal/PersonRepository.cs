using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Pessoal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Pessoal
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;

        public PersonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPersonAsync(Person person)
        {
            await _context.Person.AddAsync(person);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePersonAsync(Person person)
        {
            _context.Person.Remove(person);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync(string SystemLocationId)
        {
            return await _context.Person
                .Include(p => p.City)
                .Include(p => p.PersonGroup)
                .OrderBy(p => p.Name)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(string SystemLocationId, int id)
        {
            return await _context.Person
                .Include(p => p.City)
                .Include(p => p.PersonGroup)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> SearchPersonByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.Person
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") && p.SystemLocationId == SystemLocationId)
                .Include(p => p.City)
                .Include(p => p.PersonGroup)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<PersonGroup?> FindPersonGroupByIdAsync(string SystemLocationId, int id)
        {
            return await _context.PersonGroup.FindAsync(SystemLocationId, id);
        }

        public async Task<City?> FindCityByIdAsync(string SystemLocationId, int id)
        {
            return await _context.Cities.FindAsync(SystemLocationId, id);
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
