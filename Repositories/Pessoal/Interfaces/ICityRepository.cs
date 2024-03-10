using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City?> GetCityByIdAsync(int id);
        Task<IEnumerable<City>> SearchCityByNameAsync(string searchTerm);
        Task<bool> AddCityAsync(City city);
        Task<bool> UpdateCityAsync(City city);
        Task<bool> DeleteCityAsync(City city);
    }
}
