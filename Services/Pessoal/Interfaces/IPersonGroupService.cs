using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Interfaces
{
    public interface IPersonGroupService
    {
        Task<ServiceResponse<IEnumerable<PersonGroupDto>>> GetAllPersonGroupsAsync(string SystemLocationId);
        Task<ServiceResponse<PersonGroup>> GetPersonGroupByIdAsync(string SystemLocationId, int id);
        Task<ServiceResponse<IEnumerable<object>>> SearchPersonGroupByNameAsync(string searchTerm, string detailLevel, string SystemLocationId);
        Task<ServiceResponse<PersonGroup>> CreatePersonGroupAsync(PersonGroupRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> UpdatePersonGroupAsync( int id, PersonGroupRequestDataDto request, string SystemLocationId);
        Task<ServiceResponse<bool>> DeletePersonGroupAsync(string SystemLocationId, int id);
    }
}
