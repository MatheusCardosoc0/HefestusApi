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
            return await _context.SystemLocation.ToListAsync();
        }

        public async Task<SystemLocation?> GetSystemLocationByIdAsync(string SystemLocationId)
        {
            return await _context.SystemLocation
                .FirstOrDefaultAsync(c => c.Id == SystemLocationId);
        }

        public async Task<SystemLocation?> GetSystemLocationByNameAsync(string name)
        {
            return await _context.SystemLocation
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<SystemLocation>> SearchSystemLocationByNameAsync(string searchTerm)
        {
            return await _context.SystemLocation
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<SystemLocation> SearchSystemLocationByNameCompareAsync(string searchTerm)
        {
            return await _context.SystemLocation.FirstOrDefaultAsync(p => p.Name == searchTerm);
        }

        public async Task<bool> AddSystemLocationAsync(SystemLocation systemLocation, Person personAdmin, User userAdmin, PersonGroup personGroup, City city)
        {
            await _context.SystemLocation.AddAsync(systemLocation);
            await _context.PersonGroup.AddAsync(personGroup);
            await _context.Cities.AddAsync(city);
            await _context.Person.AddAsync(personAdmin);
            await _context.Users.AddAsync(userAdmin);
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

        public async Task<bool> SaveCahngesAsymc()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
