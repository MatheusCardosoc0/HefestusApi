using HefestusApi.Models.Administracao;
using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Administracao.Interfaces
{
    public interface ISystemLocationRepository
    {
        Task<IEnumerable<SystemLocation>> GetAllSystemLocationsAsync();
        Task<SystemLocation?> GetSystemLocationByIdAsync(int id);
        Task<SystemLocation?> GetSystemLocationByNameAsync(string name);
        Task<IEnumerable<SystemLocation>> SearchSystemLocationByNameAsync(string searchTerm);
        Task<bool> AddSystemLocationAsync(SystemLocation systemLocation);
        Task<bool> UpdateSystemLocationAsync(SystemLocation systemLocation);
        Task<bool> DeleteSystemLocationAsync(SystemLocation systemLocation);
        Task<Person?> GetPersonAsync(int id);
    }
}
