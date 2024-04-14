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

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            return await _context.Person
                .Include(p => p.City)
                .Include(p => p.PersonGroup)
                .ToListAsync();
        }

        public async Task<Person?> GetPersonByIdAsync(int id)
        {
            return await _context.Person
                .Include(p => p.City)
                .Include(p => p.PersonGroup)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> SearchPersonByNameAsync(string searchTerm)
        {
            return await _context.Person
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .Include(p => p.City)
                .Include(p => p.PersonGroup)    
                .ToListAsync();
        }

        public async Task<PersonGroup?> FindPersonGroupByIdAsync(int id)
        {
            return await _context.PersonGroup.FindAsync(id);
        }

        public async Task<City?> FindCityByIdAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
