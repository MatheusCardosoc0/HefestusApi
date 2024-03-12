using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Pessoal.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResponse<IEnumerable<PersonDto>>> GetAllPersonsAsync();
        Task<ServiceResponse<Person>> GetPersonByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPersonByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<Person>> CreatePersonAsync(PersonRequestDataDto request);
        Task<ServiceResponse<bool>> UpdatePersonAsync(int id, PersonRequestDataDto request);
        Task<ServiceResponse<bool>> DeletePersonAsync(int id);
    }
}
