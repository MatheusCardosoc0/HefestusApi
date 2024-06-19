using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao.Interfaces
{
    public interface ISystemLocationService
    {
        Task<ServiceResponse<IEnumerable<SystemLocationDto>>> GetAllSystemLocationsAsync();
        Task<ServiceResponse<object>> GetSystemLocationByIdAsync(string detailLevel, string SystemLocationId);
        Task<ServiceResponse<IEnumerable<object>>> SearchSystemLocationByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<SystemLocation>> CreateSystemLocationAsync(SystemLocationRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateSystemLocationAsync( SystemLocationRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteSystemLocationAsync(string SystemLocationId);
        Task<ServiceResponse<Dictionary<string, string>>> AuthSystemLocationsAsync(RequiredCampsForAuthentication requiredCampsForAuthentication);
        Task<ServiceResponse<Dictionary<string, string>>> ValidateTokenAsync(string token);
    }
}
