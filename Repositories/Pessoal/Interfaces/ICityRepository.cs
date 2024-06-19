using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCitiesAsync(string SystemLocationId);
        Task<City?> GetCityByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<City>> SearchCityByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddCityAsync(City city);
        Task<bool> UpdateCityAsync(City city);
        Task<bool> DeleteCityAsync(City city);
    }
}
