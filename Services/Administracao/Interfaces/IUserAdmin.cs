using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao.Interfaces
{
    public interface IUserAdminService
    {
        Task<ServiceResponse<string>> CreateUserAdminAsync();
        Task<ServiceResponse<string>> NewPasswordUserAdminAsync();
        Task<ServiceResponse<Dictionary<string, string>>> AuthUserAdminAsync(string passowrd);
    }
}
