using HefestusApi.Models.Administracao;
using HefestusApi.Models.Pessoal;

namespace HefestusApi.Repositories.Administracao.Interfaces
{
    public interface IUserAdminRepository
    {
        Task<bool> AddUserAdminAsync(UserAdmin userAdmin);
        Task<bool> UpdateUserAdminAsync(UserAdmin userAdmin);
        Task<UserAdmin> GetUserAdmin();
    }
}
