using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao.Interfaces
{
    public interface ISystemLocationService
    {
        Task<ServiceResponse<IEnumerable<SystemLocationDto>>> GetAllSystemLocationsAsync();
        Task<ServiceResponse<SystemLocationDto>> GetSystemLocationByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchSystemLocationByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<SystemLocation>> CreateSystemLocationAsync(SystemLocationRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateSystemLocationAsync(int id, SystemLocationRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteSystemLocationAsync(int id);
    }
}
