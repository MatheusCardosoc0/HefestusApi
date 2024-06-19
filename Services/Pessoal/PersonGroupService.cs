using HefestusApi.DTOs.Pessoal;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories;
using HefestusApi.Repositories.Interfaces;
using HefestusApi.Services.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services
{
    public class PersonGroupService : IPersonGroupService
    {
        private readonly IPersonGroupRepository _personGroupRepository;

        public PersonGroupService(IPersonGroupRepository personGroupRepository)
        {
            _personGroupRepository = personGroupRepository;
        }

        public async Task<ServiceResponse<IEnumerable<PersonGroupDto>>> GetAllPersonGroupsAsync(string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<PersonGroupDto>>();
            try
            {
                var personGroups = await _personGroupRepository.GetAllPersonGroupsAsync(SystemLocationId);
                var personGroupDtos = personGroups.Select(c => new PersonGroupDto { Id = c.Id, Name = c.Name, CreatedAt = c.CreatedAt, LastModifiedAt = c.LastModifiedAt });

                response.Data = personGroupDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as grupo de pessoas: " + ex;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<PersonGroup>> GetPersonGroupByIdAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<PersonGroup>();
            try
            {
                var personGroup = await _personGroupRepository.GetPersonGroupByIdAsync(SystemLocationId, id);
                if (personGroup == null)
                {
                    response.Success = false;
                    response.Message = "Grupo de pessoas não encontrada.";
                    return response;
                }

                response.Data = personGroup;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a grupo de pessoas: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchPersonGroupByNameAsync(string searchTerm, string detailLevel, string SystemLocationId)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var personGroups = await _personGroupRepository.SearchPersonGroupByNameAsync(searchTerm.ToLower(), SystemLocationId);

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    var simpleDtos = personGroups.Select(c => new PersonGroupSimpleSearchDataDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).Cast<object>().ToList();

                    response.Data = simpleDtos;
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = personGroups.Cast<object>().ToList();
                }
                else
                {
                    throw new ArgumentException("Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao processar a busca: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<PersonGroup>> CreatePersonGroupAsync(PersonGroupRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<PersonGroup>();
            try
            {
                var personGroup = new PersonGroup
                {
                    Name = request.Name,
                    SystemLocationId = SystemLocationId
                };

                await _personGroupRepository.AddPersonGroupAsync(personGroup);

                response.Data = personGroup;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a grupo de pessoas: {ex}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdatePersonGroupAsync(int id, PersonGroupRequestDataDto request, string SystemLocationId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var personGroup = await _personGroupRepository.GetPersonGroupByIdAsync(SystemLocationId, id);
                if (personGroup == null)
                {
                    response.Success = false;
                    response.Message = $"Grupo de pessoas com o ID {id} não foi encontrada.";
                    return response;
                }

                personGroup.Name = request.Name;
                personGroup.SystemLocationId = SystemLocationId;

                bool updateResult = await _personGroupRepository.UpdatePersonGroupAsync(personGroup);
                if (!updateResult)
                {
                    throw new Exception("A atualização da grupo de pessoas falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a grupo de pessoas: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeletePersonGroupAsync(string SystemLocationId, int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var personGroup = await _personGroupRepository.GetPersonGroupByIdAsync(SystemLocationId, id);

                if (personGroup == null)
                {
                    response.Success = false;
                    response.Message = $"Grupo de pessoas com o ID {id} não existe.";
                    return response;
                }

                if (personGroup.Persons.Any())
                {
                    response.Success = false;
                    response.Message = $"Grupo de pessoas não pode ser excluido, pois está relacionado a pessoas.";
                    return response;
                }

                bool deleted = await _personGroupRepository.DeletePersonGroupAsync(personGroup);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a grupo de pessoas devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a grupo de pessoas: " + ex.Message;
                return response;
            }

            return response;
        }
    }
}
