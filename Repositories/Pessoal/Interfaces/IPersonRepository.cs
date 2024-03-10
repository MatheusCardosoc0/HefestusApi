using HefestusApi.Models.Pessoal;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Pessoal.Interfaces
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Person?> GetPersonByIdAsync(int id);
        Task<IEnumerable<Person>> SearchPersonByNameAsync(string searchTerm);
        Task<bool> AddPersonAsync(Person person);
        Task<bool> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(Person person);
        Task<PersonGroup?> FindPersonGroupByIdAsync(int id);
        Task<City?> FindCityByIdAsync(int id);
    }
}
