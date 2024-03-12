using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Interfaces
{
    public interface ICityService
    {
        Task<ServiceResponse<IEnumerable<CityDto>>> GetAllCitiesAsync();
        Task<ServiceResponse<City>> GetCityByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchCityByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<City>> CreateCityAsync(CityRequestDataDto request);
        Task<ServiceResponse<bool>> UpdateCityAsync(int id, CityRequestDataDto request);
        Task<ServiceResponse<bool>> DeleteCityAsync(int id);
    }
}
