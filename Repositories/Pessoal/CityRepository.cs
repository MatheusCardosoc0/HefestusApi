using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<City?> GetCityByIdAsync(int id)
        {
            return await _context.Cities
                .Include(c => c.Persons)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<City>> SearchCityByNameAsync(string searchTerm)
        {
            return await _context.Cities
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddCityAsync(City city)
        {
            _context.Cities.Add(city);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCityAsync(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCityAsync(City city)
        {     
            _context.Cities.Remove(city);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
