using HefestusApi.Models.Pessoal;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Pessoal.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync(string SystemLocationId);
        Task<Person?> GetPersonByIdAsync(string SystemLocationId, int id);
        Task<IEnumerable<Person>> SearchPersonByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddPersonAsync(Person person);
        Task<bool> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(Person person);
        Task<PersonGroup?> FindPersonGroupByIdAsync(string SystemLocationId, int id);
        Task<City?> FindCityByIdAsync(string SystemLocationId, int id);
    }
}
