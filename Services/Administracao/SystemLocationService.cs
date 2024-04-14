using AutoMapper;
using HefestusApi.DTOs.Administracao;
using HefestusApi.Models.Administracao;
using HefestusApi.Repositories.Administracao.Interfaces;
using HefestusApi.Services.Administracao.Interfaces;
using HefestusApi.Utilities.functions;

namespace HefestusApi.Services.Administracao
{
    public class SystemLocationService: ISystemLocationService
    {
        private readonly ISystemLocationRepository _systemLocationRepository;
        private readonly IMapper _mapper;

        public SystemLocationService(ISystemLocationRepository systemLocationRepository, IMapper mapper)
        {
            _systemLocationRepository = systemLocationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<SystemLocationDto>>> GetAllSystemLocationsAsync()
        {
            var response = new ServiceResponse<IEnumerable<SystemLocationDto>>();
            try
            {
                var systemLocations = await _systemLocationRepository.GetAllSystemLocationsAsync();

                var systemLocationDtos = _mapper.Map<IEnumerable<SystemLocationDto>>(systemLocations);

                response.Data = systemLocationDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter todas as locals: " + ex.Message;
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<SystemLocationDto>> GetSystemLocationByIdAsync(int id)
        {
            var response = new ServiceResponse<SystemLocationDto>();
            try
            {
                var systemLocation = await _systemLocationRepository.GetSystemLocationByIdAsync(id);
                if (systemLocation == null)
                {
                    response.Success = false;
                    response.Message = "Usuário não encontrada.";
                    return response;
                }

                var systemLocationDtos = _mapper.Map<SystemLocationDto>(systemLocation);

                response.Data = systemLocationDtos;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao tentar obter a local: " + ex.Message;
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<IEnumerable<object>>> SearchSystemLocationByNameAsync(string searchTerm, string detailLevel)
        {
            var response = new ServiceResponse<IEnumerable<object>>();
            try
            {
                var systemLocations = await _systemLocationRepository.SearchSystemLocationByNameAsync(searchTerm.ToLower());

                if (detailLevel.Equals("simple", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<IEnumerable<SystemLocationSimpleSearchDataDto>>(systemLocations).Cast<object>().ToList();
                }
                else if (detailLevel.Equals("complete", StringComparison.OrdinalIgnoreCase))
                {
                    response.Data = _mapper.Map<IEnumerable<SystemLocationDto>>(systemLocations).Cast<object>().ToList();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Nível de detalhe não reconhecido. Use 'simple' ou 'complete'.";
                    return response;
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

        public async Task<ServiceResponse<SystemLocation>> CreateSystemLocationAsync(SystemLocationRequestDataDto request)
        {
            var response = new ServiceResponse<SystemLocation>();
            try
            {

                var existingPerson = await _systemLocationRepository.GetPersonAsync(request.PersonId);

                var exisitingSystemLocation = await _systemLocationRepository.GetSystemLocationByNameAsync(request.PersonName);

                if (exisitingSystemLocation != null)
                {
                    response.Success = false;
                    response.Message = $"Já existe o local {request.PersonName} com esse nome!";
                    return response;
                }

                if (existingPerson == null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o id {request.PersonId} não encontrada!";
                    return response;
                }

                if (existingPerson.SystemLocation != null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o id {request.PersonId} já tem um local!";
                    return response;
                }

                var systemLocation = new SystemLocation
                {
                    Description = request.Description,
                    PersonId = request.PersonId,
                    Person = existingPerson,
                };

                await _systemLocationRepository.AddSystemLocationAsync(systemLocation);

                response.Data = systemLocation;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao criar a local: {ex.Message}";
                return response;
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> UpdateSystemLocationAsync(int id, SystemLocationRequestDataDto request)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var systemLocationToUpdate = await _systemLocationRepository.GetSystemLocationByIdAsync(id);

                if (systemLocationToUpdate == null)
                {
                    response.Success = false;
                    response.Message = $"Usuário com o ID {id} não encontrado.";
                    return response;
                }


                if (systemLocationToUpdate.Person.Name != request.PersonName)
                {
                    response.Success = false;
                    response.Message = $"O nome do local não pode ser alterado";
                    return response;
                }

                var existingPerson = await _systemLocationRepository.GetPersonAsync(request.PersonId);
                if (existingPerson == null)
                {
                    response.Success = false;
                    response.Message = $"Pessoa com o ID {request.PersonId} não encontrada.";
                    return response;
                }

                if (existingPerson.SystemLocation != null && existingPerson.SystemLocation.Id != id)
                {
                    response.Success = false;
                    response.Message = $"A pessoa com ID {request.PersonId} já está vinculada a outro local.";
                    return response;
                }

                systemLocationToUpdate.Description = request.Description;
                systemLocationToUpdate.PersonId = request.PersonId;
                systemLocationToUpdate.Person = existingPerson;

                bool updateResult = await _systemLocationRepository.UpdateSystemLocationAsync(systemLocationToUpdate);
                if (!updateResult)
                {
                    throw new Exception("A atualização do local falhou por uma razão desconhecida.");
                }

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Erro ao atualizar a local: {ex.Message}";
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteSystemLocationAsync(int id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var systemLocation = await _systemLocationRepository.GetSystemLocationByIdAsync(id);

                if (systemLocation == null)
                {
                    response.Success = false;
                    response.Message = $"Usuário com o ID {id} não existe.";
                    return response;
                }

                bool deleted = await _systemLocationRepository.DeleteSystemLocationAsync(systemLocation);
                if (!deleted)
                {
                    response.Success = false;
                    response.Message = "Não foi possível deletar a local devido a restrições de integridade.";
                    return response;
                }

                response.Data = deleted;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um erro ao deletar a local: " + ex.Message;
            }

            return response;
        }
    }
}
