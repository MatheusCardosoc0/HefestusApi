

using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Interfaces
{
    public interface IPersonGroupRepository
    {
        Task<List<PersonGroup>> GetAllPersonGroupsAsync();
        Task<PersonGroup?> GetPersonGroupByIdAsync(int id);
        Task<List<PersonGroup>> SearchPersonGroupByNameAsync(string searchTerm);
        Task AddPersonGroupAsync(PersonGroup personGroup);
        Task<bool> UpdatePersonGroupAsync(PersonGroup personGroup);
        Task<bool> DeletePersonGroupAsync(PersonGroup personGroup);
    }
}
