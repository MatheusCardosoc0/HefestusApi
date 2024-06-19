using HefestusApi.Models.Administracao;
using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Administracao.Interfaces
{
    public interface ISystemLocationRepository
    {
        Task<IEnumerable<SystemLocation>> GetAllSystemLocationsAsync();
        Task<SystemLocation?> GetSystemLocationByIdAsync(string SystemLocationId);
        Task<SystemLocation?> GetSystemLocationByNameAsync(string name);
        Task<IEnumerable<SystemLocation>> SearchSystemLocationByNameAsync(string searchTerm);
        Task<bool> AddSystemLocationAsync(SystemLocation systemLocation, Person personAdmin, User userAdmin, PersonGroup personGroup, City city);
        Task<bool> UpdateSystemLocationAsync(SystemLocation systemLocation);
        Task<bool> DeleteSystemLocationAsync(SystemLocation systemLocation);
        Task<SystemLocation> SearchSystemLocationByNameCompareAsync(string searchTerm);
    }
}
