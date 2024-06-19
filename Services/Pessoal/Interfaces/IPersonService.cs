using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Pessoal.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResponse<IEnumerable<PersonDto>>> GetAllPersonsAsync(string SystemLocationId);
        Task<ServiceResponse<Person>> GetPersonByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPersonByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<Person>> CreatePersonAsync(PersonRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdatePersonAsync(string SystemLocationId, int id, PersonRequestDataDto request);
        Task<ServiceResponse<bool>> DeletePersonAsync(string SystemLocationId, int id);
    }
}
