using HefestusApi.Models.Administracao;
using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Administracao.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(string SystemLocationId);
        Task<User?> GetUserByIdAsync(string SystemLocationId, string id);
        Task<User?> GetUserByNameAsync(string name, string systemLocationId);
        Task<IEnumerable<User>> SearchUserByNameAsync(string searchTerm, string SystemLocationId);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<Person?> GetPersonAsync(string SystemLocationId, int id);
    }
}
