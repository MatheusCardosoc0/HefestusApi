using HefestusApi.Models.Administracao;
using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Administracao.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByNameAsync(string name);
        Task<IEnumerable<User>> SearchUserByNameAsync(string searchTerm);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<Person?> GetPersonAsync(int id);
    }
}
