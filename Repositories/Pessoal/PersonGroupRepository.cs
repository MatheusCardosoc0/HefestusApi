using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories
{
    public class PersonGroupRepository : IPersonGroupRepository
    {
        private readonly DataContext _context;

        public PersonGroupRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PersonGroup>> GetAllPersonGroupsAsync(string SystemLocationId)
        {
            return await _context.PersonGroup.Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task<PersonGroup?> GetPersonGroupByIdAsync(string SystemLocationId, int id)
        {
            return await _context.PersonGroup
                .Include(pg => pg.Persons)
                .FirstOrDefaultAsync(pg => pg.Id == id);
        }

        public async Task<List<PersonGroup>> SearchPersonGroupByNameAsync(string searchTerm, string SystemLocationId)
        {
            return await _context.PersonGroup
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") && p.SystemLocationId == SystemLocationId)
                .Where(p => p.SystemLocationId == SystemLocationId).ToListAsync();
        }

        public async Task AddPersonGroupAsync(PersonGroup personGroup)
        {
            _context.PersonGroup.Add(personGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePersonGroupAsync(PersonGroup personGroup)
        {
            _context.Entry(personGroup).State = EntityState.Modified;
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> DeletePersonGroupAsync(PersonGroup personGroup)
        {       
            _context.PersonGroup.Remove(personGroup);
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
