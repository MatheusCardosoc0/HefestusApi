using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync(string SystemLocationId);
        Task<ServiceResponse<UserDto>> GetUserByIdAsync(string SystemLocationId, string id);
        Task<ServiceResponse<IEnumerable<object>>> SearchUserByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<User>> CreateUserAsync(UserRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateUserAsync( string id, UserRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteUserAsync(string SystemLocationId, string id);
    }
}
