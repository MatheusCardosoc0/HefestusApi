using HefestusApi.Models.Administracao;
using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Administracao.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Administracao
{
    public class SystemLocationRepository : ISystemLocationRepository
    {
        private readonly DataContext _context;

        public SystemLocationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SystemLocation>> GetAllSystemLocationsAsync()
        {
            return await _context.SystemLocation
                .Include(x => x.Person)
                .ToListAsync();
        }

        public async Task<SystemLocation?> GetSystemLocationByIdAsync(int id)
        {
            return await _context.SystemLocation
                .Include(c => c.Person)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<SystemLocation?> GetSystemLocationByNameAsync(string name)
        {
            return await _context.SystemLocation
                .Include(c => c.Person)
                .FirstOrDefaultAsync(c => c.Person.Name == name);
        }

        public async Task<IEnumerable<SystemLocation>> SearchSystemLocationByNameAsync(string searchTerm)
        {
            return await _context.SystemLocation
                .Where(p => EF.Functions.Like(p.Person.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddSystemLocationAsync(SystemLocation systemLocation)
        {
            _context.SystemLocation.Add(systemLocation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateSystemLocationAsync(SystemLocation systemLocation)
        {
            _context.Entry(systemLocation).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteSystemLocationAsync(SystemLocation systemLocation)
        {
            _context.SystemLocation.Remove(systemLocation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Person?> GetPersonAsync(int id)
        {
            return await _context.Person.Include(p => p.SystemLocation).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> SaveCahngesAsymc()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
