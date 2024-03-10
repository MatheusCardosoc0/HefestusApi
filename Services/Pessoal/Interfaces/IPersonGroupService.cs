using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Services.functions;

namespace HefestusApi.Services.Interfaces
{
    public interface IPersonGroupService
    {
        Task<ServiceResponse<IEnumerable<PersonGroupDto>>> GetAllPersonGroupsAsync();
        Task<ServiceResponse<PersonGroup>> GetPersonGroupByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPersonGroupByNameAsync(string searchTerm, string detailLevel);
        Task<ServiceResponse<PersonGroup>> CreatePersonGroupAsync(PersonGroupRequestDataDto request);
        Task<ServiceResponse<bool>> UpdatePersonGroupAsync(int id, PersonGroupRequestDataDto request);
        Task<ServiceResponse<bool>> DeletePersonGroupAsync(int id);
    }
}
