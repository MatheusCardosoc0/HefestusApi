

using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Interfaces
{
    public interface IPersonGroupRepository
    {
        Task<List<PersonGroup>> GetAllPersonGroupsAsync(string SystemLocationId);
        Task<PersonGroup?> GetPersonGroupByIdAsync(string SystemLocationId, int id);
        Task<List<PersonGroup>> SearchPersonGroupByNameAsync(string searchTerm, string SystemLocationId);
        Task AddPersonGroupAsync(PersonGroup personGroup);
        Task<bool> UpdatePersonGroupAsync(PersonGroup personGroup);
        Task<bool> DeletePersonGroupAsync(PersonGroup personGroup);
    }
}
