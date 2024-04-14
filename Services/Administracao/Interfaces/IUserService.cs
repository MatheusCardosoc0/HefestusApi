using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ServiceResponse<UserDto>> GetUserByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchUserByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<User>> CreateUserAsync(UserRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateUserAsync(int id, UserRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
    }
}
