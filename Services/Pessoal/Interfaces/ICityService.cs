using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Interfaces
{
    public interface ICityService
    {
        Task<ServiceResponse<IEnumerable<CityDto>>> GetAllCitiesAsync(string SystemLocationId);
        Task<ServiceResponse<City>> GetCityByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchCityByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<City>> CreateCityAsync(CityRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdateCityAsync(int id, CityRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeleteCityAsync(string SystemLocationId, int id);
    }
}
